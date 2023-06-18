using MediatR;

namespace ClaimProcessing.Application.Shipments.Commands.CreateShipment
{
    public class CreateShipmentCommand : IRequest<int>
    {
        public DateTime ShipmentDate { get; set; }
        public int SupplierID { get; set; }

    }
}
