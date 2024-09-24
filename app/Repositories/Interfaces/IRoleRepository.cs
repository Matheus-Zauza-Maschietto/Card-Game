using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using app.Enums;
using Microsoft.AspNetCore.Identity;

namespace app.Repositories.Interfaces;

public interface IRoleRepository
{
    Task<bool> VerifyIfRoleExistsAsync(string role);
    Task CreateRoleAsync(IdentityRole role);
}
