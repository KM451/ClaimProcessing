using AutoMapper;
using ClaimProcessing.Application.Common.Mappings;
using ClaimProcessing.Domain.Entities;

namespace ClaimProcessing.Application.Shipments.Queries.GetShipmentClaims
{
    public class ShipmentClaimsDto : IMapFrom<Claim>
    {
        public int ClaimId { get; set; }
        public string OwnerType { get; set; }
        public string ClaimType { get; set; }
        public string ItemCode { get; set; }
        public decimal Qty { get; set; }
        public string? CustomerName { get; set; }
        public string? ItemName { get; set; }
        public int ShipmentId { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<Claim, ShipmentClaimsDto>()
                .ForMember(s => s.ClaimId, map => map.MapFrom(src => src.Id));
        }
    }
}
