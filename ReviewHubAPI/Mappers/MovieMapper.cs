using ReviewHubAPI.Mappers.Interface;
using ReviewHubAPI.Models.DTO;
using ReviewHubAPI.Models.Entity;

namespace ReviewHubAPI.Mappers;

public class MovieMapper : IMapper<Movie, MovieDTO>
{
    public MovieDTO MapToDTO(Movie entity)
    {
        return new MovieDTO
        {
            Id = entity.Id,
            MovieName = entity.MovieName,
            Summary = entity.Summary,
            ReleaseYear = entity.ReleaseYear,
            AverageRating = entity.AverageRating,
            Director = entity.Director,
            Genre = entity.Genre,
            DateCreated = entity.DateCreated,
            DateUpdated = entity.DateUpdated,
        };
    }

    public Movie MapToEntity(MovieDTO dto)
    {
        return new Movie
        {
            Id = dto.Id,
            MovieName = dto.MovieName,
            Summary = dto.Summary,
            ReleaseYear = dto.ReleaseYear,
            AverageRating = dto.AverageRating,
            Director = dto.Director,
            Genre = dto.Genre,
            DateCreated = dto.DateCreated,
            DateUpdated = dto.DateUpdated,
        };
    }
}
