using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using System.Text.Json;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddHttpClient();
builder.Services.AddSingleton<IAiEmailService, AiEmailService>();

var app = WebApplication.Create(builder.Build());

app.MapPost("/emails/analyze", async (EmailRequest request, IAiEmailService aiService) =>
{
    var result = await aiService.AnalyzeEmailAsync(request);
    return Results.Ok(result);
});

app.Run();

public record EmailRequest(string Subject, string Body);

public record EmailAnalysisResult(string Category, string SuggestedReply);

public interface IAiEmailService
{
    Task<EmailAnalysisResult> AnalyzeEmailAsync(EmailRequest request);
}

public class AiEmailService : IAiEmailService
{
    private readonly HttpClient _httpClient;

    public AiEmailService(IHttpClientFactory httpClientFactory)
    {
        _httpClient = httpClientFactory.CreateClient();
    }

    public async Task<EmailAnalysisResult> AnalyzeEmailAsync(EmailRequest request)
    {
        // Här skulle du anropa t.ex. OpenAI / Azure OpenAI / annan modell.
        // För kursen kan du börja med en "fejkad" implementation.

        // Enkel “fejk” logik för demo:
        var text = (request.Subject + " " + request.Body).ToLower();

        string category = "övrigt";
        if (text.Contains("pris") || text.Contains("kostnad"))
            category = "fråga";
        else if (text.Contains("missnöjd") || text.Contains("reklamation"))
            category = "klagomål";
        else if (text.Contains("beställa") || text.Contains("order"))
            category = "beställning";

        string suggestedReply = $"Hej! Tack för ditt mejl. Vi har tagit emot ditt ärende i kategorin: {category}. " +
                                "En medarbetare kommer att återkomma med ett mer detaljerat svar så snart som möjligt.";

        // Här kan du senare ersätta med riktigt AI‑anrop.
        await Task.CompletedTask;

        return new EmailAnalysisResult(category, suggestedReply);
    }
}
