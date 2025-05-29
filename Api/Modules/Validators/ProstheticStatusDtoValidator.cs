using Api.Dtos.ProstheticStatusDtos;
using FluentValidation;

namespace Api.Modules.Validators;

public class ProstheticStatusDtoValidator : AbstractValidator<ProstheticStatusDto>
{
    public ProstheticStatusDtoValidator()
    {
        RuleFor(x => x.Title)
            .NotEmpty().WithMessage("Title is required.")
            .Length(1, 100).WithMessage("Title must be between 1 and 100 characters.");
    }
} 