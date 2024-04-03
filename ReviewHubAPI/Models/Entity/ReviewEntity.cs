using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ReviewHubAPI.Models.Entity
{
    public class ReviewEntity
    {
        [Key] 
        public int Id { get; set; }
        [ForeignKey("MovieEntityId")]
        public int MovieId { get; set; }
        [ForeignKey("UserEntity")]
        public int UserId { get; set; }
        [Required]
        public string Title { get; set; }= string.Empty;
        [Required]
        public int Rating {  get; set; }
        [Required]
        public string Text { get; set; } = string.Empty;

        public virtual UserEntity? UserEntity { get; set; }
        public virtual ICollection<CommentEntity>? CommentEntities { get; set; }
        public virtual ReviewPictureEntity? ReviewPictureEntity { get; set; }

    }
}
