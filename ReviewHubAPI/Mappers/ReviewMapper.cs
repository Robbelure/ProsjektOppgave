using ReviewHubAPI.Mappers.Interface;
using ReviewHubAPI.Models.DTO;
using ReviewHubAPI.Models.Entity;

namespace ReviewHubAPI.Mappers
{
    public class ReviewMapper : IMapper<ReviewEntity, ReviewDTO>
    {
        public ReviewDTO MapToDTO(ReviewEntity entity)
        {
            return new ReviewDTO
            {
                Id = entity.Id,
                MovieId = entity.MovieId,
                Userid = entity.Userid,
                Title = entity.Title,
                Rating = entity.Rating,
                Text = entity.Text,
                MoviePicture = entity.MoviePicture,
            };
        }

        public ReviewEntity MapToEntity(ReviewDTO dto)
        {
            return new ReviewEntity
            {
                Id = dto.Id,
                MovieId = dto.MovieId,
                Userid = dto.Userid,
                Title = dto.Title,
                Rating = dto.Rating,
                Text = dto.Text,
                MoviePicture = dto.MoviePicture,
            };
        }
    }
}
