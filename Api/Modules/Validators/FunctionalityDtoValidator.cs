using Api.Dtos.FunctionalityDtos;
using FluentValidation;

namespace Api.Modules.Validators;

public class FunctionalityDtoValidator : AbstractValidator<FunctionalityDto>
{
    public FunctionalityDtoValidator()
    {
        RuleFor(x => x.Title).NotEmpty().MaximumLength(255).MinimumLength(3);
    }
}