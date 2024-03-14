using ReviewHubAPI.Models.DTO; 

namespace ReviewHubAPI.Services.Authentication;

public interface IAuthService
{
    Task<UserDTO?> RegisterUserAsync(UserRegistrationDTO newUser);
    Task<string> AuthenticateAsync(LoginDTO loginDto);
    string GenerateJwtToken(UserDTO user);
}
