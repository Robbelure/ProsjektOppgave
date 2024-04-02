using ReviewHubAPI.Models.Entity;

namespace ReviewHubAPI.Repositories.Interface
{
    public interface IMoviePosterRepository
    {
        public Task<ICollection<MoviePosterEntity>> GetAllMoviePostersAsync(int PageSize, int PageNummer);

        public Task<MoviePosterEntity> GetMoviePostereByMovieIdAsync(int MovieId);

        public Task<MoviePosterEntity> DeleteMoviePosterMovieIdAsync(int MovieId);
    }
}
