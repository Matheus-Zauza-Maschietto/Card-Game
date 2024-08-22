using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using app.DTOs;
using app.Models;
using FluentValidation;

namespace app.Validators;

public class CreateUserDTOValidator : AbstractValidator<CreateUserDTO>
{
    public CreateUserDTOValidator()
    {
        RuleFor(user => user.Email).NotEmpty().NotNull().EmailAddress();
        RuleFor(user => user.LanguageId).NotNull().NotEqual(0);
        RuleFor(user => user.UserName).NotNull().NotEmpty();
    }
}
