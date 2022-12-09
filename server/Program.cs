using DGP.Server.Models;
using DGP.Server.Services;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContextPool<BlogContext>(options => {
    options.UseInMemoryDatabase("BLOG_DB");
});
builder.Services.AddGrpc();

var app = builder.Build();

app.MapGet("/", () => "Hello World!");

app.MapGrpcService<GrpcServerBlogService>();

app.Run();
