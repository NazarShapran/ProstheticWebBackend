using Application.Materials.Commands;
using FluentValidation;

namespace Application.AmputationLevels.Commands;

public class UpdateAmputationLevelCommandValidator : AbstractValidator<UpdateMaterialCommand>
{
    public UpdateAmputationLevelCommandValidator()
    {
        RuleFor(x => x.MaterialId).NotEmpty().WithMessage("Id is required.");
        RuleFor(x => x.Title).NotEmpty().MaximumLength(255).MinimumLength(3);
    }
}