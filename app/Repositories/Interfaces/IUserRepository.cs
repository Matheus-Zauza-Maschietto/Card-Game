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
    Task<IdentityResult> CreateUser(CreateUserDTO userDto);
}