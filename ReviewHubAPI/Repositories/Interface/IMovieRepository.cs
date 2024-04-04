using ReviewHubAPI.Models.Entity;

namespace ReviewHubAPI.Repositories.Interface
{
    public interface IMovieRepository
    {
        public Task<ICollection<Movie>> GetAllMovies(int pagesize, int pagenummer);

        public Task<Movie> GetMovieByName(string name);

        public Task<Movie> GetMovieById(int Id);

        public Task<Movie> UpdateMovieById(int Id, Movie dto);

        public Task<Movie> DeleteMovieById(int Id);
    }
}
