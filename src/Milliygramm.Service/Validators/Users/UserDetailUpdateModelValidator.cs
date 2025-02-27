using FluentValidation;
using Milliygramm.Model.DTOs.Users;

namespace Milliygramm.Service.Validators.Users;

public sealed class UserDetailUpdateModelValidator : AbstractValidator<UserDetailUpdateModel>
{
    public UserDetailUpdateModelValidator()
    {
        RuleFor(detail => detail.Bio)
            .MaximumLength(500).WithMessage("Bio must not exceed 500 characters.");

        RuleFor(detail => detail.Location)
            .MaximumLength(100).WithMessage("Location must not exceed 100 characters.");

        RuleFor(detail => detail.DataOfBirth)
            .LessThan(DateTime.UtcNow).WithMessage("Date of birth must be in the past.");
    }
}