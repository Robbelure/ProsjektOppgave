using System.ComponentModel.DataAnnotations;

namespace ReviewHubAPI.Models.Entity
{
    public class CommentEntity
    {
        [Required]
        public int Id { get; set; }
        [Required]
        public int UserId { get; set; }
        [Required]
        public int ReviewId { get; set; }
        [Required]
        public string Title { get; set; } = string.Empty;
        [Required]
        public string Comment { get; set; } = string.Empty;
        [Required]
        public DateTime Created { get; set; }
        [Required]
        public DateTime Updated { get; set; }


    }
}
