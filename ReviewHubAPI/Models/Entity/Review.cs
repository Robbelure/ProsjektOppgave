using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ReviewHubAPI.Models.Entity
{
    public class Review
    {
        [Key] 
        public int Id { get; set; }
        [ForeignKey("MovieId")]
        public int MovieId { get; set; }
        [ForeignKey("UserId")]
        public int UserId { get; set; }
        [Required]
        public string Title { get; set; }= string.Empty;
        [Required]
        public int Rating {  get; set; }
        [Required]
        public string Text { get; set; } = string.Empty;

        public virtual User? UserEntity { get; set; }
        public virtual ICollection<Comment>? Comment { get; set; }

    }
}
