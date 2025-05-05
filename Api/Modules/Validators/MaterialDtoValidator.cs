using Api.Dtos.MaterialDtos;
using FluentValidation;

namespace Api.Modules.Validators;

public class MaterialDtoValidator : AbstractValidator<MaterialDto>
{
    public MaterialDtoValidator()
    {
        RuleFor(x => x.Title).NotEmpty().MaximumLength(255).MinimumLength(3);
    }
}