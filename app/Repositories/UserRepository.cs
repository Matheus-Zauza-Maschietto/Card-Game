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

    public async Task<User?> GetUserByEmail(string email)
    {
        return await _userManager
        .FindByEmailAsync(email);
    }

    public async Task<IdentityResult> CreateUser(CreateUserDTO userDto)
    {
        User newUser = new User(userDto.Email, userDto.UserName, string.Empty, userDto.Language);

        return await _userManager.CreateAsync(newUser, userDto.Password);
    }
}
