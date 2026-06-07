using CombinationGeneratorAPI.Api.Endpoints;
using CombinationGeneratorAPI.Application.Interfaces;
using CombinationGeneratorAPI.Application.Mapping;
using CombinationGeneratorAPI.Infrastructure.Services;
using NSwag.AspNetCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddOpenApiDocument();
builder.Services.AddMemoryCache();
builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(
    typeof(CombinationGeneratorAPI.Application.Queries.GetCombinationsQuery).Assembly));
builder.Services.AddAutoMapper(cfg => cfg.AddProfile<CombinationMappingProfile>());
builder.Services.AddScoped<ICombinationService, CombinationService>();
builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(policy =>
        policy.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());
});

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseOpenApi();
    app.UseSwaggerUi();
}

app.UseCors();
app.MapCombinationEndpoints();

app.Run();
