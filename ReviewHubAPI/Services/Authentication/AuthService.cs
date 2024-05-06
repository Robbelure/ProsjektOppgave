using Microsoft.IdentityModel.Tokens;
using ReviewHubAPI.Mappers.Interface;
using ReviewHubAPI.Middleware;
using ReviewHubAPI.Models.DTO;
using ReviewHubAPI.Models.Entity;
using ReviewHubAPI.Repositories.Interface;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace ReviewHubAPI.Services.Authentication;

public class AuthService : IAuthService
{
    private readonly IUserRepository _userRepository;
    private readonly IMapper<User, UserDTO> _userMapper;
    private readonly IMapper<User, UserRegistrationDTO> _userRegMapper;
    private readonly IMapper<User, UserRegistrationResponseDTO> _userRegResponseMapper;
    private readonly IConfiguration _configuration;
    private readonly ILogger<AuthService> _logger;

    public AuthService(IUserRepository userRepository,
        IMapper<User, UserDTO> userMapper,
        IMapper<User, UserRegistrationDTO> userRegMapper,
        IMapper<User, UserRegistrationResponseDTO> userRegResponseMapper,
        IConfiguration configuration,
        ILogger<AuthService> logger)
    {
        _userRepository = userRepository;
        _userMapper = userMapper;
        _userRegMapper = userRegMapper;
        _userRegResponseMapper = userRegResponseMapper;
        _configuration = configuration;
        _logger = logger;
    }

    public async Task<UserRegistrationResponseDTO> RegisterUserAsync(UserRegistrationDTO newUser)
    {
        if (await _userRepository.UsernameExistsAsync(newUser.Username))
            throw new ConflictException("Username is already taken.");

        if (await _userRepository.EmailExistsAsync(newUser.Email))
            throw new ConflictException("Email is already in use.");

        var userEntity = _userRegMapper.MapToEntity(newUser);
        await _userRepository.RegisterUserAsync(userEntity);
        return _userRegResponseMapper.MapToDTO(userEntity);
    }

    public async Task<AuthResponseDTO> AuthenticateAsync(LoginDTO loginDto)
    {
        var userEntity = await _userRepository.GetUserByUsernameAsync(loginDto.Username);
        if (userEntity == null || !BCrypt.Net.BCrypt.Verify(loginDto.Password, userEntity.PasswordHash))
        {
            throw new AuthenticationFailedException("Invalid username or password.");
        }

        var userDto = _userMapper.MapToDTO(userEntity);

        // Genererer JWT-token basert på den mappede UserDTO'en
        var token = GenerateJwtToken(userDto);

        // Oppretter og returnerer AuthResponseDTO 
        return new AuthResponseDTO
        {
            Token = token,
            UserId = userDto.Id,
            Username = userDto.Username,
            Email = userDto.Email
        };
    }

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
