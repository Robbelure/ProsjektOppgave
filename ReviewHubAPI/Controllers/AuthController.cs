using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ReviewHubAPI.Models.DTO;
using ReviewHubAPI.Services.Authentication;
using ReviewHubAPI.Services.Interface;

namespace ReviewHubAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class AuthController : ControllerBase
{
    private readonly IAuthService _authService;
    private readonly ILogger<AuthController> _logger;

    public AuthController(IAuthService authService, ILogger<AuthController> logger)
    {
        _authService = authService;
        _logger = logger;
    }

    [HttpPost("register")]
    public async Task<ActionResult<UserDTO>> Register([FromBody] UserRegistrationDTO userRegDTO)
    {
        var user = await _authService.RegisterUserAsync(userRegDTO);
        return Ok(user);
    }

    [HttpPost("login")]
    public async Task<ActionResult<string>> Login([FromBody] LoginDTO loginDto)
    {
        var token = await _authService.AuthenticateAsync(loginDto);

        if (string.IsNullOrEmpty(token))
        {
            return Unauthorized("Invalid username or password.");
        }

        return Ok(new { token });
    }
}
