using DGP.Client;
using DGP.Client.Protos;
using DGP.Client.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddScoped<IBlogService, BlogService>();

var app = builder.Build();

app.MapGet("/blogs", (IBlogService blogService) => {
    GrpcGetBlogRequest request = new GrpcGetBlogRequest();
    
    return blogService.GetBlogs(request);;
});

app.MapPost("/blogs", (GrpcGetBlogRequest request, IBlogService blogService) => {
    return blogService.CreateBlog(request);
});

app.Run();
