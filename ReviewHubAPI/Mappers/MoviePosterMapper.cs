using ReviewHubAPI.Mappers.Interface;
using ReviewHubAPI.Models.DTO;
using ReviewHubAPI.Models.Entity;

namespace ReviewHubAPI.Mappers
{
    public class MoviePosterMapper : IMapper<MoviePoster, MoviePosterDTO>
    {
        public MoviePosterDTO MapToDTO(MoviePoster entity)
        {
            return new MoviePosterDTO
            {
                Id = entity.Id,
                MovieId = entity.MovieId,
                MoviePoster = entity.Poster

            };
        }

        public MoviePoster MapToEntity(MoviePosterDTO dto)
        {
            return new MoviePoster
            {
                Id = dto.Id,
                MovieId = dto.MovieId,
                Poster = dto.MoviePoster
            };
        }
    }
}
