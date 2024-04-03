using ReviewHubAPI.Models.DTO; 

namespace ReviewHubAPI.Services.Authentication;

public interface IAuthService
{
    Task<UserRegistrationResponseDTO> RegisterUserAsync(UserRegistrationDTO newUser);
    Task<AuthResponseDTO> AuthenticateAsync(LoginDTO loginDto);
    //Task<string> AuthenticateAsync(LoginDTO loginDto);
    string GenerateJwtToken(UserDTO user);
}
