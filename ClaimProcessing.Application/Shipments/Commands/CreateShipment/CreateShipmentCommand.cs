using AutoMapper;
using ClaimProcessing.Application.Common.Mappings;
using ClaimProcessing.Domain.Entities;
using MediatR;

namespace ClaimProcessing.Application.Shipments.Commands.CreateShipment
{
    public class CreateShipmentCommand : IRequest<int>, IMapFrom<CreateShipmentCommand>
    {
        public DateTime ShipmentDate { get; set; }
        public string Speditor { get; set; }
        public string ShippingDocumentNo { get; set; }
        public decimal TotalWeight { get; set; }
        public int SupplierId { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<CreateShipmentCommand, Shipment>();
        }
    }
}
