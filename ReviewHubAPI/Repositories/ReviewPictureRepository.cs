﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ReviewHubAPI.Data;
using ReviewHubAPI.Models.Entity;
using ReviewHubAPI.Repositories.Interface;

//TODO! Må gjøre ferdig disse 
namespace ReviewHubAPI.Repositories
{
    public class ReviewPictureRepository : IReviewPictureRepository
    {
        private readonly ReviewHubDbContext _context;

        public ReviewPictureRepository(ReviewHubDbContext context)
        {
            _context = context;
        }
        public Task<string> AddReviewPicture(ReviewPicture entity)
        {
            throw new NotImplementedException();
        }

        public Task<ReviewPicture> DeleteReviewPictureByReviewIdAsync(int ReviewId)
        {
            throw new NotImplementedException();
        }

        public Task<ICollection<ReviewPicture>> GetAllReviewPicturesAsync(int PageSize, int PageNummer)
        {
            throw new NotImplementedException();
        }

        public async Task<ReviewPicture> GetReviewPictureByReviewIdAsync(int ReviewId)
        {
            var picture =  await _context.ReviewPicture.FirstOrDefaultAsync(x => x.Id == ReviewId);
           
            return picture ?? null!;
        }
    }
}
