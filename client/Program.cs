using DGP.Client;
using DGP.Client.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddScoped<IBlogService, BlogService>();

var app = builder.Build();

app.MapGet("/", () => "Hello World!");

app.Run();
