using MongoDB.Bson;

namespace MusicDiscoveryApp;

public class User
{
    public ObjectId Id;
    public string? Email;
    public string? Password;
    public string? FirstName;
    public string? LastName;
    public string? Username;
    public DateTime DateOfBirth;
    public string? AccessToken;
    public string? RefreshToken;
    public List<string>? Friends { get; set; }
    public List<string>? FriendRequests { get; set; }
    public List<string>? LikedSongs { get; set; }

    public List<string>? DislikedSongs { get; set; }
}