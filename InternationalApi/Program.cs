var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

// 1. LOCALIZATION
builder.Services.AddLocalization(options => options.ResourcesPath = "Resources");

builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// 2. SUPPORTED CULTURES
var supportedCultures = new[] { "en-US", "es-ES", "fr-FR" }; // USA english, Spain Spanish and France french
var localizationOptions = new RequestLocalizationOptions()
    .SetDefaultCulture(supportedCultures[0]) // English by default
    .AddSupportedCultures(supportedCultures) // Add al supported cultures
    .AddSupportedUICultures(supportedCultures); // Add supported cultures to UI

// 3. ADD LOCALIZATION to App
app.UseRequestLocalization(localizationOptions);

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.UseSwagger();
    app.UseSwaggerUI( // url = /swagger
        options => options.SwaggerEndpoint("/openapi/v1.json", "International API v1")
    );
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
