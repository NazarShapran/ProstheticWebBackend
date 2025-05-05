using Api.Dtos.TypeDtos;
using FluentValidation;

namespace Api.Modules.Validators;

public class ProstheticTypeDtoValidator : AbstractValidator<ProstheticTypeDto>
{
    public ProstheticTypeDtoValidator()
    {
        RuleFor(x => x.Title).NotEmpty().MaximumLength(255).MinimumLength(3);
    }
}