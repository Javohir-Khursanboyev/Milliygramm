using FluentValidation;
using FluentValidation.Validators;
using Milliygramm.Model.DTOs.GroupDetails;

namespace Milliygramm.Service.Validators.GroupDetails;

public sealed class GroupDetailUpdateModelValidator : AbstractValidator<GroupDetailUpdateModel>
{
    public GroupDetailUpdateModelValidator()
    {

        RuleFor(detail => detail.Description).MaximumLength(1000).WithMessage("Deskription must not exceed 1000 characters.");
        RuleFor(detail => detail.Privacy).NotNull().WithMessage("It is necessary to specify the status of the group");
        RuleFor(detail => detail.Link).NotNull().WithMessage("Enter a unique name for the group")
            .MaximumLength(255).WithMessage("Link must not exceed 1000 characters");
    }
}
