using FluentValidation;
using NicoKingdomManager.Shared.Models.Requests;

namespace NicoKingdomManager.BusinessLayer.Validators;

public class SaveUserValidator : AbstractValidator<SaveUserRequest>
{
    public SaveUserValidator()
    {
        RuleFor(u => u.UserName)
            .NotNull()
            .NotEmpty()
            .WithMessage("the username is required");
    }
}