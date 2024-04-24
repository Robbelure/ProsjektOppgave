using ReviewHubAPI.Models.DTO;
using ReviewHubAPI.Models.Entity;

namespace ReviewHubAPI.Services.Interface;

public interface IMovieService
{
    public Task<MovieDTO> AddMovie(MovieDTO DTO);
    public Task<ICollection<MovieDTO>> GetAllMovies(int pagesize, int pagenummer);
    public Task<MovieDTO> GetMovieByName(string name);
    public Task<MovieDTO> GetMovieById(int Id);
    public Task<MovieDTO> UpdateMovieById(int Id, MovieDTO dto);
    public Task<MovieDTO> DeleteMovieById(int Id);
}
