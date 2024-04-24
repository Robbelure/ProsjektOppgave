using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using ReviewHubAPI.Data;
using ReviewHubAPI.Models.Entity;
using ReviewHubAPI.Repositories.Interface;

namespace ReviewHubAPI.Repositories;

public class MovieRepository : IMovieRepository
{
    private readonly ReviewHubDbContext _dbcontext;

    public MovieRepository(ReviewHubDbContext dbContext)
    {
        _dbcontext = dbContext;
    }

    async Task<Movie> IMovieRepository.AddMovie(Movie model)
    {
        var movie = await _dbcontext.Movies.AddAsync(model);
        await _dbcontext.SaveChangesAsync();

        return movie.Entity;
    }
    public Task<Movie> DeleteMovieById(int Id)
    {
        throw new NotImplementedException();
    }
   
    public async Task<ICollection<Movie>> GetAllMovies(int pagesize, int pagenummer)
    {
        var movies = await _dbcontext.Movies
            .OrderBy( x => x.Id)
              .Skip((pagenummer - 1) * pagesize)
             .Take(pagesize).
             ToListAsync();

        return movies;
    }

    public async Task<Movie> GetMovieById(int Id)
    {
        var movie = await _dbcontext.Movies.FirstOrDefaultAsync(x => x.Id == Id);

        return movie ?? null!;
    }

    public async Task<Movie> GetMovieByName(string name)
    {
        var movie = await _dbcontext.Movies.FirstOrDefaultAsync(x => x.MovieName == name);

        return movie ?? null!;
    }

    public async Task<Movie> UpdateMovieById(Movie model)
    {
        var movie = await _dbcontext.Movies.AddAsync(model);
        await _dbcontext.SaveChangesAsync();

        return movie.Entity ?? null!;

    }

   

  
}
