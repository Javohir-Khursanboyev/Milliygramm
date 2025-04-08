using FluentValidation;
using Milliygramm.Model.DTOs.Users;
using System.Text.RegularExpressions;

namespace Milliygramm.Service.Validators.Users;

public sealed class UserUpdateModelValidator : AbstractValidator<UserUpdateModel>
{
    public UserUpdateModelValidator()
    {
        RuleFor(user => user.FirstName)
            .NotEmpty().WithMessage("First name is required.")
            .MaximumLength(50).WithMessage("First name must not exceed 50 characters.");

        RuleFor(user => user.LastName)
            .NotEmpty().WithMessage("Last name is required.")
            .MaximumLength(50).WithMessage("Last name must not exceed 50 characters.");

        RuleFor(user => user.UserName)
            .NotEmpty().WithMessage("Username is required.")
            .MinimumLength(3).WithMessage("Username must be at least 3 characters long.")
            .MaximumLength(20).WithMessage("Username must not exceed 20 characters.")
            .Matches(@"^[a-zA-Z0-9_]+$").WithMessage("Username can only contain letters, numbers, and underscores.")
            .Must(username => !Regex.IsMatch(username, @"^[^a-zA-Z0-9]"))
            .WithMessage("Username cannot start with a special character.");

        RuleFor(user => user.UserDetail)
            .SetValidator(new UserDetailUpdateModelValidator())
            .When(user => user.UserDetail != null);
    }
}