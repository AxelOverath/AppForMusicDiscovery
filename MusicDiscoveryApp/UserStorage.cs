internal class UserStorage
{
    public static string? storedEmail;
    public static string? storedUsername;
    public static string? accessToken;
    public static string? refreshToken;
    public static string[]? friends;
    public static string[]? friendRequests;

    // Usage guide
    // 
    // UserStorage.storedEmail = emailEntry.Text;

    // To get the userstorage into your object.
    // usernameEntry.Text = UserStorage.storedUsername;

    public static void Clear()
    {
        storedEmail = null;
        storedUsername = null; 
        accessToken = null;
        refreshToken = null;
        friends = null;
        friendRequests = null;
    }
}