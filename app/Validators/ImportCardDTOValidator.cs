using app.DTOs;
using FluentValidation;

namespace app.Validators;

public class ImportCardDTOValidator : AbstractValidator<ImportCardDTO>
{
    public ImportCardDTOValidator()
    {
        RuleFor(card => card.Id).NotNull().NotEmpty().Length(36);
    }
}