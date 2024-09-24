using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using app.Enums;
using app.Repositories.Interfaces;
using Microsoft.AspNetCore.Identity;

namespace app.Repositories;

public class RoleRepository : IRoleRepository
{
    private readonly RoleManager<IdentityRole> _roleManager;
    public RoleRepository(RoleManager<IdentityRole> roleManager)
    {
        _roleManager = roleManager;    
    }

    public async Task<bool> VerifyIfRoleExistsAsync(string role)
    {
        return await _roleManager.RoleExistsAsync(role);
    }

    public async Task CreateRoleAsync(IdentityRole role)
    {
        await _roleManager.CreateAsync(role);
    }
}
