using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using app.DTOs;
using FluentValidation;

namespace app.Validators;

public class CreateDeckDTOValidator : AbstractValidator<CreateDeckDTO>
{
    public CreateDeckDTOValidator()
    {
        RuleFor(deck => deck.Name).NotEmpty().NotNull().MinimumLength(4);
        RuleFor(deck => deck.CommanderCardId).NotEmpty().NotNull();
    }
}
