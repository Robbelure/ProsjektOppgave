using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ReviewHubAPI.Models.Entity
{
    public class MoviePoster
    {
        [Key]
        public int Id { get; set; }

        [ForeignKey ("MovieId")]
        public int MovieId { get; set; }

        public byte[]? Poster { get; set; }

        
        public virtual Movie? Movie { get; set; }
    }
}
