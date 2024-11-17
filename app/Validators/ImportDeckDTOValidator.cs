using app.DTOs;
using FluentValidation;

namespace app.Validators;

public class ImportDeckDTOValidator : AbstractValidator<ImportDeckDTO>
{
    public ImportDeckDTOValidator()
    {
        RuleFor(deck => deck.Name).NotEmpty().NotNull().MinimumLength(4);
        RuleFor(deck => deck.CommanderCardId).NotEmpty().NotNull();
        RuleForEach(deck => deck.CardsDTO).SetValidator(new ImportCardDTOValidator());
    }
}