using FluentValidation;

namespace Application.Functionalities.Commands;

public class UpdateFunctionalityCommandValidator : AbstractValidator<UpdateFunctionalityCommand>
{
    public UpdateFunctionalityCommandValidator()
    {
        RuleFor(x => x.FuncId).NotEmpty().WithMessage("Id is required.");
        RuleFor(x => x.Tittle).NotEmpty().MaximumLength(255).MinimumLength(3);
    }
}