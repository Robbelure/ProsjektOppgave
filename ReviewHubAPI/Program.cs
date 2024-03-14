using ReviewHubAPI.Extensions;
using ReviewHubAPI.Middleware;
using ReviewHubAPI.Repositories.Interface;
using ReviewHubAPI.Repositories;
using ReviewHubAPI.Services.Interface;
using ReviewHubAPI.Services;
using Serilog;
using ReviewHubAPI.Mappers.Interface;
using ReviewHubAPI.Mappers;
using ReviewHubAPI.Models.DTO;
using ReviewHubAPI.Models.Entity;
using Microsoft.EntityFrameworkCore;
using ReviewHubAPI.Data;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using System.Text.Json;
using ReviewHubAPI.Services.Authentication;

var builder = WebApplication.CreateBuilder(args);
var configuration = builder.Configuration;

// Sjekk om JWT-nøkkelen er konfigurert korrekt
var jwtKey = configuration["Jwt:Key"] ?? throw new InvalidOperationException("JWT Key is not configured properly.");
var jwtIssuer = configuration["Jwt:Issuer"];
var jwtAudience = configuration["Jwt:Audience"];

// JWT autentisering
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = jwtIssuer,
            ValidAudience = jwtAudience,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtKey))
        };

        options.Events = new JwtBearerEvents
        {
            OnChallenge = async context =>
            {
                // Logger tilfellet
                var logger = context.HttpContext.RequestServices.GetRequiredService<ILogger<Program>>();
                logger.LogWarning("Access denied. Invalid or missing JWT-token.");

                // Tilpasser feilmeldingen
                context.Response.StatusCode = StatusCodes.Status401Unauthorized;
                context.Response.ContentType = "application/json";
                await context.Response.WriteAsync(JsonSerializer.Serialize(new
                {
                    error = "Access denied.",
                    message = "Invalid or missing JWT-token."
                }));

                context.HandleResponse();
            },
            OnForbidden = async context =>
            {
                context.Response.StatusCode = 403;
                context.Response.ContentType = "application/json";
                var result = JsonSerializer.Serialize(new { message = "Access denied. You do not have permission to view this resource." });
                await context.Response.WriteAsync(result);
            }
        };
    });

// serilog
builder.Host.UseSerilog((context, configuration) =>
{
    configuration.ReadFrom.Configuration(context.Configuration);
});

// Dependency Injection Reg
builder.Services.AddScoped<IUserRepository, UserRepository>();

builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IAuthService, AuthService>();

builder.Services.AddScoped<IMapper<UserEntity, UserDTO>, UserMapper>();
builder.Services.AddScoped<IMapper<UserEntity, UserRegistrationDTO>, UserRegistrationMapper>();

// Middleware, Extensions
builder.Services.AddTransient<GlobalExceptionMiddleware>();
builder.RegisterMappers();

// EF Database Config
builder.Services.AddDbContext<ReviewHubDbContext>(options =>
    options.UseMySql(builder.Configuration.GetConnectionString("DefaultConnection"),
        new MySqlServerVersion(new Version(8, 0))));


builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseSerilogRequestLogging();
app.UseMiddleware<GlobalExceptionMiddleware>();

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
