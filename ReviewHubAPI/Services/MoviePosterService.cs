﻿using ReviewHubAPI.Mappers.Interface;
using ReviewHubAPI.Models.DTO;
using ReviewHubAPI.Models.Entity;
using ReviewHubAPI.Repositories.Interface;
using ReviewHubAPI.Services.Interface;

namespace ReviewHubAPI.Services
{
    public class MoviePosterService : IMoviePosterService
    {
        private readonly IMoviePosterRepository _movieposterrep;
        private readonly IMapper<MoviePosterEntity, MoviePosterDTO> _moviepostermapper;
     
        public MoviePosterService(IMoviePosterRepository movieposterRep, IMapper<MoviePosterEntity,MoviePosterDTO> moviepostermapper)
        {
            _movieposterrep = movieposterRep;
            _moviepostermapper = moviepostermapper;
        }

        public async Task<string> AddMoviePoster(IFormFile file, int MovieID)
        {
            var pic = await GetPictureBytesAsync(file);
            var movieposter = new MoviePosterEntity { MovieEntityId = MovieID, MoviePoster = pic };
            var addedposter = await _movieposterrep.AddMoviePoster(movieposter);

            return addedposter;
        }
        public async Task<MoviePosterDTO> DeleteMoviePosterMovieIdAsync(int MovieId)
        {
            var posterToDelete = await _movieposterrep.DeleteMoviePosterMovieIdAsync(MovieId);

            return _moviepostermapper.MapToDTO(posterToDelete) ?? null!;

        }

        public async Task<ICollection<MoviePosterDTO>> GetAllMoviePostersAsync(int PageSize, int PageNummer)
        {
            var posters = await _movieposterrep.GetAllMoviePostersAsync(PageSize, PageNummer);
            List<MoviePosterDTO> returnposter = new();

            if(posters == null)
            { return null!; }

            foreach (var poster in posters)
            {
                returnposter.Add(_moviepostermapper.MapToDTO(poster));
            }

            return returnposter;
        }

        public async Task<MoviePosterDTO> GetMoviePostereByMovieIdAsync(int MovieId)
        {
            var poster = await _movieposterrep.GetMoviePostereByMovieIdAsync(MovieId);

            return _moviepostermapper.MapToDTO(poster) ?? null!;
        }

        private async Task<byte[]> GetPictureBytesAsync(IFormFile picture)
        {

            using (var memoryStream = new System.IO.MemoryStream())
            {
                await picture.CopyToAsync(memoryStream);
                return memoryStream.ToArray();
            }
        }
    }
}