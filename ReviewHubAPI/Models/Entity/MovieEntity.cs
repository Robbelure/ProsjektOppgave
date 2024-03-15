using System.ComponentModel.DataAnnotations;

namespace ReviewHubAPI.Models.Entity
{
    public class MovieEntity
    {
        [Required]
        public int Id { get; set; }
        [Required]
        public string MovieName { get; set; } = string.Empty;
        [Required]
        public int ReleaseYear { get; set; }
        [Required]
        public string Director { get; set; } = string.Empty;
        [Required]
        public string Genre { get; set; } = string.Empty;
        public DateTime DateCreated { get; set; }
        public DateTime DateUpdated { get; set; }
    }
}
