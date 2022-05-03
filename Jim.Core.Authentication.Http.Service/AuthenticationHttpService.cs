using Jim.Core.Authentication.Models.DTOs;
using System.Net.Http.Json;
using System.Text.Json;

namespace Jim.Core.Authentication.Http.Service
{
    public class AuthenticationHttpService : IAuthenticationHttpService
    {
        private HttpClient _client;

        public AuthenticationHttpService(IHttpClientFactory clientFactory)
        {
            _client = clientFactory?.CreateClient(nameof(IAuthenticationHttpService)) ?? throw new ArgumentNullException(nameof(clientFactory));
        }

        public async Task<TokenLoginResponse> LoginAsync(LoginRequest login)
        {
            using var response = await _client.PostAsync("login", JsonContent.Create(login));

            response.EnsureSuccessStatusCode();
            var stream = await response.Content.ReadAsStreamAsync();
            var deserialised = await JsonSerializer.DeserializeAsync<TokenLoginResponse>(stream);

            return deserialised;
        }
    }
}