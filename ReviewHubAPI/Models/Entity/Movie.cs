﻿using System.ComponentModel.DataAnnotations;

namespace ReviewHubAPI.Models.Entity;

public class Movie
{
    [Required]
    public int Id { get; set; }
    [Required]
    public string MovieName { get; set; } = string.Empty;

    [Required]
    public string Summary { get; set; } = string.Empty;

    [Required]
    public int AverageRating { get; set; }

    [Required]
    public int ReleaseYear { get; set; }
    [Required]
    public string Director { get; set; } = string.Empty;
    [Required]
    public string Genre { get; set; } = string.Empty;
    [Required]
    public DateTime DateCreated { get; set; }
    [Required]
    public DateTime DateUpdated { get; set; }

    public virtual ICollection<Review>? Review { get; set; }
}
