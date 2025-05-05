using Application.Materials.Commands;
using FluentValidation;

namespace Application.ProstheticTypes.Commands;

public class CreateProstheticTypeCommandValidator : AbstractValidator<CreateProstheticTypeCommand>
{
    public CreateProstheticTypeCommandValidator()
    {
        RuleFor(x => x.Title).NotEmpty().MaximumLength(255).MinimumLength(3);
    }
}