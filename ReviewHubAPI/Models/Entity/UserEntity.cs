using System.ComponentModel.DataAnnotations;

namespace ReviewHubAPI.Models.Entity;

public class UserEntity
{
    [Key]
    public int UserID { get; set; }

    [Required]
    public string Username { get; set; } = string.Empty;

    [Required]
    [EmailAddress]
    public string Email { get; set; } = string.Empty;

    [Required]
    public string PasswordHash { get; set; } = string.Empty;

    public string Firstname { get; set; } = string.Empty;

    public string Lastname { get; set; } = string.Empty;

    public DateTime DateCreated { get; set; } = DateTime.UtcNow;

    public bool IsAdmin { get; set; } = false;

    // Navigasjonsegenskap når vi oppretter ReviewEntity
    // public virtual ICollection<ReviewEntity>? Reviews { get; set; }
}
