using board_game_den_server.Middleware;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerUI;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "Board Game Den Api", Version = "v1" });
    c.AddSecurityDefinition("ApiKey", new OpenApiSecurityScheme
    {
        Type = SecuritySchemeType.ApiKey,
        In = ParameterLocation.Header,
        Name = "X-API-Key",
        Description = "Enter your API key in the field below"
    });

    c.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "ApiKey"
                }
            },
            new string[] {}
        }
    });
});

// Project services and allied classes
builder.Services.AddHttpClient<IBoardGameService, BoardGameService>();
builder.Services.AddScoped<IBoardGameService, BoardGameService>();

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAngularApp",
        policy =>
        {
            policy.WithOrigins("http://localhost:4200")
                  .AllowAnyHeader()
                  .AllowAnyMethod();
        });
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "Board Game Den Api");
    c.DocExpansion(DocExpansion.None);
});
}

app.UseHttpsRedirection();

app.UseCors("AllowAngularApp");
app.UseMiddleware<ApiKeyMiddleware>();

app.MapControllers();

app.MapGet("/", () =>
{
    var apiInfo = new
    {
        Name = "Board Game Den Api",
        Environment = app.Environment.EnvironmentName,
        Timestamp = DateTime.UtcNow,
        Version = "1.0.0",
        Status = "Running",
        RequiresApiKey = true,
        ApiKeyHeader = "X-API-Key",
        SwaggerUrl = "/swagger"
    };
    return Results.Ok(apiInfo);
})
.WithName("GetApiInfo")
.WithSummary("Get Api information")
.WithDescription("Returns basic information about the Board Game Den Api: name, environment, and current timestamp");

app.Run();