using Microsoft.EntityFrameworkCore;
using StatTracker;
using StatTracker.DbContexts;
using StatTracker.Extensions;
using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Http.Json;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointDefinitions(typeof(AssemblyMarker));

builder.Services.Configure<JsonOptions>(options =>
{
    options.SerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
});

builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(policy => policy
        .AllowAnyHeader()
        .AllowAnyMethod()
        .AllowAnyOrigin());
});

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<StatTrackerDbContext>(options =>
{
    var serverVersion = new MySqlServerVersion(new Version(8, 0, 31));
    options.UseMySql("Server=localhost;Database=StatTrackerDB; Uid=root; Pwd=MTNspark33!;", serverVersion);
});

var app = builder.Build();

app.UseCors();

app.UseEndpointDefinitions();

app.UseSwagger();
app.UseSwaggerUI();

app.Run();