using System.ComponentModel.DataAnnotations;

namespace ReviewHubAPI.Models.DTO;
public class ReviewDTO
{
    public int Id { get; set; }
    public int MovieId { get; set; }
    public int UserId { get; set; }
    public string Title { get; set; } = string.Empty;                                                    
    public int Rating { get; set; }
    public string Text { get; set; } = string.Empty;
    public DateTime DateCreated { get; set; }
    public DateTime DateUpdated { get; set; }
}
