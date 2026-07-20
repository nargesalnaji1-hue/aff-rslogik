using EmailAIBackend;

using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

var builder = WebApplication.CreateBuilder(args);

// Registrera tjänster
builder.Services.AddSingleton<IEmailAnalysisService, EmailAnalysisService>();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

app.MapPost("/analyze", async (EmailRequest request, IEmailAnalysisService service) =>
{
    var result = await service.AnalyzeAsync(request);
    return Results.Ok(result);
});

app.Run();
