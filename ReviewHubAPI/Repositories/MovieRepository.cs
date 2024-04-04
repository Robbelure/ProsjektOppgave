using ReviewHubAPI.Models.Entity;
using ReviewHubAPI.Repositories.Interface;

namespace ReviewHubAPI.Repositories
{
    public class MovieRepository : IMovieRepository
    {
        public Task<Movie> DeleteMovieById(int Id)
        {
            throw new NotImplementedException();
        }

        public Task<ICollection<Movie>> GetAllMovies(int pagesize, int pagenummer)
        {
            throw new NotImplementedException();
        }

        public Task<Movie> GetMovieById(int Id)
        {
            throw new NotImplementedException();
        }

        public Task<Movie> GetMovieByName(string name)
        {
            throw new NotImplementedException();
        }

        public Task<Movie> UpdateMovieById(int Id, Movie entity)
        {
            throw new NotImplementedException();
        }
    }
}
