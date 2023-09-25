using AutoMapper;
using ClaimProcessing.Application.Common.Mappings;
using ClaimProcessing.Domain.Entities;
using MediatR;

namespace ClaimProcessing.Application.Shipments.Commands.UpdateShipment
{
    public class UpdateShipmentCommand : IRequest<int>, IMapFrom<UpdateShipmentCommand>
    {
        public int ShipmentId { get; private set; }
        public DateTime ShipmentDate { get; set; }
        public int SupplierId { get; set; }
        public string Speditor { get; set; }
        public string ShippingDocumentNo { get; set; }
        public decimal TotalWeight { get; set; }
        
        public void SetId(int id)
        {
            ShipmentId = id;
        }
        public void Mapping(Profile profile)
        {
            profile.CreateMap<UpdateShipmentCommand, Shipment>(MemberList.Source)
                .ForMember(s => s.Id, map => map.MapFrom(src => src.ShipmentId));
        }
    }
}
