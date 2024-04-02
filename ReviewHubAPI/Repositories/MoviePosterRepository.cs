using ReviewHubAPI.Models.Entity;
using ReviewHubAPI.Repositories.Interface;

namespace ReviewHubAPI.Repositories
{
    public class MoviePosterRepository : IMoviePosterRepository
    {
        public Task<MoviePosterEntity> DeleteMoviePosterMovieIdAsync(int MovieId)
        {
            throw new NotImplementedException();
        }

        public Task<ICollection<MoviePosterEntity>> GetAllMoviePostersAsync(int PageSize, int PageNummer)
        {
            throw new NotImplementedException();
        }

        public Task<MoviePosterEntity> GetMoviePostereByMovieIdAsync(int MovieId)
        {
            throw new NotImplementedException();
        }
    }
}
