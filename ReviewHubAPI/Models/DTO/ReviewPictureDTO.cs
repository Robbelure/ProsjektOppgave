namespace ReviewHubAPI.Models.DTO;
public class ReviewPictureDTO
{
    public int Id { get; set; }
    public int ReviewId { get; set; }
    public byte[]? ReviewPicture { get; set; }
    public DateTime DateCreated { get; set; }
    public DateTime DateUpdated { get; set; }
}
