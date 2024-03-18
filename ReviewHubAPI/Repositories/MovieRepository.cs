using ReviewHubAPI.Models.Entity;
using ReviewHubAPI.Repositories.Interface;

namespace ReviewHubAPI.Repositories
{
    public class MovieRepository : IMovieRepository
    {
        public Task<MovieEntity> DeleteMovieById(int Id)
        {
            throw new NotImplementedException();
        }

        public Task<ICollection<MovieEntity>> GetAllMovies(int pagesize, int pagenummer)
        {
            throw new NotImplementedException();
        }

        public Task<MovieEntity> GetMovieById(int Id)
        {
            throw new NotImplementedException();
        }

        public Task<MovieEntity> GetMovieByName(string name)
        {
            throw new NotImplementedException();
        }

        public Task<MovieEntity> UpdateMovieById(int Id, MovieEntity entity)
        {
            throw new NotImplementedException();
        }
    }
}
