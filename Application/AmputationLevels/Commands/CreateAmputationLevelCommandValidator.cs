using Application.Materials.Commands;
using FluentValidation;

namespace Application.AmputationLevels.Commands;

public class CreateAmputationLevelCommandValidator : AbstractValidator<CreateMaterialCommand>
{
    public CreateAmputationLevelCommandValidator()
    {
        RuleFor(x => x.Title).NotEmpty().MaximumLength(255).MinimumLength(3);
    }
}