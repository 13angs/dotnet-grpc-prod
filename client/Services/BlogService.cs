using DGP.Client.Protos;
using Grpc.Net.Client;

namespace DGP.Client.Services
{
    public class BlogService : IBlogService
    {
        private readonly ILogger<BlogService> _logger;
        private readonly IConfiguration _configuration;
        private readonly string _endpoint;

        public BlogService(ILogger<BlogService> logger, IConfiguration configuration)
        {
            _logger = logger;
            _configuration = configuration;
            _endpoint = _configuration["GrpcServers:Blog:Endpoint"];
        }
        public IEnumerable<GrpcGetBlogModel> GetBlogs(GrpcGetBlogRequest request)
        {
            var httpHandler = new HttpClientHandler();
            httpHandler.ServerCertificateCustomValidationCallback = HttpClientHandler.DangerousAcceptAnyServerCertificateValidator;

            var channel = GrpcChannel.ForAddress(_endpoint, new GrpcChannelOptions { HttpHandler = httpHandler });
            var client = new GrpcBlog.GrpcBlogClient(channel);

            try
            {
                var reply = client.GrpcGetBlogs(request);
                return reply.Blogs;

            }
            catch (Exception e)
            {

                _logger.LogError(e.Message, e);
                return null!;
            }
        }
        public GrpcGetBlogModel CreateBlog(GrpcGetBlogRequest request)
        {
            // var httpHandler = new HttpClientHandler();
            // httpHandler.ServerCertificateCustomValidationCallback = HttpClientHandler.DangerousAcceptAnyServerCertificateValidator;

            // keep alive handler
            var handler = new SocketsHttpHandler{
                PooledConnectionIdleTimeout=Timeout.InfiniteTimeSpan,
                KeepAlivePingDelay=TimeSpan.FromSeconds(60),
                KeepAlivePingTimeout=TimeSpan.FromSeconds(30),
                EnableMultipleHttp2Connections=true
            };

            var channel = GrpcChannel.ForAddress(_endpoint, new GrpcChannelOptions { HttpHandler = handler });
            var client = new GrpcBlog.GrpcBlogClient(channel);

            try
            {
                var reply = client.GrpcPostBlog(request);
                return reply;

            }
            catch (Exception e)
            {

                _logger.LogError(e.Message, e);
                return null!;
            }
        }
    }
}