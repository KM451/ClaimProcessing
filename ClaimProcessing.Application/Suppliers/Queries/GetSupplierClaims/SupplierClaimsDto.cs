using AutoMapper;
using ClaimProcessing.Application.Common.Mappings;
using ClaimProcessing.Domain.Entities;

namespace ClaimProcessing.Application.Suppliers.Queries.GetSupplierClaims
{
    public class SupplierClaimsDto : IMapFrom<Claim>
    {
        public int ClaimId { get; set; }
        public string OwnerType { get; set; }
        public string ClaimType { get; set; }
        public string ItemCode { get; set; }
        public decimal Qty { get; set; }
        public string SupplierName { get; set; }
        public string? CustomerName { get; set; }
        public string? ItemName { get; set; }
        public int ClaimStatus { get; set; }
        public bool RmaAvailable { get; set; }
        public DateTime? ShipmentDate { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<Claim, SupplierClaimsDto>()
                .ForMember(s => s.ClaimId, map => map.MapFrom(src => src.Id))
                .ForMember(s => s.SupplierName, map => map.MapFrom(src => src.Supplier.Name))
                .ForMember(s => s.ShipmentDate, map => map.MapFrom(src => src.Shipment.ShipmentDate));
        }
    }
}
