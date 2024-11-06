using Ordering.API;
using Ordering.Application;
using Ordering.Infrastructure;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container
var config = builder.Configuration;

builder.Services
    .AddApplicationServices(config)
    .AddInfrastructureServices(config)
    .AddApiServices(config);


var app = builder.Build();

// Configure the HTTP request pipeline.
app.Run();
