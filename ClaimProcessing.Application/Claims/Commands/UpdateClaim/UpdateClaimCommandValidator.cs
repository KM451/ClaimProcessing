using FluentValidation;

namespace ClaimProcessing.Application.Claims.Commands.UpdateClaim
{
    public class UpdateClaimCommandValidator : AbstractValidator<UpdateClaimCommand>
    {
        public UpdateClaimCommandValidator()
        {
            RuleFor(c => c.ClaimNumber).NotEmpty().MaximumLength(40);
            RuleFor(c => c.OwnerType).NotEmpty().MaximumLength(40);
            RuleFor(c => c.ClaimType).NotEmpty().MaximumLength(40);
            RuleFor(c => c.ItemCode).NotEmpty().MaximumLength(40);
            RuleFor(c => c.Qty).NotNull().GreaterThan(0);
            RuleFor(c => c.CustomerName).MaximumLength(100);
            RuleFor(c => c.ItemName).MaximumLength(100);
            RuleFor(c => c.ClaimStatus).NotNull();
            RuleFor(c => c.RmaAvailable).NotNull();
        }
    }
}
