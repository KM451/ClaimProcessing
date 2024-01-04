using FluentValidation;

namespace ClaimProcessing.Application.FotoUrls.Commands.CreateFotoUrl
{
    public class CreateFotoUrlCommandValidator : AbstractValidator<CreateFotoUrlCommand>
    {
        public CreateFotoUrlCommandValidator()
        {
            RuleFor(a => a.ClaimId).NotNull().GreaterThan(0);
            RuleFor(a => a.Path).NotEmpty().MaximumLength(200);
        }
    }
}
