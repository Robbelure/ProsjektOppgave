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

        public MovieController(IMovieService movieservice)
        {
            _movieservice = movieservice;
        }
        [HttpGet(Name = "GetAllMovies")]

        public async Task<ActionResult< ICollection<MovieDTO>>> GetAllMovies()
        {
            var movies =  await _movieservice.GetAllMovies();

            if (movies == null)
            { 
                return NotFound("There are no movies in the database"); 
            }

            return Ok(movies);
        }

        //TODO: Legg til update Movie
        [HttpGet("{Id}", Name = "GetMovieById")]

        public async Task<ActionResult<MovieDTO>> GetMovieById(int Id)
        {
            var movie = await _movieservice.GetMovieById(Id);
            if (movie == null)
            {
                return NotFound("No movie with that id was found");
            }
            return Ok(movie);
        }

        // POST: MovieController/Edit/5
        [HttpPut("{Id}", Name = "UpdateMovieById")]

        public async Task<ActionResult<MovieDTO>> UpdateMovieById(int Id, MovieDTO dto)
        {
            var movie = await _movieservice.UpdateMovieById(Id, dto);
            if (movie == null)
            {
                return BadRequest("The movie could not be updated");
            }
            return Ok(movie);
        }

        
        [HttpDelete("{Id}", Name ="DeleteMovieById")]
        public async Task<ActionResult<MovieDTO>> DeleteMovieById(int Id)
        {
            var movie = await _movieservice.DeleteMovieById(Id);

            if (movie == null)
            {
                return BadRequest("The movie could not be deleted");
            }
            return Ok(movie);

        }
    }
}
