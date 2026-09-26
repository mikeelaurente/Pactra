using FluentValidation;
using Pactra.Api.Application.DTOs;

namespace Pactra.Api.Application.Validators;
public class CreateEngagementRequestValidator : AbstractValidator<CreateEngagementRequest>
{
    public CreateEngagementRequestValidator()
    {
        RuleFor(x => x.Description)
            .NotEmpty()
            .MaximumLength(1000);

        RuleFor(x => x.Goals)
            .NotEmpty()
            .MaximumLength(1000);

        RuleFor(x => x.RequestedFeatures)
            .NotEmpty()
            .MaximumLength(1000);

        RuleFor(x => x.Constraints)
            .NotEmpty()
            .MaximumLength(1000);

        RuleFor(x => x.Budget)
            .GreaterThan(0)
            .When(x => x.Budget.HasValue)
            .WithMessage("Budget must be greater than 0.");

        RuleFor(x => x.DesiredStartDate)
            .GreaterThan(DateTimeOffset.UtcNow)
            .When(x => x.DesiredStartDate.HasValue)
            .WithMessage("Desired start date must be in the future.");

        RuleFor(x => x.AdditionalInfo)
            .MaximumLength(1000);
    }
}