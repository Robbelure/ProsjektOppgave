namespace ReviewHubAPI.Models.DTO;

public class UserPrivateProfileDTO
{
    public int UserID { get; set; }
    public string Username { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string? Firstname { get; set; }
    public string? Lastname { get; set; }
    public DateTime DateCreated { get; set; }
    public bool IsAdmin { get; set; }
}
