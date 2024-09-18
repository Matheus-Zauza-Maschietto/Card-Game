using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using app.DTOs;
using app.Models;
using Microsoft.AspNetCore.Identity;

namespace app.Repositories;

public interface IUserRepository
{
    Task<User> GetUserByEmail(string email);
    Task<IdentityResult> CreateUser(User newUser, string password);
    Task<IdentityResult> DeleteUserById(User user);
    Task<bool> CheckPassword(User foundedUser, string password);
    Task<IList<string>> GetRolesFromUserAsync(User user);
    Task<IdentityResult> AddRoleToUserAsync(string role, User user);
}