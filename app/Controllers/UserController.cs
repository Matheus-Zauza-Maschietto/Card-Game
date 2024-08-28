  using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using app.DTOs;
using app.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace app.Controllers;

[AllowAnonymous]
[ApiController]
[Route("api/[controller]")]
public class UserController : ControllerBase
{
    private readonly UserService _userService;
    public UserController(UserService userService)
    {
        _userService = userService;
    }

    [HttpPost("Create")]
    public async Task<IActionResult> CreateUserAsync(CreateUserDTO userDTO)
    {
        try
        {
            await _userService.CreateUser(userDTO);
            return Ok("Usuario criado com sucesso");
        }
        catch (Exception ex)
        {
            return Problem(ex.Message);
        }
    }

    [HttpPost("Login")]
    public async Task<IActionResult> LoginUserAsync(LoginUserDTO loginDTO)
    {
        try
        {
            string token = await _userService.LoginUser(loginDTO);
            return Ok(token);
        }
        catch (Exception ex)
        {
            return Problem(ex.Message);
        }
    }
}
