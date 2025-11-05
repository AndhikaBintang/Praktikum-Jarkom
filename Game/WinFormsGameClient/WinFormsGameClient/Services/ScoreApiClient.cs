using System.Net.Http;
using System.Net.Http.Json;
using WinFormsGameClient.Models;

namespace WinFormsGameClient.Services
{
    public class ScoreApiClient
    {
        private readonly HttpClient _http;

        // Ganti BASE_URL sesuai port yang muncul saat API-mu running
        private const string BASE_URL = "http://localhost:5080"; // contoh

        public ScoreApiClient(HttpClient? http = null)
        {
            _http = http ?? new HttpClient
            {
                BaseAddress = new Uri(BASE_URL),
                Timeout = TimeSpan.FromSeconds(10)
            };
        }

        public async Task<List<PlayerScore>> GetTopAsync(int n = 10, CancellationToken ct = default)
        {
            // GET /api/scores/top/{n}
            var res = await _http.GetAsync($"/api/scores/top/{n}", ct);
            res.EnsureSuccessStatusCode();
            var data = await res.Content.ReadFromJsonAsync<List<PlayerScore>>(cancellationToken: ct);
            return data ?? new List<PlayerScore>();
        }

        public async Task SubmitAsync(string playerName, int score, CancellationToken ct = default)
        {
            // POST /api/scores
            var payload = new { playerName, score };
            var res = await _http.PostAsJsonAsync("/api/scores", payload, ct);
            res.EnsureSuccessStatusCode();
        }
    }
}
