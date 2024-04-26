using Microsoft.AspNetCore.Mvc;
using ReviewHubAPI.Models.DTO;
using ReviewHubAPI.Services.Authentication;

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
    public async Task<ActionResult<UserRegistrationResponseDTO>> Register([FromBody] UserRegistrationDTO userRegDTO)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        var userResponse = await _authService.RegisterUserAsync(userRegDTO);
        if (userResponse == null)
        {
            _logger.LogWarning("User registration failed.");
            return BadRequest("Unable to register user.");
        }

        return Ok(userResponse);
    }

    [HttpPost("login")]
    public async Task<ActionResult<AuthResponseDTO>> Login([FromBody] LoginDTO loginDto)
    {
        var authResult = await _authService.AuthenticateAsync(loginDto);
        if (authResult == null)
            return Unauthorized("Invalid username or password.");
        return Ok(authResult);
    }
}
