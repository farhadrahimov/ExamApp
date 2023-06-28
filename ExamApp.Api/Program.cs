using ExamApp.Api.Infrastructure;
using ExamApp.Repository.Infrastructure;
using ExamApp.Service.Infrastructure;

var builder = WebApplication.CreateBuilder(args);
var services = builder.Services;
var configuration = builder.Configuration;

services.AddProjectDependencies();

// Add services to the container.
var app = builder.Build();

// Configure the HTTP request pipeline.
app.AddPipeline(app);

app.Run();
