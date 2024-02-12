using ClaimProcessing.Shared.Claims.Commands.UpdateRmaAvailability;
using FluentValidation;

namespace ClaimProcessing.Application.Claims.Commands.UpdateRmaAvailability
{
    public class UpdateRmaAvailabilityCommandValidator : AbstractValidator<UpdateRmaAvailabilityCommand>
    {
        public UpdateRmaAvailabilityCommandValidator()
        {
            RuleFor(a => a.ClaimId).NotNull().GreaterThan(0);
            RuleFor(c => c.RmaAvailable).NotNull();
        }
    }
}
