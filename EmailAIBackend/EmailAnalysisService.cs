namespace EmailAIBackend;

using System.Threading.Tasks;

public interface IEmailAnalysisService
{
    Task<EmailResponse> AnalyzeAsync(EmailRequest request);
}

public class EmailAnalysisService : IEmailAnalysisService
{
    public Task<EmailResponse> AnalyzeAsync(EmailRequest request)
    {
        var text = (request.Subject + " " + request.Body).ToLower();
        var category = "övrigt";

        if (text.Contains("pris") || text.Contains("kostnad"))
            category = "fråga";
        else if (text.Contains("missnöjd") || text.Contains("reklamation"))
            category = "klagomål";
        else if (text.Contains("beställa") || text.Contains("order"))
            category = "beställning";

        var reply =
            $"Hej! Tack för ditt mejl. Vi har lagt det i kategorin '{category}'. En medarbetare återkommer snart.";

        return Task.FromResult(new EmailResponse(category, reply));
    }
}
