namespace ReviewHubAPI.Models.DTO;

public class CommentDTO
{
    public int Id { get; set; }       
    public int UserId { get; set; }  
    public int ReviewId { get; set; } 
    public string Title { get; set; } = string.Empty; 
    public string Comment { get; set; } = string.Empty; 
    public DateTime Created { get; set; } 
    public DateTime Updated { get; set; }
}
