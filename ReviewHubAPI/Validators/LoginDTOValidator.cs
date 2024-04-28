using FluentValidation;
using ReviewHubAPI.Models.DTO;

namespace ReviewHubAPI.Validators;

public class LoginDTOValidator : AbstractValidator<LoginDTO>
{
    public LoginDTOValidator()
    {
        RuleFor(x => x.Username)
            .NotEmpty().WithMessage("Username is required.")
            .MaximumLength(50).WithMessage("Username can not be more than 50 characters long.");

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
