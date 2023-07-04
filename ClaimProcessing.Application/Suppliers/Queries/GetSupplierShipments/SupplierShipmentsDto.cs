using AutoMapper;
using ClaimProcessing.Application.Common.Mappings;
using ClaimProcessing.Domain.Entities;

namespace ClaimProcessing.Application.Suppliers.Queries.GetSupplierShipments
{
    public class SupplierShipmentsDto : IMapFrom<Shipment>
    {
        public DateTime ShipmentDate { get; set; }
        public string Speditor { get; set; }
        public string ShippingDocumentNo { get; set; }
        public decimal TotalWeight { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<Shipment, SupplierShipmentsDto>();
        }
    }
}
