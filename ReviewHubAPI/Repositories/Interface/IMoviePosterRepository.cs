using ReviewHubAPI.Models.Entity;

namespace ReviewHubAPI.Repositories.Interface
{
    public interface IMoviePosterRepository
    {
        public Task<string> AddMoviePoster(MoviePoster entity);
        public Task<ICollection<MoviePoster>> GetAllMoviePostersAsync(int PageSize, int PageNummer);

        public Task<MoviePoster> GetMoviePostereByMovieIdAsync(int MovieId);

        public Task<MoviePoster> DeleteMoviePosterMovieIdAsync(int MovieId);
    }
}
