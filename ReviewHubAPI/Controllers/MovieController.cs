using Microsoft.AspNetCore.Mvc;
using ReviewHubAPI.Models.DTO;
using ReviewHubAPI.Services.Interface;

namespace ReviewHubAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class MovieController : Controller
{
    private readonly IMovieService _movieService;

    public ILogger<MovieController> _logger;

    public MovieController(IMovieService movieservice, ILogger<MovieController> logger)
    {
        _movieService = movieservice;
        _logger = logger;
    }

    [HttpPost(Name = "AddMovie")]
    public async Task<ActionResult<MovieDTO>> AddMovie(MovieDTO dto)
    {
        var movie = await _movieService.AddMovie(dto);
        if (movie == null)
            return BadRequest("Movie was not added");

        return Ok(movie);
    }


    [HttpGet(Name = "GetAllMovies")]
    public async Task<ActionResult<ICollection<MovieDTO>>> GetAllMovies(int pageSize, int pageNumber)
    {
        var movies = await _movieService.GetAllMovies(pageSize, pageNumber);
        if (movies == null)
            return NotFound("There are no movies in the database");

        return Ok(movies);
    }


    [HttpGet("movieId={Id}", Name = "GetMovieById")]
    public async Task<ActionResult<MovieDTO>> GetMovieById(int Id)
    {
        var movie = await _movieService.GetMovieById(Id);
        if (movie == null)
            return NotFound("No movie with that ID was found");

        return Ok(movie);
    }


    [HttpGet("name={name}", Name = "GetMovieByName")]
    public async Task<ActionResult<MovieDTO>> GetMovieByName(string name)
    {
        var movie = await _movieService.GetMovieByName(name);
        if (movie == null)
            return NotFound("No movie with that name was found");

        return Ok(movie);
    }


    [HttpPut("Id={Id}", Name = "UpdateMovieById")]
    public async Task<ActionResult<MovieDTO>> UpdateMovieById(int Id, MovieDTO dto)
    {
        var movie = await _movieService.UpdateMovieById(Id, dto);
        if (movie == null)
            return BadRequest("The movie could not be updated");

        return Ok(movie);
    }


    [HttpDelete("Id={Id}", Name = "DeleteMovieById")]
    public async Task<ActionResult<MovieDTO>> DeleteMovieById(int Id)
    {
        var movie = await _movieService.DeleteMovieById(Id);
        if (movie == null)
            return BadRequest("The movie could not be deleted");

        return Ok(movie);
    }
}
