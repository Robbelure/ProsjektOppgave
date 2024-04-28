using System.ComponentModel.DataAnnotations;

namespace ReviewHubAPI.Models.DTO;

public class ProfilePictureDTO
{
    public int Id { get; set; }
    public int UserId { get; set; }
    public byte[]? ProfilePicture { get; set; }
    public DateTime DateCreated { get; set; }
    public DateTime DateUpdated { get; set; }
}
