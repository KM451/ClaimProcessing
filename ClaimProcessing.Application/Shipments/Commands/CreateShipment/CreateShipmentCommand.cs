using AutoMapper;
using ClaimProcessing.Application.Common.Mappings;
using ClaimProcessing.Domain.Entities;
using MediatR;

namespace ClaimProcessing.Application.Shipments.Commands.CreateShipment
{
    public class CreateShipmentCommand : IRequest<int>, IMapFrom<CreateShipmentCommand>
    {
        public DateTime ShipmentDate { get; set; }
        public int SupplierID { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<CreateShipmentCommand, Shipment>();
        }
    }
}
