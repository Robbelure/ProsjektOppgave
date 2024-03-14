namespace ReviewHubAPI.Utilities;

public static class PasswordHelper
{
    public static string HashPassword(string password)
    {
        return BCrypt.Net.BCrypt.HashPassword(password);
    }
}
