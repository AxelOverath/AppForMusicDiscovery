using System.Net.Http;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using MusicDiscoveryApp.ApiCalls;
using System.Text.Json;


public class SpotifyAuthenticator
{
    private const string ClientId = keys.clientId;
    private const string ClientSecret = keys.clientSecret;
    private const string RedirectUri = "http://localhost:8000/callback";
    private const string Scope = "user-read-private user-read-email";

    public async Task<string> GetAccessTokenAsync(string authorizationCode)
    {
        using var client = new HttpClient();

        var tokenUrl = "https://accounts.spotify.com/api/token";
        var tokenData = new FormUrlEncodedContent(new Dictionary<string, string>
        {
            {"grant_type", "authorization_code"},
            {"code", authorizationCode},
            {"redirect_uri", RedirectUri},
            {"client_id", ClientId},
            {"client_secret", ClientSecret}
        });

        var tokenResponse = await client.PostAsync(tokenUrl, tokenData);
        var tokenJson = await tokenResponse.Content.ReadAsStringAsync();
        var token = JsonSerializer.Deserialize<SpotifyToken>(tokenJson);

        return token.AccessToken;
    }

    private class SpotifyToken
    {
        [JsonPropertyName("access_token")]
        public string AccessToken { get; set; }
    }
}
