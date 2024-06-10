using BlogApp.WebAdmin.Config;
using Microsoft.Extensions.Options;
using RestSharp;

namespace BlogApp.WebAdmin.Services;

public class WebAuthService(IOptions<ClientConfig> config) : WebApiService(config)
{
    public async Task<string?> Authenticate(string username, string password)
    {
        var request = new RestRequest("auth", Method.Post);
        request.AddJsonBody(new { Username = username, Password = password });
        var response = await _client.ExecuteAsync<AuthResponse>(request);
        if (!response.IsSuccessful || response.Data == null) return null;
        return response.Data.Token;
    }

    public async Task<bool> ValidateToken(string token)
    {
        var request = new RestRequest($"auth/{token}");
        var response = await _client.ExecuteAsync<TokenValidationResponse>(request);
        if (!response.IsSuccessful || response.Data == null) return false;
        return response.Data.Valid;
    }

    private record AuthResponse
    {
        public string Token { get; set; } = "";
    }

    private record TokenValidationResponse
    {
        public bool Valid { get; set; }
    }
}
