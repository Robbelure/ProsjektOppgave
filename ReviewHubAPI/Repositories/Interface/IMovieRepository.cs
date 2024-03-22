using ReviewHubAPI.Models.Entity;

namespace ReviewHubAPI.Repositories.Interface
{
    public interface IMovieRepository
    {
        public Task<ICollection<MovieEntity>> GetAllMovies(int pagesize, int pagenummer);

        public Task<MovieEntity> GetMovieByName(string name);

        public Task<MovieEntity> GetMovieById(int Id);

        public Task<MovieEntity> UpdateMovieById(int Id, MovieEntity dto);

        public Task<MovieEntity> DeleteMovieById(int Id);
    }
}
