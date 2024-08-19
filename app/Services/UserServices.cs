using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using app.DTOs;
using app.Models;
using app.Repositories;
using Microsoft.AspNetCore.Identity;

namespace app.Services;

public class UserService
{
    private readonly UserManager<User> _userManager;
    private readonly JsonWebTokensService _webTokensService;
    private readonly IUserRepository _userRepository;
    public UserService(UserManager<User> userManager, JsonWebTokensService webTokensService, IUserRepository userRepository)
    {
        _userRepository = userRepository;
        _userManager = userManager;
        _webTokensService = webTokensService;
    }

    public async Task CreateUser(CreateUserDTO userDto)
    {
        User? existUser = await _userRepository.GetUserByEmail(userDto.Email);

        if(existUser is not null)
        {
            throw new Exception("Already exists an user with this email");
        }

        IdentityResult result = await _userRepository.CreateUser(userDto);

        if (!result.Succeeded)
        {
            throw new Exception(GetIdentityResultErros(result));
        }
    }

    public async Task<string> LoginUser(LoginUserDTO loginDto)
    {
        User existUser = await GetUserByEmail(loginDto.Email);

        bool passwordIsOk = await _userManager.CheckPasswordAsync(existUser, loginDto.Password);

        if(!passwordIsOk)
        {
            throw new Exception("Password was wrong");
        }

        return _webTokensService.GerarToken(GetClaims(loginDto));
    }

    public async Task<User> GetUserByEmail(string email)
    {
        User? user = await _userRepository.GetUserByEmail(email);

        if(user is null){
            throw new Exception("User not found");
        }

        return user;
    }

    private ClaimsIdentity GetClaims(LoginUserDTO loginDto)
    {
        return new ClaimsIdentity(new Claim[]
        {
            new Claim(ClaimTypes.Email, loginDto.Email)
        });
    }

    private string GetIdentityResultErros(IdentityResult identityResult) => string.Join(". ", identityResult.Errors.Select(p => p.Description));
    
}