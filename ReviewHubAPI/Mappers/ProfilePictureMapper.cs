﻿using ReviewHubAPI.Mappers.Interface;
using ReviewHubAPI.Models.DTO;
using ReviewHubAPI.Models.Entity;

namespace ReviewHubAPI.Mappers
{
    public class ProfilePictureMapper : IMapper<ProfilePicture, ProfilePictureDTO>
    {
        public ProfilePictureDTO MapToDTO(ProfilePicture entity)
        {
            return new ProfilePictureDTO
            {
                Id = entity.Id,
                UserId = entity.UserId,
                ProfilePicture = entity.Picture

            };
        }

        public ProfilePicture MapToEntity(ProfilePictureDTO dto)
        {
            return new ProfilePicture
            {
                Id = dto.Id,
                UserId = dto.UserId,
                Picture = dto.ProfilePicture
            };
        }
    }
}
