using ReviewHubAPI.Mappers.Interface;
using ReviewHubAPI.Models.DTO;
using ReviewHubAPI.Models.Entity;
using ReviewHubAPI.Repositories.Interface;
using ReviewHubAPI.Services.Interface;
using System.IO;

namespace ReviewHubAPI.Services
{
    public class MovieService : IMovieService
    {
        private readonly IMovieRepository _movierep;
        private readonly IMapper<Movie, MovieDTO> _moviemapper;

        public MovieService(IMovieRepository movierep, IMapper<Movie, MovieDTO> moviemapper)
        {
            _movierep = movierep;
            _moviemapper = moviemapper;
        }

        public async Task<MovieDTO> AddMovie(MovieDTO DTO)
        {

            var movie = new Movie
            {
                MovieName = DTO.MovieName,
                Summary = DTO.Summary,
                ReleaseYear = DTO.ReleaseYear,
                Director = DTO.Director,
                Genre = DTO.Genre,
                DateCreated = DateTime.Now,
                DateUpdated = DateTime.Now
            };

           var movieToAdd = await  _movierep.AddMovie(movie);

            return _moviemapper.MapToDTO(movieToAdd) ?? null!;
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

        public async Task<ICollection<MovieDTO>> GetAllMovies(int pagesize, int pagenummer)
        {
            var movies = await  _movierep.GetAllMovies(pagesize, pagenummer);
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
