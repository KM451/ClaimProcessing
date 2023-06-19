using FluentValidation;


namespace ClaimProcessing.Application.Claims.Commands.CreateClaim
{
    public class CreateClaimComandValidator : AbstractValidator<CreateClaimCommand>
    {
        public CreateClaimComandValidator()
        {
                RuleFor(x => x.ItemName).MinimumLength(5);
        }
    }
}
