using Microsoft.EntityFrameworkCore;
using Scalar.AspNetCore;
using UniversitiApiBackend.DataAccess;

var builder = WebApplication.CreateBuilder(args);

// TODO: connction with database
// 1. nombre de la conexion
const string CONNECTIONNAME = "UniversitiDB";
// 2/ estring de conexion con sql server
var connectionString = builder.Configuration.GetConnectionString(CONNECTIONNAME);
// 3. establecer contexto
builder.Services.AddDbContext<UniversityDBContext>(options => options.UseSqlServer(connectionString));

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

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

app.Run();
