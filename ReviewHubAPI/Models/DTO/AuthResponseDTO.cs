namespace ReviewHubAPI.Models.DTO;

/// <summary>
/// Inneholder resultatet av en autentiseringsforespørsel(innlogging).
/// Denne klassen brukes til å sende tilbake brukeridentifikasjon og en JWT-token som validerer
/// brukerens autentiserte økt, samt annen relevant brukerinformasjon.
/// </summary>

public class AuthResponseDTO
{
    public string Token { get; set; }
    public string Username { get; set; }
    public int UserId { get; set; }
    public string Email { get; set; }
}
