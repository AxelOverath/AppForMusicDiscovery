using MongoDB.Driver;
using MusicDiscoveryApp.Models;

namespace MusicDiscoveryApp.Services;

public class SpotifyService : ISpotifyService
{
    private readonly ISecureStorageService secureStorageService;
    private string accessToken;
    private string refreshToken;

    public SpotifyService(ISecureStorageService secureStorageService)
    {
        this.secureStorageService = secureStorageService;
    }

    public async Task<bool> Initialize(string authCode)
    {
        var bytes = Encoding.UTF8.GetBytes($"{Constants.SpotifyClientId}:{Constants.SpotifyClientsecret}");
        var authHeader = Convert.ToBase64String(bytes);

        var client = new HttpClient();
        client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Basic", authHeader);


        var content = new FormUrlEncodedContent(new List<KeyValuePair<string, string>>()
        {
            new( "code", authCode),
            new("redirect_uri", Constants.RedirectUrl),
            new("grant_type","authorization_code")
        });
        var response = await client.PostAsync("https://accounts.spotify.com/api/token", content);

        var json = await response.Content.ReadAsStringAsync();
        var result = JsonSerializer.Deserialize<AuthResult>(json);

        accessToken = result.AccessToken;

        if (!string.IsNullOrWhiteSpace(result.RefreshToken))
        {
            refreshToken = result.RefreshToken;
        }

        await secureStorageService.Save(nameof(result.AccessToken), result.AccessToken);
        await secureStorageService.Save(nameof(result.RefreshToken), result.RefreshToken);

        IMongoCollection<User> usersCollection = Database.UsersCollection;

        var filter = Builders<User>.Filter.Eq(u => u.Email, UserStorage.storedEmail);

        var update = Builders<User>.Update
            .Set(u => u.AccessToken, accessToken)
            .Set(u => u.RefreshToken, refreshToken);

        var updateResult = await usersCollection.UpdateOneAsync(filter, update);

        // Update successful
        UserStorage.accessToken = accessToken;
        UserStorage.refreshToken = refreshToken;

        return true;
    }

}