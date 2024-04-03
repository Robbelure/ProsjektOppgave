using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ReviewHubAPI.Models.Entity
{
    public class CommentEntity
    {
        [Key]
        public int Id { get; set; }

        [ForeignKey("UserID")]
        public int UserId { get; set; }
        [ForeignKey("Id")]
        public int ReviewId { get; set; }
        [Required]
        public string Title { get; set; } = string.Empty;
        [Required]
        public string Comment { get; set; } = string.Empty;
        [Required]
        public DateTime Created { get; set; }
        [Required]
        public DateTime Updated { get; set; }


        public virtual UserEntity? UserEntity { get; set; }
        public virtual ReviewEntity? ReviewEntity { get; set; }

    }
}
