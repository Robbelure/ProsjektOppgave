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

    /// <summary>
    /// Lagrer en ny poster i databasen som en BLOB.
    /// </summary>
    /// <param name="entity">MoviePoster-entiteten som skal lagres.</param>
    public async Task<string> AddMoviePoster(MoviePoster entity)
    {
        var poster = await _dbcontext.MoviePoster.AddAsync(entity);
        await _dbcontext.SaveChangesAsync();

        if(poster == null)
            return "Something went wrong and movieposter was not saved";

        return "Movieposter was saved";
    }

    public Task<MoviePoster> DeleteMoviePosterByMovieIdAsync(int MovieId)
    {
        throw new NotImplementedException();
    }

    public async Task<ICollection<MoviePoster>> GetAllMoviePostersAsync(int PageSize, int PageNummer)
    {
        var offset = (PageNummer - 1) * PageSize;
        if (offset < 0)
        {
            offset = 0; 
        }

        var posters = await _dbcontext.MoviePoster
            .OrderBy(x => x.Id)
            .Skip(offset) 
            .Take(PageSize)
            .ToListAsync();

        return posters;
    }

    public async Task<MoviePoster> GetMoviePosterByMovieIdAsync(int MovieId)
    {
        var poster = await _dbcontext.MoviePoster.FirstOrDefaultAsync(x => x.MovieId == MovieId);

        return poster ?? null!;
    }
}
