using ClaimProcessing.Shared.Packagings.Commands.UpdatePackaging;
using FluentValidation;

namespace ClaimProcessing.Application.Packagings.Commands.UpdatePackaging
{
    public class UpdatePackagingCommandValidator : AbstractValidator<UpdatePackagingCommand>
    {
        public UpdatePackagingCommandValidator()
        {
            RuleFor(a => a.PackagingId).NotNull().GreaterThan(0);
            RuleFor(a => a.Type).NotEmpty().MaximumLength(40);
            RuleFor(a => a.Height).NotNull().GreaterThan(0);
            RuleFor(a => a.Width).NotNull().GreaterThan(0);
            RuleFor(a => a.Depth).NotNull().GreaterThan(0);
            RuleFor(a => a.Weight).NotNull().GreaterThan(0);
            RuleFor(a => a.ShipmentId).NotNull().GreaterThan(0);
        }
    }
}
