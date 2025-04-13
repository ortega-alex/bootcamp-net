using Microsoft.EntityFrameworkCore;
using Scalar.AspNetCore;
using UniversitiApiBackend.DataAccess;
using UniversitiApiBackend.Services;

var builder = WebApplication.CreateBuilder(args);

// TODO: connction with database
// 1. nombre de la conexion
const string CONNECTIONNAME = "UniversitiDB";
// 2/ estring de conexion con sql server
var connectionString = builder.Configuration.GetConnectionString(CONNECTIONNAME);
// 3. establecer contexto
builder.Services.AddDbContext<UniversityDBContext>(options => options.UseSqlServer(connectionString));

// 7. add service jwt autorization
// builder.Services.AddJwtTokenServices(builder.Configuration);

// Add services to the container.

builder.Services.AddControllers();
// 4. Add Custom services (folder services)
builder.Services.AddScoped<IStudentsService, StudentsService>(); // permite injectar a los controladores, IStudentsService es la interface y StudentsService es la implementacion
// Todo: add the rest of services

// 8. TODO: config swagget to take care of autorization of jwt (open api)

// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

// 5. cors configuration
builder.Services.AddCors(options =>
{
    options.AddPolicy(name: "CorsPolicy", builder =>
    {
        builder.AllowAnyOrigin(); // allow any origin
        builder.AllowAnyMethod(); // allow any method [GET, POST, PUT, PATCH, ETC]
        builder.AllowAnyHeader(); // allow any header
    });
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.MapScalarApiReference();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

// 6. Tell app to use cors
app.UseCors("CorsPolicy");

app.Run();
