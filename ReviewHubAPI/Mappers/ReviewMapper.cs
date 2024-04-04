using ReviewHubAPI.Mappers.Interface;
using ReviewHubAPI.Models.DTO;
using ReviewHubAPI.Models.Entity;

namespace ReviewHubAPI.Mappers
{
    public class ReviewMapper : IMapper<Review, ReviewDTO>
    {
        public ReviewDTO MapToDTO(Review entity)
        {
            return new ReviewDTO
            {
                Id = entity.Id,
                MovieId = entity.MovieId,
                UserId = entity.UserId,
                Title = entity.Title,
                Rating = entity.Rating,
                Text = entity.Text
            };
        }

        public Review MapToEntity(ReviewDTO dto)
        {
            return new Review
            {
                Id = dto.Id,
                MovieId = dto.MovieId,
                UserId = dto.UserId,
                Title = dto.Title,
                Rating = dto.Rating,
                Text = dto.Text
            };
        }
    }
}
