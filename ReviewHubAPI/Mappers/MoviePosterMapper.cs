using ReviewHubAPI.Mappers.Interface;
using ReviewHubAPI.Models.DTO;
using ReviewHubAPI.Models.Entity;

namespace ReviewHubAPI.Mappers
{
    public class MoviePosterMapper : IMapper<MoviePosterEntity, MoviePosterDTO>
    {
        public MoviePosterDTO MapToDTO(MoviePosterEntity entity)
        {
            return new MoviePosterDTO
            {
                Id = entity.Id,
                MovieEntityId = entity.MovieEntityId,
                MoviePoster = entity.MoviePoster

            };
        }

        public MoviePosterEntity MapToEntity(MoviePosterDTO dto)
        {
            return new MoviePosterEntity
            {
                Id = dto.Id,
                MovieEntityId = dto.MovieEntityId,
                MoviePoster = dto.MoviePoster
            };
        }
    }
}
