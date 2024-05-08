using FluentValidation;
using Microsoft.IdentityModel.Tokens;
using ReviewHubAPI.Mappers.Interface;
using ReviewHubAPI.Middleware;
using ReviewHubAPI.Models.DTO;
using ReviewHubAPI.Models.Entity;
using ReviewHubAPI.Repositories.Interface;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using ReviewHubAPI.Validators;
//using ValidationException = FluentValidation.ValidationException;

namespace ReviewHubAPI.Services.Authentication;

/// <summary>
/// Håndterer autentisering og registrering av brukere. 
/// Utfører validering av brukerdata, genererer JWT-tokens for gyldige autentiseringer(innlogginger), og registrerer nye brukere.
/// Sikrer at kun gyldige og autentiserte forespørsler får tilgang til systemressurser.
/// </summary>

public class AuthService : IAuthService
{
    private readonly IUserRepository _userRepository;
    private readonly IMapper<User, UserDTO> _userMapper;
    private readonly IMapper<User, UserRegistrationDTO> _userRegMapper;
    private readonly IMapper<User, UserRegistrationResponseDTO> _userRegResponseMapper;
    private readonly IConfiguration _configuration;
    private readonly ILogger<AuthService> _logger;
    private readonly IValidator<UserRegistrationDTO> _registrationValidator;
    private readonly IValidator<LoginDTO> _loginValidator;

    public AuthService(
        IUserRepository userRepository,
        IMapper<User, UserDTO> userMapper,
        IMapper<User, UserRegistrationDTO> userRegMapper,
        IMapper<User, UserRegistrationResponseDTO> userRegResponseMapper,
        IConfiguration configuration,
        ILogger<AuthService> logger,
        IValidator<UserRegistrationDTO> registrationValidator,
        IValidator<LoginDTO> loginValidator)
    {
        _userRepository = userRepository;
        _userMapper = userMapper;
        _userRegMapper = userRegMapper;
        _userRegResponseMapper = userRegResponseMapper;
        _configuration = configuration;
        _logger = logger;
        _registrationValidator = registrationValidator;
        _loginValidator = loginValidator;
    }

    // Registrerer bruker med fluentvalidation
    public async Task<UserRegistrationResponseDTO> RegisterUserAsync(UserRegistrationDTO newUser)
    {
        var validationResult = await _registrationValidator.ValidateAsync(newUser);

        if (!validationResult.IsValid)
            throw new FluentValidation.ValidationException(validationResult.Errors.Select(e => e.ErrorMessage).Aggregate((i, j) => i + "; " + j));

        if (!UserRegistrationDTOValidator.ValidUsername(newUser.Username))
            throw new Middleware.ValidationException("Username contains sensitive words.");

        if (await _userRepository.UsernameExistsAsync(newUser.Username))
            throw new ConflictException("Username is already taken.");

        if (await _userRepository.EmailExistsAsync(newUser.Email))
            throw new EmailConflictException("Email is already in use.");

        var userEntity = _userRegMapper.MapToEntity(newUser);
        await _userRepository.RegisterUserAsync(userEntity);
        return _userRegResponseMapper.MapToDTO(userEntity);
    }

    public async Task<AuthResponseDTO> AuthenticateAsync(LoginDTO loginDto)
    {
        var validationResult = await _loginValidator.ValidateAsync(loginDto);
        if (!validationResult.IsValid)
            throw new FluentValidation.ValidationException(validationResult.Errors.Select(e => e.ErrorMessage).Aggregate((i, j) => i + "; " + j));

        var userEntity = await _userRepository.GetUserByUsernameAsync(loginDto.Username);
        if (userEntity == null || !BCrypt.Net.BCrypt.Verify(loginDto.Password, userEntity.PasswordHash))
            throw new AuthenticationFailedException("Invalid username or password.");

        var userDto = _userMapper.MapToDTO(userEntity);

        // Genererer JWT-token for autentisert bruker
        var token = GenerateJwtToken(userDto);
        return new AuthResponseDTO
        {
            Token = token,
            UserId = userDto.Id,
            Username = userDto.Username,
            Email = userDto.Email
        };
    }

    /// <summary>
    /// Genererer en JWT-token for autentiserte brukere.
    /// </summary>
    /// <param name="user">User-DTO'enen som inneholder brukerdetaljene som trengs for tokenet.</param>
    /// <returns>en JWT-token som en streng, som klienten kan bruke for å autentisere forespørsler.</returns>
    public string GenerateJwtToken(UserDTO user)
    {
        _logger.LogInformation($"Generating JWT token for user: {user.Username}, Id: {user.Id}");

        var jwtKey = _configuration["Jwt:Key"] ?? throw new InvalidOperationException("JWT Key is not configured properly.");
        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtKey));
        var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

        var expiration = DateTime.UtcNow.AddHours(1);
        var claims = new[]
        {
            new Claim(JwtRegisteredClaimNames.Sub, user.Username),
            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
            new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
            new Claim(ClaimTypes.Role, user.IsAdmin ? "Admin" : "User")
        };

        // Logger hver claim som legges til i tokenet
        foreach (var claim in claims)
        {
            _logger.LogInformation($"Claim added to token: {claim.Type} = {claim.Value}");
        }

        var token = new JwtSecurityToken(
            issuer: _configuration["Jwt:Issuer"],
            audience: _configuration["Jwt:Audience"],
            claims: claims,
            expires: expiration,
            signingCredentials: creds);

        _logger.LogInformation($"JWT token generated with claims: {string.Join(", ", claims.Select(c => $"{c.Type}={c.Value}"))}");
        return new JwtSecurityTokenHandler().WriteToken(token);
    }
}
