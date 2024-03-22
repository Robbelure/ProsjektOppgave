﻿using Microsoft.IdentityModel.Tokens;
using ReviewHubAPI.Mappers.Interface;
using ReviewHubAPI.Middleware;
using ReviewHubAPI.Models.DTO;
using ReviewHubAPI.Models.Entity;
using ReviewHubAPI.Repositories.Interface;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace ReviewHubAPI.Services.Authentication
{
    public class AuthService : IAuthService
    {
        private readonly IUserRepository _userRepository;
        private readonly IMapper<UserEntity, UserDTO> _userMapper;
        private readonly IMapper<UserEntity, UserRegistrationDTO> _userRegMapper;
        private readonly IMapper<UserEntity, UserRegistrationResponseDTO> _userRegResponseMapper;
        private readonly IConfiguration _configuration;
        private readonly ILogger<AuthService> _logger;

        public AuthService(IUserRepository userRepository,
            IMapper<UserEntity, UserDTO> userMapper,
            IMapper<UserEntity, UserRegistrationDTO> userRegMapper,
            IMapper<UserEntity, UserRegistrationResponseDTO> userRegResponseMapper,
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

        public async Task<string> AuthenticateAsync(LoginDTO loginDto)
        {
            var userEntity = await _userRepository.GetUserByUsernameAsync(loginDto.Username);
            if (userEntity == null || !BCrypt.Net.BCrypt.Verify(loginDto.Password, userEntity.PasswordHash))
                throw new AuthenticationFailedException("Invalid username or password.");

            var userDto = _userMapper.MapToDTO(userEntity);
            return GenerateJwtToken(userDto);
        }

        public string GenerateJwtToken(UserDTO user)
        {
            _logger.LogInformation($"Generating JWT token for user: {user.Username}, UserID: {user.UserID}");

            var jwtKey = _configuration["Jwt:Key"] ?? throw new InvalidOperationException("JWT Key is not configured properly.");
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtKey));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, user.Username),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(ClaimTypes.NameIdentifier, user.UserID.ToString()),
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
                expires: DateTime.Now.AddHours(1),
                signingCredentials: creds);

            _logger.LogInformation($"JWT token generated with claims: {string.Join(", ", claims.Select(c => $"{c.Type}={c.Value}"))}");
            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
