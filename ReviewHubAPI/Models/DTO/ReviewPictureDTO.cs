using System.ComponentModel.DataAnnotations.Schema;

namespace ReviewHubAPI.Models.DTO
{
    public class ReviewPictureDTO
    {
        public int Id { get; set; }

        public int ReviewId { get; set; }

        public byte[]? ReviewPicture { get; set; }
    }
}
