using ClaimProcessing.Shared.Packagings.Commands.CreatePackaging;
using FluentValidation;

namespace ClaimProcessing.Application.Packagings.Commands.CreatePackaging
{
    public class CreatePackagingCommandValidator : AbstractValidator<CreatePackagingCommand>
    {
        public CreatePackagingCommandValidator()
        {
            RuleFor(a => a.Type).NotEmpty().MaximumLength(40);
            RuleFor(a => a.Height).NotNull().GreaterThan(0);
            RuleFor(a => a.Width).NotNull().GreaterThan(0);
            RuleFor(a => a.Depth).NotNull().GreaterThan(0);
            RuleFor(a => a.Weight).NotNull().GreaterThan(0);
            RuleFor(a => a.ShipmentId).NotNull().GreaterThan(0);
            
        }
    }
}
