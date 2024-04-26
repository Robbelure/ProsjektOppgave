namespace ReviewHubAPI.Models.DTO;

public class AuthResponseDTO
{
    public string Token { get; set; }
    public string Username { get; set; }
    public int UserId { get; set; }
    public string Email { get; set; }
}
