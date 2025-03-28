using FluentValidation;
using Milliygramm.Model.DTOs.Groups;

namespace Milliygramm.Service.Validators.Groups;

public sealed class GroupCreatModelValidator : AbstractValidator<GroupCreatModel>
{
    public GroupCreatModelValidator()
    {
        RuleFor(group => group.Name).NotEmpty().WithMessage("Name is required.")
            .MaximumLength(50).WithMessage("Name must not exceed 50 characters.");
    }
}
