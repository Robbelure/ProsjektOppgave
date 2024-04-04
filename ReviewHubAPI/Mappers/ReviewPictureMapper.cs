using ReviewHubAPI.Mappers.Interface;
using ReviewHubAPI.Models.DTO;
using ReviewHubAPI.Models.Entity;

namespace ReviewHubAPI.Mappers
{
    public class ReviewPictureMapper : IMapper<ReviewPicture, ReviewPictureDTO>
    {
        public ReviewPictureDTO MapToDTO(ReviewPicture entity)
        {
            return new ReviewPictureDTO
            {
                Id = entity.Id,
                ReviewId = entity.ReviewId,
                ReviewPicture = entity.Picture
            };
        }

        public ReviewPicture MapToEntity(ReviewPictureDTO dto)
        {
            return new ReviewPicture
            {
                Id = dto.Id,
                ReviewId = dto.ReviewId,
                Picture = dto.ReviewPicture
            };
        }
    }
}
