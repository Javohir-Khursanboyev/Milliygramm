using FluentValidation;
using Milliygramm.Model.DTOs.GroupDetails;
namespace Milliygramm.Service.Validators.GroupDetails;

public sealed class GroupDetailCreateModelValidator : AbstractValidator<GroupDetailCreateModel>
{
    public GroupDetailCreateModelValidator()
    {
        RuleFor(detail => detail.Description).MaximumLength(1000).WithMessage("Deskription must not exceed 1000 characters.");
        RuleFor(detail => detail.Privacy).NotNull().WithMessage("It is necessary to specify the status of the group");
        RuleFor(detail => detail.Link).NotNull().WithMessage("Enter a unique name for the group")
            .MaximumLength(255).WithMessage("Link must not exceed 1000 characters");
    }
}
