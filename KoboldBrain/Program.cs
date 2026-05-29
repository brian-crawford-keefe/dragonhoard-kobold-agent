using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Hosting;
using System.Text.Json;
using System.Text;
using System.Text.RegularExpressions;
using System.Net.Http;

var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

string LoadPersonality()
{
    // Try a few likely relative paths for the personality file
    var candidates = new[]
    {
        Path.Combine("src", "personality", "kobold-personality.md"),
        Path.Combine("..", "src", "personality", "kobold-personality.md"),
        Path.Combine("..", "..", "src", "personality", "kobold-personality.md"),
        "kobold-personality.md"
    };

    foreach (var p in candidates)
    {
        if (File.Exists(p))
        {
            try { return File.ReadAllText(p, Encoding.UTF8); } catch { }
        }
    }

    return string.Empty;
}

var personalityText = LoadPersonality();
var http = new HttpClient();
var llmEndpoint = Environment.GetEnvironmentVariable("KOBOLD_LLM_ENDPOINT");

app.MapPost("/summarize", async (HttpContext context) =>
{
    using var reader = new StreamReader(context.Request.Body);
    var code = await reader.ReadToEndAsync();

    // If an external LLM endpoint is configured, forward a composed prompt to it
    if (!string.IsNullOrWhiteSpace(llmEndpoint))
    {
        var promptBuilder = new StringBuilder();
        if (!string.IsNullOrWhiteSpace(personalityText))
        {
            promptBuilder.AppendLine("<PERSONALITY>");
            promptBuilder.AppendLine(personalityText);
            promptBuilder.AppendLine("</PERSONALITY>");
        }
        promptBuilder.AppendLine("<CODE>");
        promptBuilder.AppendLine(code);
        promptBuilder.AppendLine("</CODE>");
        promptBuilder.AppendLine("\nProvide a concise, technical summary of the code focusing on responsibilities, likely bugs (intruders), TODOs, and suggestions for improvements. Keep the summary factual and actionable.");

        var payload = JsonSerializer.Serialize(new { prompt = promptBuilder.ToString() });
        var response = await http.PostAsync(llmEndpoint, new StringContent(payload, Encoding.UTF8, "application/json"));
        if (response.IsSuccessStatusCode)
        {
            var text = await response.Content.ReadAsStringAsync();
            // Return raw LLM output as summary; caller may parse if needed
            await context.Response.WriteAsJsonAsync(new { summary = text, llm = true });
            return;
        }
        else
        {
            // Fall through to heuristic summary if LLM call fails
        }
    }

    // Heuristic local summary (no external LLM)
    var lines = string.IsNullOrEmpty(code) ? 0 : code.Split('\n').Length;
    // crude multi-language function/method detection
    var fnPatterns = new[]
    {
        new Regex(@"\b(def)\s+\w+\s*\(|\bfunction\s+\w+\s*\(|\bclass\s+\w+\b", RegexOptions.IgnoreCase),
        new Regex(@"\b(?:public|private|protected|internal)\b.*\b\w+\s*\([^)]*\)\s*\{", RegexOptions.IgnoreCase)
    };
    var fnCount = 0;
    foreach (var p in fnPatterns) fnCount += p.Matches(code).Count;
    var todos = Regex.Matches(code ?? string.Empty, "TODO", RegexOptions.IgnoreCase).Count;

    var intruders = new List<string>();
    if (todos > 0) intruders.Add($"{todos} TODO(s)");
    if (code.Contains("eval(") || code.Contains("Process.Start(")) intruders.Add("potential unsafe calls");
    if (code.Contains(".Result") || code.Contains(".Wait(")) intruders.Add("sync-over-async patterns");

    var sb = new StringBuilder();
    sb.AppendLine("Kobold quick summary:");
    sb.AppendLine($"- Lines of code: {lines}");
    sb.AppendLine($"- Rough function/type matches: {fnCount}");
    sb.AppendLine($"- Noted intruders: {(intruders.Count == 0 ? "none obvious" : string.Join(", ", intruders))}");
    sb.AppendLine($"- Suggestions:");
    sb.AppendLine("  1) Add focused unit tests if logic is complex (Snip recommends traps).");
    sb.AppendLine("  2) If you see sync-over-async (.Result/.Wait), prefer async/await (Vex frowns on blocking calls).");
    if (todos > 0) sb.AppendLine("  3) Address TODO comments or add tickets for follow-up.");

    var result = new { summary = sb.ToString(), llm = false, personalityLoaded = !string.IsNullOrWhiteSpace(personalityText) };
    await context.Response.WriteAsJsonAsync(result);
});

app.Run();
