using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerUI;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "Board Game Den Api", Version = "v1" });
});

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

app.MapControllers();

app.MapGet("/", () =>
{
    var apiInfo = new
    {
        Name = "Board Game Den Api",
        Environment = app.Environment.EnvironmentName,
        Timestamp = DateTime.UtcNow,
        Version = "1.0.0",
        Status = "Running"
    };
    return Results.Ok(apiInfo);
})
.WithName("GetApiInfo")
.WithSummary("Get Api information")
.WithDescription("Returns basic information about the Board Game Den Api: name, environment, and current timestamp");

app.Run();