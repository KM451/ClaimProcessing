using FluentValidation;

namespace ClaimProcessing.Application.Claims.Commands.UpdateClaimRemarks
{
    public class UpdateClaimRemarksCommandValidator : AbstractValidator<UpdateClaimRemarksCommand>
    {
        public UpdateClaimRemarksCommandValidator()
        {
            RuleFor(a => a.ClaimId).NotNull().GreaterThan(0);
            RuleFor(c => c.Remarks).NotNull();
        }
    }
}
