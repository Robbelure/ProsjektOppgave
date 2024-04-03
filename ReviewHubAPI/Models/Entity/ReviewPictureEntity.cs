using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace ReviewHubAPI.Models.Entity
{
    public class ReviewPictureEntity
    {
        [Key]
        public int Id { get; set; }

        [ForeignKey("ReviewEntity")]
        public int ReviewId { get; set; }

        public byte[]? ReviewPicture { get; set; }

        public virtual ReviewEntity? ReviewEntity { get; set; }
    }
}
