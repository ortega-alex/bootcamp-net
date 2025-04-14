using ApiVersionControl;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ApiExplorer;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();

// 1. Add HttpClient to send HttpRequets in controller
builder.Services.AddHttpClient();

// 2. Add versioning control
builder.Services.AddApiVersioning(setup =>
{
    setup.DefaultApiVersion = new ApiVersion(1, 0);
    setup.AssumeDefaultVersionWhenUnspecified = true;
    setup.ReportApiVersions = true;
});

// 3. add confuration to document versions
builder.Services.AddVersionedApiExplorer(setup =>
{
    setup.GroupNameFormat = "'v'VVV"; // 1.0.0, 1.1.0, etc
    setup.SubstituteApiVersionInUrl = true;
});

// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();
builder.Services.AddOpenApi("v2"); // linea adicional para que funciones el ejemplo que fue tomado de una version .net 6 que ya tiene swagger por defecto.
builder.Services.AddSwaggerGen();

// 4. configure options
builder.Services.ConfigureOptions<ConfigureSwaggerOptions>();

var app = builder.Build();

// 5. Configure Endpoints for swagger DOCS for each of the version of our API
var apiVersionDescriptionProvider = app.Services.GetRequiredService<IApiVersionDescriptionProvider>();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    // 6. configure swagger docs
    app.UseSwaggerUI(options =>
    {
        foreach (var description in apiVersionDescriptionProvider.ApiVersionDescriptions)
        {
            options.SwaggerEndpoint(
                // $"/swagger/{description.GroupName}/swagger.json",
                $"/openapi/{description.GroupName}.json",
                description.GroupName.ToUpperInvariant()
            );
        }
    });

    //app.UseSwaggerUI( // url = /swagger
    //  options => options.SwaggerEndpoint("/openapi/v1.json", "Api Version API v1")
    //);
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
