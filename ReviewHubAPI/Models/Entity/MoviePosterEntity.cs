using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ReviewHubAPI.Models.Entity
{
    public class MoviePosterEntity
    {
        [Key]
        public int Id { get; set; }

        [ForeignKey ("Id")]
        public int MovieEntityId { get; set; }

        public byte[]? MoviePoster { get; set; }

        public virtual MovieEntity? MovieEntity { get; set; }
    }
}
