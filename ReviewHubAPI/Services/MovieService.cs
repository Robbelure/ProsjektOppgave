using ReviewHubAPI.Mappers.Interface;
using ReviewHubAPI.Models.DTO;
using ReviewHubAPI.Models.Entity;
using ReviewHubAPI.Repositories.Interface;
using ReviewHubAPI.Services.Interface;
using System.IO;

namespace ReviewHubAPI.Services;

public class MovieService : IMovieService
{
    private readonly IMovieRepository _movierep;
    private readonly IMapper<Movie, MovieDTO> _moviemapper;
    private readonly IReviewService _reviewsservice;

    public MovieService(IMovieRepository movierep, IMapper<Movie, MovieDTO> moviemapper, IReviewService reviewsService)
    {
        _movierep = movierep;
        _moviemapper = moviemapper;
        _reviewsservice = reviewsService;
    }

    public async Task<MovieDTO> AddMovie(MovieDTO DTO)
    {

        var movie = new Movie
        {
            MovieName = DTO.MovieName,
            AverageRating = 0,
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

        if(movies.Count > 0) 
        { 
            foreach (var movie in movies)
            {
                int avaragerating = 0;
                var reviews = await  _reviewsservice.GetReviewByMovieId(movie.Id);
                if(reviews.Count > 0)
                {
                    int reviewsCount = reviews.Count();
                    foreach (var review in reviews)
                    {
                        avaragerating += review.Rating;
                    }

                    var movierating = avaragerating / reviewsCount;

                    movie.AverageRating = (int)movierating;
                }
               
                moviesdto.Add(_moviemapper.MapToDTO(movie));
            }
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
        if (movie != null)
        {
            
            var reviews = await _reviewsservice.GetReviewByMovieId(Id);
            if (reviews.Count != 0)
            {
                int avaragerating = 0;
                foreach (var review in reviews)
                {
                    avaragerating += review.Rating;
                }

                movie.AverageRating = (int)Math.Round((double)avaragerating / reviews.Count());
            }
        }
        return _moviemapper.MapToDTO(movie!) ?? null!;   
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
