namespace ReviewHubAPI.Models.DTO;

public class UserPublicProfileDTO
{
    public string Username { get; set; } = string.Empty;
    public byte[]? ProfilePicture { get; set; }
    public string? Firstname { get; set; }
    public string? Lastname { get; set; }
}
