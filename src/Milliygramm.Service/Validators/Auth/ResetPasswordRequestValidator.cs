using FluentValidation;
using Milliygramm.Model.DTOs.Auth;

namespace Milliygramm.Service.Validators.Auth;

public sealed class ResetPasswordRequestValidator : AbstractValidator<ResetPasswordRequest>
{
    public ResetPasswordRequestValidator()
    {
        RuleFor(x => x.Email)
            .NotEmpty().WithMessage("Email kiritilishi shart.")
            .EmailAddress().WithMessage("Email noto‘g‘ri formatda.");
    }
}