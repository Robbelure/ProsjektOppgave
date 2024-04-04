using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ReviewHubAPI.Models.Entity
{
    public class Comment
    {
        [Key]
        public int Id { get; set; }

        [ForeignKey("UserId")]
        public int UserId { get; set; }
        [ForeignKey("ReviewId")]
        public int ReviewId { get; set; }
        [Required]
        public string Title { get; set; } = string.Empty;
        [Required]
        public string CommentText { get; set; } = string.Empty;
        [Required]
        public DateTime Created { get; set; }
        [Required]
        public DateTime Updated { get; set; }


        public virtual User? User { get; set; }
        public virtual Review? Review { get; set; }

    }
}
