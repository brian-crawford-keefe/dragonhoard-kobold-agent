using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace DragonHoard.VSIX
{
    internal static class KoboldBrainClient
    {
        private static readonly HttpClient _http = new HttpClient();
        private const string SummarizeUrl = "http://localhost:5077/summarize";

        public static async Task<string> SummarizeAsync(string code)
        {
            return await SummarizeAsync(code, CancellationToken.None).ConfigureAwait(false);
        }

        public static async Task<string> SummarizeAsync(string code, CancellationToken cancellationToken)
        {
            var content = new StringContent(code, Encoding.UTF8, "text/plain");
            var response = await _http.PostAsync(SummarizeUrl, content, cancellationToken).ConfigureAwait(false);
            response.EnsureSuccessStatusCode();
            var json = await response.Content.ReadAsStringAsync(cancellationToken).ConfigureAwait(false);
            // naive extraction for demo
            var summary = System.Text.Json.JsonDocument.Parse(json).RootElement.GetProperty("summary").GetString();
            return summary;
        }
    }
}
