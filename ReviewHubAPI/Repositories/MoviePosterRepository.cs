using Microsoft.EntityFrameworkCore;
using ReviewHubAPI.Data;
using ReviewHubAPI.Models.Entity;
using ReviewHubAPI.Repositories.Interface;

namespace ReviewHubAPI.Repositories;

public class MoviePosterRepository : IMoviePosterRepository
{
    private readonly ReviewHubDbContext _dbcontext;

    public MoviePosterRepository(ReviewHubDbContext dbContext)
    {
        _dbcontext = dbContext;
    }

    public async Task<string> AddMoviePoster(MoviePoster entity)
    {
        var poster = await _dbcontext.MoviePoster.AddAsync(entity);
        await _dbcontext.SaveChangesAsync();

       if(poster == null)
        {
            return "Something went wrong and movieposter was not saved";
        }

        return "Movieposter was save";
    }
    public Task<MoviePoster> DeleteMoviePosterMovieIdAsync(int MovieId)
    {
        throw new NotImplementedException();
    }

    public async Task<ICollection<MoviePoster>> GetAllMoviePostersAsync(int PageSize, int PageNummer)
    {
        var posters = await _dbcontext.MoviePoster
            .OrderBy(x => x.Id)
              .Skip((PageNummer - 1) * PageSize)
             .Take(PageSize)
             .ToListAsync();

        return posters;
    }

    public async Task<MoviePoster> GetMoviePostereByMovieIdAsync(int MovieId)
    {
        var poster = await _dbcontext.MoviePoster.FirstOrDefaultAsync(x => x.MovieId == MovieId);

        return poster ?? null!;
    }
}
