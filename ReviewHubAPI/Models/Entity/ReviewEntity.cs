using System.ComponentModel.DataAnnotations;

namespace ReviewHubAPI.Models.Entity
{
    public class ReviewEntity
    {
        [Key] 
        public int Id { get; set; }
        [Required]
        public int MovieId { get; set; }
        [Required]
        public int Userid { get; set; }
        [Required]
        public string Title { get; set; }= string.Empty;
        [Required]
        public int Rating {  get; set; }
        [Required]
        public string Text { get; set; } = string.Empty;

        [Required]
        public byte[]? MoviePicture { get; set; }

    }
}
