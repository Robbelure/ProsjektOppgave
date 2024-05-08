namespace ReviewHubAPI.Models.DTO;

/// <summary>
/// Inneholder nødvendig informasjon for innlogging.
/// Denne klassen brukes til å transportere brukernavn og passord fra klienten til autentiseringssystemet.
/// </summary>
public class LoginDTO
{
    public string Username { get; set; } = string.Empty;
    public string Password { get; set; } = string.Empty;
}
