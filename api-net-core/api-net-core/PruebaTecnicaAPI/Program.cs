using IlustraApp.Infrastructure;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);
var configuration = builder.Configuration;

builder.Services.AddInfrastructure(configuration);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c => new OpenApiInfo { Title = "Prueba Técnica", Version = "v1" });

// cors
builder.Services.AddCors(options => options
                    .AddPolicy("AllowAll", p => p.SetIsOriginAllowed(isOriginAllowed: _ => true)
                     .AllowAnyMethod()
                     .AllowAnyHeader()
                     .AllowCredentials()));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors("AllowAll");

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
