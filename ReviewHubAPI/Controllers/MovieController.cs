using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ReviewHubAPI.Models.DTO;
using ReviewHubAPI.Services.Interface;

namespace ReviewHubAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MovieController : Controller
    {
        private readonly IMovieService _movieservice;

        public ILogger<MovieController> _logger;

        public MovieController(IMovieService movieservice, ILogger<MovieController> logger)
        {
            _movieservice = movieservice;
            _logger = logger;
        }

        [HttpGet(Name = "GetAllMovies")]

        public async Task<ActionResult<ICollection<MovieDTO>>> GetAllMovies(int pagesize, int pagenummer)
        {
            try
            {
                var movies = await _movieservice.GetAllMovies(pagesize, pagenummer);
                if (movies == null)
                {
                    return NotFound("There are no movies in the database");
                }
                return Ok(movies);
            }
            catch (Exception ex)
            {
                _logger.LogError("An Error occurred on get all movies in the MovieController:{ex}", ex);
                return StatusCode(500, $"An error occurred while trying to get all movies");
            }
        }


        [HttpGet("movieId={Id}", Name = "GetMovieById")]

        public async Task<ActionResult<MovieDTO>> GetMovieById(int Id)
        {
            try
            {
                var movie = await _movieservice.GetMovieById(Id);
                if (movie == null)
                {
                    return NotFound("No movie with that id was found");
                }
                return Ok(movie);
            }
            catch (Exception ex)
            {
                _logger.LogError("An Error occurred on get movie by id in the MovieController:{ex}", ex);
                return StatusCode(500, $"An error occurred while trying to get the movie by id");
            }
        }

        [HttpGet("name={name}", Name = "GetMovieByName")]

        public async Task<ActionResult<MovieDTO>> GetMovieByName(string name)
        {
            try 
            { 
                var movie = await _movieservice.GetMovieByName(name);

                if (movie == null)
                {
                    return NotFound("No movie with that name was found");
                }
                return Ok(movie);
            }
            catch (Exception ex)
            {
                _logger.LogError("An Error occurred on get movies by name in the MovieController: {ex}",ex);
                return StatusCode(500, $"An error occurred while trying to get movie by name");

            }
        }


        [HttpPut("Id={Id}", Name = "UpdateMovieById")]

        public async Task<ActionResult<MovieDTO>> UpdateMovieById(int Id, MovieDTO dto)
        {
            try 
            { 
                var movie = await _movieservice.UpdateMovieById(Id, dto);
                if (movie == null)
                {
                    return BadRequest("The movie could not be updated");
                }   
                return Ok(movie);
            }
            catch(Exception ex)
            {
                _logger.LogError("An Error occurred on update movie in the MovieController:{ex}", ex);
                return StatusCode(500, $"An error occurred while trying to update the movie");

            }
        }


        [HttpDelete("Id={Id}", Name = "DeleteMovieById")]
        public async Task<ActionResult<MovieDTO>> DeleteMovieById(int Id)
        {
            try 
            { 
            var movie = await _movieservice.DeleteMovieById(Id);

            if (movie == null)
            {
                return BadRequest("The movie could not be deleted");
            }
            return Ok(movie);
            }
            catch(Exception ex)
            {
                _logger.LogError("An Error occurred on delete movie in the MovieController: {ex}",ex);
                return StatusCode(500, $"An error occurred while trying to delete the movie");

            }

        }

    }
}
