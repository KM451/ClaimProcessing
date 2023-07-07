using FluentValidation;

namespace ClaimProcessing.Application.SerialNumbers.Commands.CreateSerialNumber
{
    public class CreateSerialNumberCommandValidator : AbstractValidator<CreateSerialNumberCommand>
    {
        public CreateSerialNumberCommandValidator()
        {
            RuleFor(a => a.ClaimId).NotNull().GreaterThan(0);
            RuleFor(a => a.Value).NotEmpty().MaximumLength(50);
        }
    }
}
