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

    public virtual Review? Review { get; set; }
}
