internal class UserStorage
{
    public static string? storedEmail;
    public static string? storedUsername;
    public static string? accessToken;
    public static string? refreshToken;
    public static string[]? friends;
    public static string[]? friendRequests;
    public static string[]? likedSongs;
    public static string[]? dislikedSongs;

    public static void Clear()
    {
        storedEmail = null;
        storedUsername = null; 
        accessToken = null;
        refreshToken = null;
        friends = null;
        friendRequests = null;
        likedSongs = null;
        dislikedSongs = null;
    }
}