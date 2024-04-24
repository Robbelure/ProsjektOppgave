﻿namespace ReviewHubAPI.Models.DTO;
public class UserDTO
{
    public int Id { get; set; }
    public string Username { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string Firstname { get; set; } = string.Empty;
    public string Lastname { get; set; } = string.Empty;
    public DateTime DateCreated { get; set; }
    public bool IsAdmin { get; set; } = false;
}
