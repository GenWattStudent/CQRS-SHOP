using CarShop.Application.Commands.CarCommands;
using FluentValidation;

namespace CarShop.Application.Validators;

public  class CreateAddCommandValidator : AbstractValidator<CreateCarCommand>
{
    public CreateAddCommandValidator()
    {
        RuleFor(x => x.Brand)
            .NotEmpty()
            .MaximumLength(50);

        RuleFor(x => x.Model)
            .NotEmpty()
            .MaximumLength(50);

        RuleFor(x => x.Year)
            .NotEmpty()
            .InclusiveBetween(1900, 2100);

        RuleFor(x => x.Color)
            .NotEmpty()
            .MaximumLength(50);

        RuleFor(x => x.VIN)
            .NotEmpty()
            .MaximumLength(50);

        RuleFor(x => x.Price)
            .NotEmpty()
            .InclusiveBetween(0, 1000000);
    }
}
