using System.Security.Claims;
using app.DTOs;
using app.Enums;
using app.Models;
using app.Repositories;
using Microsoft.AspNetCore.Identity;

namespace app.Services;

public class UserService
{
    private readonly JsonWebTokensService _webTokensService;
    private readonly IUserRepository _userRepository;
    private readonly RoleService _roleService;
    public UserService(JsonWebTokensService webTokensService, IUserRepository userRepository, RoleService roleService)
    {
        _roleService = roleService;
        _userRepository = userRepository;
        _webTokensService = webTokensService;
    }

    public async Task CreateUser(CreateUserDTO userDto)
    {
        User? existUser = await _userRepository.GetUserByEmail(userDto.Email);
        if(existUser is not null)
        {
            throw new Exception("Already exists an user with this email");
        }

        if(userDto.IsAdmin)
        {
            await _roleService.CreateRoleIfNotExistisAsync();
        }

        User newUser = new User(userDto.Email, userDto.UserName, userDto.LanguageId);
        IdentityResult result = await _userRepository.CreateUser(newUser, userDto.Password);
        if (!result.Succeeded)
        {
            throw new Exception(GetIdentityResultErros(result));
        }

        IdentityResult roleResult = await _userRepository.AddRoleToUserAsync(Roles.ADMIN.ToString(), newUser);
        if (!roleResult.Succeeded)
        {
            throw new Exception(GetIdentityResultErros(roleResult));
        }
    }

    public async Task<string> LoginUser(LoginUserDTO loginDto)
    { 
        User existUser = await GetUserByEmail(loginDto.Email);
        bool passwordIsOk = await _userRepository.CheckPassword(existUser, loginDto.Password);

        if(!passwordIsOk)
        {
            throw new Exception("Password was wrong");
        }

        IList<string> roles = await _userRepository.GetRolesFromUserAsync(existUser);

        return _webTokensService.GerarToken(GetClaims(loginDto, roles));
    }

    public async Task<User> GetUserById(string id)
    {
        User? user = await _userRepository.GetUserById(id);

        if(user is null){
            throw new Exception("User not found");
        }

        return user;
    }

    public async Task<User> GetUserByEmail(string email)
    {
        User? user = await _userRepository.GetUserByEmail(email);

        if(user is null){
            throw new Exception("User not found");
        }

        return user;
    }

    public async Task DeleteUserByEmail(string email)
    {
        User user = await GetUserByEmail(email);
        IdentityResult result = await _userRepository.DeleteUserById(user);
        if (!result.Succeeded)
        {
            throw new Exception(GetIdentityResultErros(result));
        }
    }

    private ClaimsIdentity GetClaims(LoginUserDTO loginDto, IList<string> roles)
    {

        List<Claim> claims = new List<Claim>()
        {
            new Claim(ClaimTypes.Email, loginDto.Email)
        };
        string? adminRole = roles.FirstOrDefault(p => p == Roles.ADMIN.ToString());
        if(adminRole is not null)
        {
            claims.Add(new Claim(ClaimTypes.Role, adminRole));
        }

        return new ClaimsIdentity(claims);
    }

    private string GetIdentityResultErros(IdentityResult identityResult) => string.Join(". ", identityResult.Errors.Select(p => p.Description));
    
}