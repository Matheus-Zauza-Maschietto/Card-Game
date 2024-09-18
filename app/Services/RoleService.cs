using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using app.Enums;
using app.Models;
using Microsoft.AspNetCore.Identity;

namespace app.Services;

public class RoleService
{
    private readonly RoleManager<IdentityRole> _roleManager;
    public RoleService(RoleManager<IdentityRole> roleManager)
    {
        _roleManager = roleManager;
    }

    public async Task CreateRoleIfNotExistisAsync()
    {
        if (!await _roleManager.RoleExistsAsync(Roles.ADMIN.ToString()))
        {
            await _roleManager.CreateAsync(new IdentityRole(Roles.ADMIN.ToString()));
        }
    }

    

}
