using ReviewHubAPI.Models.DTO;

namespace ReviewHubAPI.Services.Interface
{
    public interface IMoviePosterService
    {
        public Task<string> AddMoviePoster(int MovieID, IFormFile file);
        public Task<ICollection<MoviePosterDTO>> GetAllMoviePostersAsync(int PageSize, int PageNummer);

        public Task<MoviePosterDTO> GetMoviePostereByMovieIdAsync(int MovieId);

        public Task<MoviePosterDTO> DeleteMoviePosterMovieIdAsync(int MovieId);
    }
}
