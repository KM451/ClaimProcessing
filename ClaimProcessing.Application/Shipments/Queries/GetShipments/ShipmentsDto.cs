using AutoMapper;
using ClaimProcessing.Application.Common.Mappings;
using ClaimProcessing.Domain.Entities;

namespace ClaimProcessing.Application.Shipments.Queries.GetShipments
{
    public class ShipmentsDto : IMapFrom<Shipment>
    {
        public int ShipmentId { get; set; }
        public DateTime ShipmentDate { get; set; }
        public string SupplierName { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<Shipment, ShipmentsDto>()
                .ForMember(s => s.ShipmentId, map => map.MapFrom(src => src.Id))
                .ForMember(s => s.SupplierName, map => map.MapFrom(src => src.Supplier.Name));
        }
    }
}
