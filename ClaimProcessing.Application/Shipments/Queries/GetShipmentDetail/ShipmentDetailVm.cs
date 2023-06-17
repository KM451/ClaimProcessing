using AutoMapper;
using ClaimProcessing.Application.Common.Mappings;
using ClaimProcessing.Domain.Entities;

namespace ClaimProcessing.Application.Shipments.Queries.GetShipmentDetail
{
    public class ShipmentDetailVm : IMapFrom<Shipment>
    {
        public DateTime ShipmentDate { get; set; }
        public int SupplierID { get; set; }
        public string? Speditor { get; set; }
        public string? ShippingDocumentNo { get; set; }
        public decimal? TotalWeight { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<Shipment, ShipmentDetailVm>();
        }
    }
}
