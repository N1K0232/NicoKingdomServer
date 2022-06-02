using FluentValidation;
using NicoKingdomManager.Shared.Models.Requests;

namespace NicoKingdomManager.BusinessLayer.Validators;

public class SaveLogValidator : AbstractValidator<SaveLogRequest>
{
    public SaveLogValidator()
    {
        RuleFor(log => log.UserName)
            .NotNull()
            .NotEmpty()
            .WithMessage("you must enter your username");

        RuleFor(log => log.Title)
            .NotNull()
            .NotEmpty()
            .WithMessage("you must enter the title");

        RuleFor(log => log.Action)
            .NotNull()
            .NotEmpty()
            .WithMessage("you must enter the action");

        RuleFor(log => log.Description)
            .NotNull()
            .NotEmpty()
            .WithMessage("you must enter the description");

        RuleFor(log => log.UserName)
            .NotNull()
            .NotEmpty()
            .WithMessage("you must enter your username");

        RuleFor(log => log.LogDate)
            .NotNull()
            .NotEmpty()
            .WithMessage("you must enter the date of the log");

        RuleFor(log => log.LogType)
            .NotNull()
            .NotEmpty()
            .WithMessage("you must add the type of the log");
    }
}