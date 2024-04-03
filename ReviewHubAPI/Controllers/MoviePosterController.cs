using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using ReviewHubAPI.Models.DTO;
using ReviewHubAPI.Services.Interface;

namespace ReviewHubAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MoviePosterController : Controller
    {
        private readonly IMoviePosterService _movieposterservice;
        private readonly ILogger<MoviePosterController> _logger;

        public MoviePosterController(IMoviePosterService Movieposterservice, ILogger<MoviePosterController> logger)
        {
            _movieposterservice = Movieposterservice;
            _logger = logger;
        }

        [HttpPost]
        public async Task<ActionResult<string>> AddMoviePoster(IFormFile file, int MovieID)
        {
            try
            {
                var message = _movieposterservice.AddMoviePoster(file, MovieID);

                return Ok(message);
            }
            catch (Exception ex) 
            {
                _logger.LogError("An Error occurred on add movie poster in the MoviePosterController : {ex}", ex);

                return StatusCode(500, $"An error occurred while trying to add movie poster");

            }


        }


        [HttpGet (Name = "GetAllMoviePosters")]
        public async Task<ActionResult<ICollection<MoviePosterDTO>>> GetAllMoviePostersAsync(int PageSize, int PageNummer)
        {
            try
            {
                var posters = await _movieposterservice.GetAllMoviePostersAsync(PageSize, PageNummer);

                if (posters == null)
                {
                    return NotFound("No poster were found");
                }
                return Ok(posters);

            }
            catch(Exception ex)
            {
                _logger.LogError("An Error occurred on get all movie posters in the MoviePosterController : {ex}", ex);

                return StatusCode(500, $"An error occurred while trying to get profile all movie poster");
            }
           
        }

        [HttpGet("UserId/{UserId}", Name = "GetMoviePostereByMovieId")]
         public async Task<ActionResult<MoviePosterDTO>> GetMoviePostereByMovieIdAsync(int MovieId)
        {
            try
            {
                var poster = await _movieposterservice.GetMoviePostereByMovieIdAsync(MovieId);
                if (poster == null)
                {
                    return NotFound("No poster with that id was found");
                }

                return Ok(poster);
            }
            catch (Exception ex)
            {
                _logger.LogError("An Error occurred on get movie poster by movie Id in the MoviePosterController : {ex}", ex);

                return StatusCode(500, $"An error occurred while trying to get movie poster by movie id");

            }
         }

        [HttpDelete("UserId/{UserId}", Name = "DeleteMoviePosterMovieId")]
        public async Task<ActionResult<MoviePosterDTO>> DeleteMoviePosterMovieIdAsync(int MovieId)
        {
            try
            {
                var poster = await _movieposterservice.DeleteMoviePosterMovieIdAsync(MovieId);
                if (poster == null)
                {
                    return BadRequest("The poster could not be delete");
                }

                return Ok(poster);
            
            }
            catch(Exception ex)
            {
                _logger.LogError("An Error occurred on delete movie poster by movie Id in the MoviePosterController : {ex}", ex);

                return StatusCode(500, $"An error occurred while trying to delete movie poster by movie id");

            }
        }
    }
}
