using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using app.DTOs;
using FluentValidation;

namespace app.Helpers;

public static class ValidatorAssemblyConfig
{
    public static void ConfigureValidators(this WebApplicationBuilder builder)
    {
        builder.Services.AddValidatorsFromAssemblyContaining<LoginUserDTO>();    
        builder.Services.AddValidatorsFromAssemblyContaining<CreateUserDTO>();
        builder.Services.AddValidatorsFromAssemblyContaining<CreateDeckDTO>();        
    }
}
