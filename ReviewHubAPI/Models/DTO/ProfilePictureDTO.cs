﻿using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace ReviewHubAPI.Models.DTO
{
    public class ProfilePictureDTO
    {
        public int Id { get; set; }

        public int UserID { get; set; }

        public byte[]? ProfilePicture { get; set; }
    }
}