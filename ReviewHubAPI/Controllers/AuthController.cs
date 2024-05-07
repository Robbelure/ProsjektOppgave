using FluentValidation;
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
    private readonly IValidator<UserRegistrationDTO> _validator;

    public AuthController(IAuthService authService, ILogger<AuthController> logger, IValidator<UserRegistrationDTO> validator)
    {
        _authService = authService;
        _logger = logger;
        _validator = validator;
    }

    [HttpPost("register")]
    public async Task<ActionResult<UserRegistrationResponseDTO>> Register([FromBody] UserRegistrationDTO userRegDTO)
    {
        var validationResult = await _validator.ValidateAsync(userRegDTO);

        // Hvis validering feiler, returner valideringsfeil til klienten
        if (!validationResult.IsValid)
        {
            var errors = validationResult.Errors.Select(error => error.ErrorMessage).ToList();
            return BadRequest(new { Errors = errors });
        }

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
