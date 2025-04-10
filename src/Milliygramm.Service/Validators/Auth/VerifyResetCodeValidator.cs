using FluentValidation;
using Milliygramm.Model.DTOs.Auth;

namespace Milliygramm.Service.Validators.Auth;

public sealed class VerifyResetCodeValidator : AbstractValidator<VerifyResetCode>
{
    public VerifyResetCodeValidator()
    {
        RuleFor(x => x.Email)
            .NotEmpty().WithMessage("Email kiritilishi shart.")
            .EmailAddress().WithMessage("Email noto‘g‘ri formatda.");

        RuleFor(x => x.Code)
            .NotEmpty().WithMessage("Tasdiqlovchi kod kiritilishi shart.")
            .Length(5).WithMessage("Kod aniq 5 raqamdan iborat bo‘lishi kerak.")
            .Matches(@"^\d{5}$").WithMessage("Kod faqat raqamlardan iborat bo‘lishi kerak.");
    }
}