﻿using Microsoft.AspNetCore.Mvc;
using ReviewHubAPI.Models.DTO;
using ReviewHubAPI.Services.Authentication;

namespace ReviewHubAPI.Controllers;

/// <summary>
/// Håndterer HTTP-forespørsler for brukerinnlogging og registrering.
/// Mottar innloggings- og registreringsdata, som deretter blir videreført til AuthService for autentisering.
/// Denne klassen fungerer som et bindeledd mellom brukerens innloggingsforespørsler og autentiseringstjenestene
/// som utfører den faktiske valideringen og token-genereringen.
/// </summary>

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
