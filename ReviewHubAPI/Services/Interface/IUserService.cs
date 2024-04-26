﻿using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using ReviewHubAPI.Models.DTO;

namespace ReviewHubAPI.Services.Interface;

public interface IUserService
{
    Task<IEnumerable<UserDTO?>> GetAllUsersAsync();
    Task<UserDTO?> GetUserByIdAsync(int userId);
    Task<UserPublicProfileDTO?> GetUserPublicProfileByUsernameAsync(string username);
    Task<UserPublicProfileDTO?> GetUserPublicProfileByIdAsync(int userId);
    Task<UserDTO?> UpdateUserAsync(int userId, UserUpdateDTO userUpdateDto);
    //Task<UserDTO?> PatchUserAsync(int userId, JsonPatchDocument<UserUpdateDTO> patchDoc);
    Task DeleteUserAsync(int userId);
    Task<bool> IsUserAdminAsync(int userId);
}
