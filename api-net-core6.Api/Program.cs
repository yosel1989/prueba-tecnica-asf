using api_net_core6.Application.Interfaces;
using api_net_core6.Application.Services;
using api_net_core6.Domain.Interfaces;
using api_net_core6.Infrastructure.Data;
using api_net_core6.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Configurar la cadena de conexión
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// Registrar los servicios de la aplicación
// Configurar la cadena de conexión
builder.Services.AddScoped<IEmpleadoRepository>(provider =>
{
    var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
    return new EmpleadoRepository(connectionString);
});
builder.Services.AddScoped<IEmpleadoService, EmpleadoService>();


// Agregar los controladores
builder.Services.AddControllers();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


// Agregar CORS
builder.Services.AddCors(options =>
{
    options.AddPolicy("OpenPolicy", builder =>
    {
        builder.AllowAnyOrigin()
               .AllowAnyMethod()
               .AllowAnyHeader();
    });
});


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

// Habilitar CORS
app.UseCors("OpenPolicy");

app.UseAuthorization();

app.MapControllers();

app.Run();
