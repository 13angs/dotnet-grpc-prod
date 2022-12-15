using DGP.Server.Models;
using DGP.Server.Protos;
using Grpc.Core;

namespace DGP.Server.Services
{
    public class GrpcServerBlogService : GrpcBlog.GrpcBlogBase
    {
        private readonly BlogContext _context;
        private readonly ILogger<GrpcServerBlogService> _logger;

        public GrpcServerBlogService(BlogContext context, ILogger<GrpcServerBlogService> logger)
        {
            _context = context;
            _logger = logger;
        }
        public override Task<GrpcGetBlogResponse> GrpcGetBlogs(GrpcGetBlogRequest request, ServerCallContext context)
        {
            IEnumerable<Blog> blogs = _context.Blogs;
            GrpcGetBlogResponse response = new GrpcGetBlogResponse();

            foreach(Blog blog in blogs)
            {
                GrpcGetBlogModel model = new GrpcGetBlogModel();
                model.Id=blog.Id;
                model.Name=blog.Name;
                response.Blogs.Add(model);
            }

            return Task.FromResult(response);
        }

        public override Task<GrpcGetBlogModel> GrpcPostBlog(GrpcGetBlogRequest request, ServerCallContext context)
        {
            GrpcGetBlogModel model = new GrpcGetBlogModel();
            Blog blog = new Blog{
                Name=request.Name
            };

            try{
                _context.Blogs.Add(blog);
                _context.SaveChanges();
            }catch (Exception e)
            {
                _logger.LogError(e.Message);
            }

            model.Id=blog.Id;
            model.Name=blog.Name;

            return Task.FromResult(model);
        }
    }
}