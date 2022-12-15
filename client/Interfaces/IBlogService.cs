using DGP.Client.Protos;

namespace DGP.Client
{
    public interface IBlogService
    {
        public IEnumerable<GrpcGetBlogModel> GetBlogs(GrpcGetBlogRequest request);
        public GrpcGetBlogModel CreateBlog(GrpcGetBlogRequest request);
    }
}