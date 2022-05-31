using FluentValidation;
using NicoKingdomManager.Shared.Models.Requests;

namespace NicoKingdomManager.BusinessLayer.Validators;

public class SaveRoleValidator : AbstractValidator<SaveRoleRequest>
{
    public SaveRoleValidator()
    {
        RuleFor(u => u.Name)
            .NotNull()
            .NotEmpty()
            .WithMessage("the name of the role is required");
    }
}