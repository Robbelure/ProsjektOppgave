using System.Security.Claims;

namespace ReviewHubAPI.Extensions;

/// <summary>
/// Statisk klasse som inneholder utvidelsesmetoder relatert til identitet og påstander. 
/// Har en GetUserId-metode, som gjør det mulig å hente ut brukerens ID fra claims i et ClaimsPrincipal-objekt.
/// </summary>
public static class IdentityExtensions
{
    private static ILogger? _logger;

    public static void SetLogger(ILogger logger)
    {
        _logger = logger;
    }

    public static int GetUserId(this ClaimsPrincipal user)
    {
        if (user.Identity is ClaimsIdentity identity)
        {
            var userIdClaim = identity.FindFirst(c => c.Type == ClaimTypes.NameIdentifier && int.TryParse(c.Value, out _));
            if (userIdClaim != null && int.TryParse(userIdClaim.Value, out var userId))
            {
                return userId;
            }
            else
            {
                _logger?.LogError($"Invalid user ID format in token: {userIdClaim?.Value}");
                throw new Exception("Invalid user ID format in token");
            }
        }
        throw new Exception("User ID not found in token");
    }
}
