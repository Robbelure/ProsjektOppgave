using ReviewHubAPI.Mappers.Interface;
using ReviewHubAPI.Models.DTO;
using ReviewHubAPI.Models.Entity;

namespace ReviewHubAPI.Mappers
{
    public class ReviewPictureMapper : IMapper<ReviewPictureEntity, ReviewPictureDTO>
    {
        public ReviewPictureDTO MapToDTO(ReviewPictureEntity entity)
        {
            return new ReviewPictureDTO
            {
                Id = entity.Id,
                ReviewId = entity.ReviewId,
                ReviewPicture = entity.ReviewPicture
            };
        }

        public ReviewPictureEntity MapToEntity(ReviewPictureDTO dto)
        {
            return new ReviewPictureEntity
            {
                Id = dto.Id,
                ReviewId = dto.ReviewId,
                ReviewPicture = dto.ReviewPicture
            };
        }
    }
}
