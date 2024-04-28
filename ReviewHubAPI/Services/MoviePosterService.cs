using ReviewHubAPI.Mappers.Interface;
using ReviewHubAPI.Models.DTO;
using ReviewHubAPI.Models.Entity;
using ReviewHubAPI.Repositories.Interface;
using ReviewHubAPI.Services.Interface;

namespace ReviewHubAPI.Services;

public class MoviePosterService : IMoviePosterService
{
    private readonly IMoviePosterRepository _movieposterrep;
    private readonly IMapper<MoviePoster, MoviePosterDTO> _moviepostermapper;
 
    public MoviePosterService(IMoviePosterRepository movieposterRep, IMapper<MoviePoster,MoviePosterDTO> moviepostermapper)
    {
        _movieposterrep = movieposterRep;
        _moviepostermapper = moviepostermapper;
    }

    public async Task<string> AddMoviePoster(int movieID, IFormFile file)
    {
        var pic = await GetPictureBytesAsync(file);
        var movieposter = new MoviePoster { MovieId= movieID, Poster = pic ,DateCreated = DateTime.Now, DateUpdated = DateTime.Now};
        var addedposter = await _movieposterrep.AddMoviePoster(movieposter);

        return addedposter;
    }

    public async Task<MoviePosterDTO> DeleteMoviePosterByMovieIdAsync(int MovieId)
    {
        var posterToDelete = await _movieposterrep.DeleteMoviePosterByMovieIdAsync(MovieId);

        return _moviepostermapper.MapToDTO(posterToDelete) ?? null!;

    }

    public async Task<ICollection<MoviePosterDTO>> GetAllMoviePostersAsync(int PageSize, int PageNummer)
    {
        var posters = await _movieposterrep.GetAllMoviePostersAsync(PageSize, PageNummer);
        List<MoviePosterDTO> returnposter = new();

        if(posters == null)
        { return null!; }

        foreach (var poster in posters)
        {
            returnposter.Add(_moviepostermapper.MapToDTO(poster));
        }

        return returnposter;
    }

    public async Task<MoviePosterDTO> GetMoviePosterByMovieIdAsync(int MovieId)
    {
        var poster = await _movieposterrep.GetMoviePosterByMovieIdAsync(MovieId);

        return _moviepostermapper.MapToDTO(poster) ?? null!;
    }

    private async Task<byte[]> GetPictureBytesAsync(IFormFile picture)
    {

        using (var memoryStream = new System.IO.MemoryStream())
        {
            await picture.CopyToAsync(memoryStream);
            return memoryStream.ToArray();
        }
    }
}
