using FluentValidation;

namespace Application.Users.Commands;

public class UpdateUserDetailsCommandValidator : AbstractValidator<UpdateUserDetailsCommand>
{
    public UpdateUserDetailsCommandValidator()
    {
        RuleFor(x => x.UserId)
            .NotEmpty().WithMessage("User ID is required.");
            
        RuleFor(x => x.FullName)
            .NotEmpty()
            .MaximumLength(255)
            .MinimumLength(3);
            
        RuleFor(x => x.Email)
            .NotEmpty()
            .EmailAddress().WithMessage("Invalid email format");
        
        RuleFor(x => x.PhoneNumber)
            .NotEmpty()
            .Matches(@"^\+?[0-9]{10,15}$").WithMessage("Phone number must be between 10 and 15 digits long and can start with a '+' sign");
            
        RuleFor(x => x.BirthDate)
            .NotEmpty().WithMessage("Birth date is required.");
    }
} 