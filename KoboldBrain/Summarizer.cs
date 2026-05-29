using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace KoboldBrain
{
    public record SummaryResult(string Summary, bool Llm, bool PersonalityLoaded, int Lines, int FunctionCount, int Todos, string[] Intruders);

    public static class Summarizer
    {
        public static SummaryResult HeuristicSummary(string code, string personalityText)
        {
            var lines = string.IsNullOrEmpty(code) ? 0 : code.Split('\n').Length;

            var fnPatterns = new[]
            {
                new Regex("\\b(def)\\s+\\w+\\s*\\(|\\bfunction\\s+\\w+\\s*\\(|\\bclass\\s+\\w+\\b", RegexOptions.IgnoreCase),
                new Regex("\\b(?:public|private|protected|internal)\\b.*\\b\\w+\\s*\\([^)]*\\)\\s*\\{", RegexOptions.IgnoreCase)
            };
            var fnCount = 0;
            foreach (var p in fnPatterns) fnCount += p.Matches(code ?? string.Empty).Count;

            var todos = Regex.Matches(code ?? string.Empty, "TODO", RegexOptions.IgnoreCase).Count;

            var intruders = new List<string>();
            if (todos > 0) intruders.Add($"{todos} TODO(s)");
            if (!string.IsNullOrEmpty(code) && (code.Contains("eval(") || code.Contains("Process.Start("))) intruders.Add("potential unsafe calls");
            if (!string.IsNullOrEmpty(code) && (code.Contains(".Result") || code.Contains(".Wait("))) intruders.Add("sync-over-async patterns");

            var sb = new System.Text.StringBuilder();
            sb.AppendLine("Kobold quick summary:");
            sb.AppendLine($"- Lines of code: {lines}");
            sb.AppendLine($"- Rough function/type matches: {fnCount}");
            sb.AppendLine($"- Noted intruders: {(intruders.Count == 0 ? "none obvious" : string.Join(", ", intruders))}");
            sb.AppendLine($"- Suggestions:");
            sb.AppendLine("  1) Add focused unit tests if logic is complex (Snip recommends traps).");
            sb.AppendLine("  2) If you see sync-over-async (.Result/.Wait), prefer async/await (Vex frowns on blocking calls).");
            if (todos > 0) sb.AppendLine("  3) Address TODO comments or add tickets for follow-up.");

            return new SummaryResult(sb.ToString(), false, !string.IsNullOrWhiteSpace(personalityText), lines, fnCount, todos, intruders.ToArray());
        }
    }
}
