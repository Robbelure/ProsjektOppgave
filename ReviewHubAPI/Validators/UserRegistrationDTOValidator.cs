using FluentValidation;
using ReviewHubAPI.Models.DTO;
using System.Text.RegularExpressions;

namespace ReviewHubAPI.Validators;

public class UserRegistrationDTOValidator : AbstractValidator<UserRegistrationDTO>
{
    public UserRegistrationDTOValidator()
    {
        RuleFor(x => x.Username)
            .NotEmpty().WithMessage("Username is required")
            .MaximumLength(50).WithMessage("Username can not be longer than 50 characters");

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


    /// <summary>
    /// Denne listen representerer en samling av sensitive ord som bør unngås i brukernavn.
    /// Fungerer slik at 
    /// Gadd ikke liste opp en haug med fæle ord, men dere skjønner poenget :D
    /// </summary>
    private static readonly List<string> sensitiveWords = new List<string> { "jævel", "fuck", "helvete" };

    public static bool ValidUsername(string username)
    {
        string pattern = @"\b(" + string.Join("|", sensitiveWords) + @")\b";
        Regex regex = new Regex(pattern, RegexOptions.IgnoreCase);
        return !regex.IsMatch(username);
    }
}
