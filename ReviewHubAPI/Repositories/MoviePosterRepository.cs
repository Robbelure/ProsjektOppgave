using ReviewHubAPI.Models.Entity;
using ReviewHubAPI.Repositories.Interface;

namespace ReviewHubAPI.Repositories
{
    public class MoviePosterRepository : IMoviePosterRepository
    {

        public Task<string> AddMoviePoster(MoviePoster entity)
        {
            throw new NotImplementedException();
        }
        public Task<MoviePoster> DeleteMoviePosterMovieIdAsync(int MovieId)
        {
            throw new NotImplementedException();
        }

        public Task<ICollection<MoviePoster>> GetAllMoviePostersAsync(int PageSize, int PageNummer)
        {
            throw new NotImplementedException();
        }

        public Task<MoviePoster> GetMoviePostereByMovieIdAsync(int MovieId)
        {
            throw new NotImplementedException();
        }
    }
}
