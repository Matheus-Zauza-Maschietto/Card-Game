using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using app.Enums;
using app.Models;
using app.Repositories;
using app.Repositories.Interfaces;
using Microsoft.AspNetCore.Identity;

namespace app.Services;

public class RoleService
{
    private readonly IRoleRepository _roleRepository;
    public RoleService(IRoleRepository roleRepository)
    {
        _roleRepository = roleRepository;
    }

    public async Task CreateRoleIfNotExistisAsync()
    {
        if (!await _roleRepository.VerifyIfRoleExistsAsync(Roles.ADMIN.ToString()))
        {
            await _roleRepository.CreateRoleAsync(new IdentityRole(Roles.ADMIN.ToString()));
        }
    }

    

}
