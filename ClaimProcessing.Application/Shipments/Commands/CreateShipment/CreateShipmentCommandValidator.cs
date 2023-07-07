using ClaimProcessing.Application.Common.Interfaces;
using FluentValidation;

namespace ClaimProcessing.Application.Shipments.Commands.CreateShipment
{
    public class CreateShipmentCommandValidator : AbstractValidator<CreateShipmentCommand>
    {
        private readonly IDateTime _dateTime;
        public CreateShipmentCommandValidator(IDateTime dateTime)
        {
            _dateTime = dateTime;
            RuleFor(a => a.ShipmentDate).NotNull().GreaterThanOrEqualTo(_dateTime.Now);
            RuleFor(a => a.Speditor).NotEmpty().MaximumLength(40);
            RuleFor(a => a.ShippingDocumentNo).NotEmpty().MaximumLength(50);
            RuleFor(a => a.TotalWeight).NotNull().GreaterThan(0);
            RuleFor(a => a.SupplierId).NotNull().GreaterThan(0);
            
        }
    }
}
