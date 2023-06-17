using AutoMapper;
using ClaimProcessing.Application.Common.Mappings;
using ClaimProcessing.Domain.Entities;

namespace ClaimProcessing.Application.Shipments.Queries.GetShipments
{
    public class ShipmentsVm : IMapFrom<Shipment>
    {
        public ICollection<ShipmentsDto> Shipments { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<Shipment, ShipmentsVm>()
                .ForMember(s => s.Shipments, map => map.MapFrom(src => new ShipmentsDto
                {
                    ShipmentId = src.Id,
                    ShipmentDate = src.ShipmentDate,
                    SupplierName = src.Supplier.Name
                }));
        }
    }
}
