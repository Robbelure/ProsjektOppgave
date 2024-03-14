using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ReviewHubAPI.Models.Entity;

public class UserEntity
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int UserID { get; set; }

    [Required]
    public string Username { get; set; } = string.Empty;

    // Tenkte vi kunne legge til profil bilde også
    [Required]
    public byte[]? ProfilePicture { get; set; } 

    [Required]
    [EmailAddress]
    public string Email { get; set; } = string.Empty;

    [Required]
    public string PasswordHash { get; set; } = string.Empty;

    [Required]
    public string Firstname { get; set; } = string.Empty;

    [Required]
    public string Lastname { get; set; } = string.Empty;

    [Required]
    public DateTime DateCreated { get; set; } = DateTime.UtcNow;

    [Required]
    public bool IsAdmin { get; set; } = false;

    // Navigasjonsegenskap når vi oppretter ReviewEntity
    // public virtual ICollection<ReviewEntity>? Reviews { get; set; }
}
