# aff-rslogik
# Email AI Backend

En enkel .NET-backend som analyserar kundmejl åt ett litet företag.

## Funktion

- Tar emot mejl (subject + body)
- Sätter en kategori (t.ex. fråga, klagomål, beställning)
- Skapar ett enkelt svarsförslag som personalen kan godkänna

## Teknik

- .NET 8 minimal API
- C#
- (Plats för AI-anrop, t.ex. OpenAI eller annan modell)

## Köra lokalt

1. Klona repot
2. Kör `dotnet run`
3. Skicka en POST mot `https://localhost:5001/emails/analyze` med JSON:

{
  "subject": "Fråga om pris",
  "body": "Hej, vad kostar er tjänst?"
}

4. Du får tillbaka kategori + svarsförslag.
