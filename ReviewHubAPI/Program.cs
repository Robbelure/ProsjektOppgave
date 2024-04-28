using ReviewHubAPI.Extensions;
using ReviewHubAPI.Middleware;
using ReviewHubAPI.Repositories.Interface;
using ReviewHubAPI.Repositories;
using ReviewHubAPI.Services.Interface;
using ReviewHubAPI.Services;
using Serilog;
using Microsoft.EntityFrameworkCore;
using ReviewHubAPI.Data;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using System.Text.Json;
using ReviewHubAPI.Services.Authentication;
using Microsoft.OpenApi.Models;
using FluentValidation;
using FluentValidation.AspNetCore;
using ReviewHubAPI.Validators;
using AspNetCoreRateLimit;
using System.IdentityModel.Tokens.Jwt;

var builder = WebApplication.CreateBuilder(args);
var configuration = builder.Configuration;

#region CORS
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowSpecificOrigin",
        builder =>
        {
            builder.WithOrigins("http://127.0.0.1:5501")
                   .AllowAnyHeader()
                   .AllowAnyMethod();      
        });
});
#endregion

#region JWT-token
// Sjekk om JWT-n�kkelen er konfigurert korrekt
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
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtKey)),
            ClockSkew = TimeSpan.Zero
        };

        options.Events = new JwtBearerEvents
        {
            OnTokenValidated = context =>
            {
                var exp = context.Principal.Claims.FirstOrDefault(c => c.Type == JwtRegisteredClaimNames.Exp)?.Value;
                var expirationTime = DateTimeOffset.FromUnixTimeSeconds(long.Parse(exp)).UtcDateTime;

                
                var logger = context.HttpContext.RequestServices.GetRequiredService<ILogger<Program>>();
                logger.LogInformation($"Token validated for {context.Principal.Identity.Name}, exp: {expirationTime} UTC");

                return Task.CompletedTask;
            },

            OnAuthenticationFailed = context =>
            {
                var logger = context.HttpContext.RequestServices.GetRequiredService<ILogger<Program>>();
                logger.LogError($"Authentication failed: {context.Exception.Message}");
                return Task.CompletedTask;
            },

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
#endregion

#region Serilog
builder.Host.UseSerilog((context, configuration) =>
{
    configuration.ReadFrom.Configuration(context.Configuration);
});
#endregion

#region DI
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IAuthService, AuthService>();
builder.Services.AddScoped<IMovieRepository, MovieRepository>();
builder.Services.AddScoped<IMovieService, MovieService>();
builder.Services.AddScoped<IReviewRespository,ReviewRepository>();
builder.Services.AddScoped<IReviewService, ReviewService>();
builder.Services.AddScoped<ICommentRepository, commentRepository>();
builder.Services.AddScoped<IcommentService, CommentService>();
builder.Services.AddScoped<IReviewPictureRepository, ReviewPictureRepository>();
builder.Services.AddScoped<IReviewPictureService, ReviewPictureService>();
builder.Services.AddScoped<IMoviePosterRepository, MoviePosterRepository>();
builder.Services.AddScoped <IMoviePosterService, MoviePosterService>();
builder.Services.AddScoped <IUploadProfilePictureRepository, UploadProfilePictureRepository>();
builder.Services.AddScoped <IUploadProfilePictureService, UploadProfilePictureService>();
#endregion

#region FluentValidation Registrering
builder.Services.AddControllers();
builder.Services.AddFluentValidationAutoValidation().AddFluentValidationClientsideAdapters();
builder.Services.AddValidatorsFromAssemblyContaining<UserRegistrationDTOValidator>();
builder.Services.AddValidatorsFromAssemblyContaining<LoginDTOValidator>();
#endregion

#region Rate-limiting
builder.Services.AddMemoryCache();
builder.Services.Configure<IpRateLimitOptions>(builder.Configuration.GetSection("IpRateLimiting"));

builder.Services.AddSingleton<IRateLimitCounterStore, MemoryCacheRateLimitCounterStore>();
builder.Services.AddSingleton<IRateLimitConfiguration, RateLimitConfiguration>();
builder.Services.AddSingleton<IProcessingStrategy, AsyncKeyLockProcessingStrategy>();
builder.Services.AddSingleton<IIpPolicyStore, MemoryCacheIpPolicyStore>();
#endregion

#region Middleware, Extensions
builder.Services.AddTransient<GlobalExceptionMiddleware>();
builder.RegisterMappers();
#endregion

#region EF Database Config
builder.Services.AddDbContext<ReviewHubDbContext>(options =>
    options.UseMySql(builder.Configuration.GetConnectionString("DefaultConnection"),
        new MySqlServerVersion(new Version(8, 0))));
#endregion

builder.Services.AddEndpointsApiExplorer();

#region Swagger Configuration
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "ReviewHubAPI", Version = "v1" });

   // c.OperationFilter<JsonPatchDocumentFilter>();

    // Legger til JWT-st�tte i Swagger
    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Name = "Authorization",
        Type = SecuritySchemeType.Http,
        Scheme = "Bearer",
        BearerFormat = "JWT",
        In = ParameterLocation.Header,
        Description = "JWT Authorization header using the Bearer scheme."
    });

    c.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer"
                }
            },
            Array.Empty<string>()
        }
    });
});
#endregion


var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseSerilogRequestLogging();
app.UseStaticFiles();
app.UseMiddleware<GlobalExceptionMiddleware>();

app.UseHttpsRedirection();

app.UseCors("AllowSpecificOrigin");

app.UseAuthentication();
app.UseAuthorization();

app.UseIpRateLimiting();
app.UseMiddleware<RateLimitResponseMiddleware>();

app.MapControllers();

app.Run();
