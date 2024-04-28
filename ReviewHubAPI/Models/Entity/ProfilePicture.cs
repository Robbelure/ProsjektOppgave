using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ReviewHubAPI.Models.Entity;

public class ProfilePicture
{
    [Key]
    public int Id { get; set; }

    [Required]
    [ForeignKey("UserId")]
    public int UserId { get; set; }

    public byte[]? Picture { get; set; }
    [Required]
    public DateTime DateCreated { get; set; }
    [Required]
    public DateTime DateUpdated { get; set; }

    public virtual User? User { get; set; }
}
