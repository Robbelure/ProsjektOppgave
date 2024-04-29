using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ReviewHubAPI.Models.DTO;
using ReviewHubAPI.Services.Interface;

namespace ReviewHubAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class MoviePosterController : Controller
{
    private readonly IMoviePosterService _moviePosterService;
    private readonly ILogger<MoviePosterController> _logger;

    public MoviePosterController(IMoviePosterService Movieposterservice, ILogger<MoviePosterController> logger)
    {
        _moviePosterService = Movieposterservice;
        _logger = logger;
    }


    [Authorize]
    [HttpPost(Name = "AddMoviePoster")]
    public async Task<ActionResult<string>> AddMoviePoster([FromForm] int MovieID, IFormFile file)
    {
        var message = await _moviePosterService.AddMoviePoster(MovieID, file);
        return Ok(message);
    }


    [HttpGet(Name = "GetAllMoviePosters")]
    public async Task<ActionResult<ICollection<MoviePosterDTO>>> GetAllMoviePosters(int PageSize, int PageNumber)
    {
        var posters = await _moviePosterService.GetAllMoviePostersAsync(PageSize, PageNumber);
        if (posters == null || !posters.Any())
            return NotFound("No posters were found.");

        return Ok(posters);
    }


    [HttpGet("movieId={MovieId}", Name = "GetMoviePosterByMovieId")]
    public async Task<ActionResult<MoviePosterDTO>> GetMoviePosterByMovieId(int MovieId)
    {
        var poster = await _moviePosterService.GetMoviePosterByMovieIdAsync(MovieId);
        if (poster == null)
            return NotFound("No poster with that ID was found.");

        return Ok(poster);
    }


    [Authorize]
    [HttpDelete("movieId={MovieId}", Name = "DeleteMoviePosterByMovieId")]
    public async Task<ActionResult<MoviePosterDTO>> DeleteMoviePosterByMovieI(int MovieId)
    {
        var poster = await _moviePosterService.DeleteMoviePosterByMovieIdAsync(MovieId);
        if (poster == null)
            return BadRequest("The poster could not be deleted.");

        return Ok(poster);
    }
}
