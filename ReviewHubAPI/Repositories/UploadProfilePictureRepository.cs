using Microsoft.EntityFrameworkCore;
using ReviewHubAPI.Data;
using ReviewHubAPI.Models.Entity;
using ReviewHubAPI.Repositories.Interface;

namespace ReviewHubAPI.Repositories
{
    public class UploadProfilePictureRepository : IUploadProfilePictureRepository
    {
        private readonly ReviewHubDbContext _context;

        public UploadProfilePictureRepository(ReviewHubDbContext context)
        {
            _context = context;
        }

        public async Task<string> AddOrUpdateProfilePictureAsync(ProfilePicture entity)
        {
            var existingPicture = await _context.ProfilePicture.FirstOrDefaultAsync(p => p.UserId == entity.UserId);
            if (existingPicture != null)
            {
                existingPicture.Picture = entity.Picture;
                _context.ProfilePicture.Update(existingPicture);
            }
            else
            {
                await _context.ProfilePicture.AddAsync(entity);
            }

            await _context.SaveChangesAsync();
            return "Profile picture updated successfully";
        }

        public async Task<ProfilePicture> DeleteProfilePictureByUserIdAsync(int UserId)
        {
            throw new NotImplementedException();
        }

        public async Task<ICollection<ProfilePicture>> GetAllProfilePicturesAsync(int pageSize, int pageNumber)
        {
            // Bruk pagination til å hente data
            var profilePictures = await _context.ProfilePicture
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            return profilePictures;  // Returnerer en tom liste hvis det ikke finnes noen bilder
        }

        public async Task<ProfilePicture> GetProfilePictureByUserIdAsync(int userId)
        {
            return await _context.ProfilePicture.FirstOrDefaultAsync(p => p.UserId == userId);
        }
    }
}
