using ReviewHubAPI.Mappers.Interface;
using ReviewHubAPI.Models.DTO;
using ReviewHubAPI.Models.Entity;
using ReviewHubAPI.Repositories.Interface;
using ReviewHubAPI.Services.Interface;

namespace ReviewHubAPI.Services
{
    public class MovieService : IMovieService
    {
        private readonly IMovieRepository _movierep;
        private readonly IMapper<MovieEntity, MovieDTO> _moviemapper;

        public MovieService(IMovieRepository movierep, IMapper<MovieEntity, MovieDTO> moviemapper)
        {
            _movierep = movierep;
            _moviemapper = moviemapper;
        }
        public async Task<MovieDTO> DeleteMovieById(int Id)
        {
            var movie = await _movierep.GetMovieById(Id);

            if (movie != null)
            {
                await _movierep.DeleteMovieById(Id);

            }

            return _moviemapper.MapToDTO(movie!);

        }

        public async Task<ICollection<MovieDTO>> GetAllMovies()
        {
            var movies = await  _movierep.GetAllMovies();
            ICollection<MovieDTO> moviesdto = new List<MovieDTO>();

            foreach (var movie in movies)
            {
                moviesdto.Add(_moviemapper.MapToDTO(movie));
            }

            return moviesdto ?? null!;
        }

        public async Task<MovieDTO> GetMovieByName(string name)
        {
            var movie = await _movierep.GetMovieByName(name);

            return _moviemapper.MapToDTO(movie) ?? null!;
        }

        public async Task<MovieDTO> GetMovieById(int Id)
        {
            var movie = await _movierep.GetMovieById(Id);

            return _moviemapper.MapToDTO(movie) ?? null!;   
        }

        public async Task<MovieDTO> UpdateMovieById(int Id, MovieDTO dto)
        {
            var foundmovie = await  _movierep.GetMovieById(Id);

            var dateupdated = DateTime.Now;

            if (foundmovie != null)
            {
                foundmovie.Id = dto.Id;
                foundmovie.MovieName = dto.MovieName;
                foundmovie.ReleaseYear = dto.ReleaseYear;
                foundmovie.Director = dto.Director;
                foundmovie.Genre = dto.Genre;
                foundmovie.DateUpdated = dateupdated;

            }

            return _moviemapper.MapToDTO(foundmovie!);
                        
        }
    }
}
