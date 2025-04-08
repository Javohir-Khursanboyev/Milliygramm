using FluentValidation;
using Milliygramm.Model.DTOs.Users;

namespace Milliygramm.Service.Validators.Users;

public sealed class ChangeEmailValidator : AbstractValidator<ChangeEmail>
{
    public ChangeEmailValidator()
    {
        RuleFor(email => email.Email)
            .NotEmpty().WithMessage("Email is required.")
            .EmailAddress().WithMessage("Invalid email format.");
    }
}
