using FluentValidation;
using Pactra.Api.Application.DTOs;

namespace Pactra.Api.Application.Validators;
public class CreateEngagementRequestValidator : AbstractValidator<CreateEngagementRequest>
{
    public CreateEngagementRequestValidator()
    {
        RuleFor(x => x.Description).NotEmpty().MaximumLength(1000);
        RuleFor(x => x.Goals).NotEmpty().MaximumLength(1000);
        RuleFor(x => x.RequestedFeatures).NotEmpty().MaximumLength(1000);
        RuleFor(x => x.Constraints).NotEmpty().MaximumLength(1000);
        RuleFor(x => x.Budget).GreaterThan(0).When(x => x.Budget.HasValue);
        RuleFor(x => x.DesiredStartDate).GreaterThan(DateTimeOffset.Now).When(x => x.DesiredStartDate.HasValue);
        RuleFor(x => x.AdditionalInfo).MaximumLength(1000);
    }
}