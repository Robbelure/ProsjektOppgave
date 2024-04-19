using Microsoft.EntityFrameworkCore;
using ReviewHubAPI.Data;
using ReviewHubAPI.Models.Entity;
using ReviewHubAPI.Repositories.Interface;

namespace ReviewHubAPI.Repositories
{
    public class ReviewRepository : IReviewRespository
    {
        private readonly ReviewHubDbContext _dbcontext;

        public ReviewRepository(ReviewHubDbContext dbContext)
        {
            _dbcontext = dbContext;
        }
        public async Task<Review> AddReview(Review entity)
        {
            var review = await _dbcontext.Reviews.AddAsync(entity);
            await _dbcontext.SaveChangesAsync();
            return review.Entity;
        }

        public async Task<Review> DeleteReviewById(int id)
        {
            var review = await _dbcontext.Reviews.FirstOrDefaultAsync(x => x.Id == id);
            if (review != null)
            {
                _dbcontext.Reviews.Remove(review);
            }
            return review!;
        }

        //TODO trenger vi denne?
        public Task<ICollection<Review>> GetAllReviews(int pagesize, int pagenummer)
        {
            throw new NotImplementedException();
        }

        public async Task<Review> GetReviewById(int id)
        {
            var review = await _dbcontext.Reviews.FirstOrDefaultAsync(x => x.Id == id);

            return review ?? null!;
        }

        public async Task<ICollection<Review>> GetReviewByMovieId(int MovieId)
        {
            var review = await _dbcontext.Reviews.Where(x => x.MovieId == MovieId).ToListAsync();

            return review ?? null!;
        }

        public async Task<ICollection<Review>> GetReviewByUserId(int UserId)
        {
            var review = await _dbcontext.Reviews.Where(x => x.UserId == UserId).ToListAsync();

            return review ?? null!;
        }

        public Task<Review> UpdateReviewById(Review entity)
        {
            throw new NotImplementedException();
        }
    }
}
