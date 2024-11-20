using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using app.DTOs;
using app.Models;
using app.Services;
using Microsoft.AspNetCore.Identity;

namespace app.Repositories;

public class UserRepository: IUserRepository
{
    private readonly UserManager<User> _userManager;

    public UserRepository(UserManager<User> userManager)
    {
        _userManager = userManager;
    }

    public async Task<User?> GetUserById(string id)
    {
        return await _userManager.FindByIdAsync(id);
    }
    
    public async Task<User?> GetUserByEmail(string email)
    {
        return await _userManager.FindByEmailAsync(email);
    }

    public async Task<IdentityResult>  CreateUser(User newUser, string password)
    {
        return await _userManager.CreateAsync(newUser, password);
    }

    public async Task<IdentityResult> DeleteUserById(User user)
    {
        return await _userManager.DeleteAsync(user);
    }

    public async Task<bool> CheckPassword(User foundedUser, string password)
    {
        return await _userManager.CheckPasswordAsync(foundedUser, password);
    }

    public async Task<IdentityResult> AddRoleToUserAsync(string role, User user)
    {
        return await _userManager.AddToRoleAsync(user, role);
    }

    public async Task<IList<string>> GetRolesFromUserAsync(User user)
    {
        return await _userManager.GetRolesAsync(user);
    }

}
