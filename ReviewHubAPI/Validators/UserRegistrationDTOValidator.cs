using FluentValidation;
using ReviewHubAPI.Models.DTO;

namespace ReviewHubAPI.Validators;

public class UserRegistrationDTOValidator : AbstractValidator<UserRegistrationDTO>
{
    public UserRegistrationDTOValidator()
    {
        RuleFor(x => x.Username)
            .NotEmpty().WithMessage("Username is required")
            .MinimumLength(4).WithMessage("Username must have at least 4 characters")
            .MaximumLength(30).WithMessage("Username can not be longer than 30 signs");

        RuleFor(x => x.Email)
            .NotEmpty().WithMessage("Email is required")
            .EmailAddress().WithMessage("A valid email is required")
            .MaximumLength(50).WithMessage("Email can not be longer than 50 characters"); 

        RuleFor(x => x.Password)
            .NotEmpty().WithMessage("Password is required")
            .MinimumLength(8).WithMessage("Password must contain at least 8 characters")
            .MaximumLength(100).WithMessage("Password can not be longer than 100 characters")
            .Matches(@"[0-9]+").WithMessage("Password must contain at least 1 number")
            .Matches(@"[A-Z]+").WithMessage("Password must contain at least 1 uppercase letter")
            .Matches(@"[a-z]").WithMessage("Password must contain at least 1 lowercase letter")
            .Matches(@"[^a-zA-Z0-9]+").WithMessage("Password must contain at least one special character");
    }
}
