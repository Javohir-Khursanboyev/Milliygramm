using FluentValidation;
using Milliygramm.Model.DTOs.Groups;

namespace Milliygramm.Service.Validators.Groups;

public sealed class GroupUpdateModelValidator : AbstractValidator<GroupUpdateModel>
{
    public GroupUpdateModelValidator()
    {
        RuleFor(group => group.Name).MaximumLength(50).WithMessage("Name must not exceed 50 characters.");
    }
}

