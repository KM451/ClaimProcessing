using ClaimProcessing.Shared.Claims.Commands.UpdateClaimStatus;
using FluentValidation;

namespace ClaimProcessing.Application.Claims.Commands.UpdateClaimStatus
{
    public class UpdateClaimStatusCommandValidator : AbstractValidator<UpdateClaimStatusCommand>
    {
        public UpdateClaimStatusCommandValidator()
        {
            RuleFor(a => a.ClaimId).NotNull().GreaterThan(0);
            RuleFor(c => c.ClaimStatus).NotNull();
        }
    }
}
