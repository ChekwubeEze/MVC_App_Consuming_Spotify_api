using ConsumeSpotifywebApiUsingHttpClient.Models;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace ConsumeSpotifywebApiUsingHttpClient.services
{
    public class SpotifyAccountServices : ISpotifyAccountservices
    {
        private readonly HttpClient _httpClient;

        public SpotifyAccountServices(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }
        public async Task<string> GetToken(string clientId, string clientSecret)
        {
            var request = new HttpRequestMessage(HttpMethod.Post, "token");
            request.Headers.Authorization = new AuthenticationHeaderValue(
                "Basic", Convert.ToBase64String(Encoding.UTF8.GetBytes($"{clientId}:{clientSecret}")));
            request.Content = new FormUrlEncodedContent(new Dictionary<string, string>
            {
                {"grant_type", "client_credentials" }
            });
            var response = await _httpClient.SendAsync(request);
            response.EnsureSuccessStatusCode();
            var responsestream = await response.Content.ReadAsStreamAsync();
            var authResult = await JsonSerializer.DeserializeAsync<AuthResult>(responsestream);
            return authResult.access_token;
        }
    }
}
