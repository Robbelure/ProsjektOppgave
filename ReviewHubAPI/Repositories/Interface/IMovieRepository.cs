using ReviewHubAPI.Models.Entity;

namespace ReviewHubAPI.Repositories.Interface;

public interface IMovieRepository
{
    public Task<Movie> AddMovie(Movie model);
    public Task<ICollection<Movie>> GetAllMovies(int pagesize, int pagenummer);
    public Task<Movie> GetMovieByName(string name);
    public Task<Movie> GetMovieById(int Id);
    public Task<Movie> UpdateMovieById( Movie model);
    public Task<Movie> DeleteMovieById(int Id);
}
