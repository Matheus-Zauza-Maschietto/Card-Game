using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using app.DTOs;
using FluentValidation;

namespace app.Validators;

public class LoginUserDTOValidator : AbstractValidator<LoginUserDTO>
{
    public LoginUserDTOValidator()
    {
        RuleFor(user => user.Email).NotEmpty().NotNull().EmailAddress();
        RuleFor(user => user.Password).NotEmpty().NotNull();
    }
}
