using FluentValidation;
using Milliygramm.Model.DTOs.Auth;

namespace Milliygramm.Service.Validators.Auth;

public sealed class ResetPasswordValidator : AbstractValidator<ResetPasswordModel>
{
    public ResetPasswordValidator()
    {
        RuleFor(x => x.Email)
            .NotEmpty().WithMessage("Email kiritilishi shart.")
            .EmailAddress().WithMessage("Email noto‘g‘ri formatda.");

        RuleFor(x => x.Code)
            .NotEmpty().WithMessage("Tasdiqlovchi kod kiritilishi shart.")
            .Length(5).WithMessage("Kod aniq 5 raqamdan iborat bo‘lishi kerak.")
            .Matches(@"^\d{5}$").WithMessage("Kod faqat raqamlardan iborat bo‘lishi kerak.");

        RuleFor(x => x.NewPassword)
            .NotEmpty().WithMessage("Yangi parol kiritilishi shart.")
            .MinimumLength(8).WithMessage("Parol kamida 8 belgidan iborat bo‘lishi kerak.")
            .Matches("[A-Z]").WithMessage("Kamida bitta katta harf bo‘lishi kerak.")
            .Matches("[a-z]").WithMessage("Kamida bitta kichik harf bo‘lishi kerak.")
            .Matches(@"\d").WithMessage("Kamida bitta raqam bo‘lishi kerak.");

        RuleFor(x => x.ConfirmPassword)
            .NotEmpty().WithMessage("Parolni tasdiqlang.")
            .Equal(x => x.NewPassword).WithMessage("Tasdiqlovchi parol yangi parol bilan mos emas.");
    }
}