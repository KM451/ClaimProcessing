using AutoMapper;
using ClaimProcessing.Application.Common.Mappings;
using ClaimProcessing.Domain.Entities;

namespace ClaimProcessing.Application.Shipments.Queries.GetShipmentPackagings
{
    public class ShipmentPackagingsDto : IMapFrom<Packaging>
    {
        public string Type { get; set; }
        public string Dimensions { get; set; }
        public decimal Weight { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<Packaging, ShipmentPackagingsDto>()
                .ForMember(p => p.Dimensions, map => map.MapFrom(src => src.Dimensions.ToString()));
        }
    }
}
