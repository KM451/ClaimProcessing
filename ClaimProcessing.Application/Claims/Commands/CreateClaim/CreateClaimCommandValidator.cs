using ClaimProcessing.Shared.Claims.Commands.CreateClaim;
using FluentValidation;


namespace ClaimProcessing.Application.Claims.Commands.CreateClaim
{
    public class CreateClaimCommandValidator : AbstractValidator<CreateClaimCommand>
    {
        public CreateClaimCommandValidator()
        {
            RuleFor(c => c.ClaimNumber).NotEmpty().MaximumLength(40);
            RuleFor(c => c.OwnerType).NotEmpty().MaximumLength(40);
            RuleFor(c => c.ClaimType).NotEmpty().MaximumLength(40);
            RuleFor(c => c.ItemCode).NotEmpty().MaximumLength(40);
            RuleFor(c => c.Qty).NotNull().GreaterThan(0);
            RuleFor(c => c.CustomerName).MaximumLength(100);
            RuleFor(c => c.ItemName).MaximumLength(100);
            RuleFor(c => c.ClaimStatus).NotEmpty();
        }
    }
}
