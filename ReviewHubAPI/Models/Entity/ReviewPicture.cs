using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace ReviewHubAPI.Models.Entity;

public class ReviewPicture
{
    [Key]
    public int Id { get; set; }

    [ForeignKey("ReviewId")]
    public int ReviewId { get; set; }

    public byte[]? Picture { get; set; }
    [Required]
    public DateTime DateCreated { get; set; }
    [Required]
    public DateTime DateUpdated { get; set; }

    public virtual Review? Review { get; set; }
}
