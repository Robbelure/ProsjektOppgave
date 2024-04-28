
using System.ComponentModel.DataAnnotations;

namespace ReviewHubAPI.Models.DTO;

public class MoviePosterDTO
{
    public int Id { get; set; }
    public int MovieId { get; set; }
    public byte[]? MoviePoster { get; set; }
    public DateTime DateCreated { get; set; }
    public DateTime DateUpdated { get; set; }
}
