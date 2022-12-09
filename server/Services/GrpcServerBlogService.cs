using DGP.Server.Models;
using DGP.Server.Protos;
using Grpc.Core;

namespace DGP.Server.Services
{
    public class GrpcServerBlogService : GrpcBlog.GrpcBlogBase
    {
        private readonly BlogContext _context;

        public GrpcServerBlogService(BlogContext context)
        {
            _context = context;
        }
        public override Task<GrpcGetBlogResponse> GrpcGetBlog(GrpcGetBlogRequest request, ServerCallContext context)
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
    }
}