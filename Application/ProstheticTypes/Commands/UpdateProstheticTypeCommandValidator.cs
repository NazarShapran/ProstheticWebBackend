using Application.Materials.Commands;
using FluentValidation;

namespace Application.ProstheticTypes.Commands;

public class UpdateProstheticTypeCommandValidator : AbstractValidator<UpdateProstheticTypeCommand>
{
    public UpdateProstheticTypeCommandValidator()
    {
        RuleFor(x => x.ProstheticTypeId).NotEmpty().WithMessage("Id is required.");
        RuleFor(x => x.Title).NotEmpty().MaximumLength(255).MinimumLength(3);
    }
}