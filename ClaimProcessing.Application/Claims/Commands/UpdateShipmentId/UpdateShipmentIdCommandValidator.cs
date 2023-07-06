using FluentValidation;

namespace ClaimProcessing.Application.Claims.Commands.UpdateShipmentId
{
    public class UpdateShipmentIdCommandValidator : AbstractValidator<UpdateShipmentIdCommand>
    {
        public UpdateShipmentIdCommandValidator()
        {
            RuleFor(a => a.ClaimId).NotNull().GreaterThan(0);
            RuleFor(a => a.ShipmentId).NotNull().GreaterThan(0);
        }
    }
}
