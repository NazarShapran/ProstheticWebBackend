using FluentValidation;

namespace Application.Functionalities.Commands;

public class CreateFunctionalityCommandValidator : AbstractValidator<CreateFunctionalityCommand>
{
    public CreateFunctionalityCommandValidator()
    {
        RuleFor(x => x.Title).NotEmpty().MaximumLength(255).MinimumLength(3);
    }
}