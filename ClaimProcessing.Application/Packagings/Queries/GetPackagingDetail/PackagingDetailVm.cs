using AutoMapper;
using ClaimProcessing.Application.Common.Mappings;
using ClaimProcessing.Domain.Entities;

namespace ClaimProcessing.Application.Packagings.Queries.GetPackagingDetail
{
    public class PackagingDetailVm : IMapFrom<Packaging>
    {
        public string Type { get; set; }
        public string Dimensions{ get; set; }
        public decimal Weight { get; set; }
        public string Notes { get; set; }
        public int ShipmentId { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<Packaging, PackagingDetailVm>()
                .ForMember(d => d.Dimensions, map => map.MapFrom(d => d.Dimensions.ToString()));
        }
    }
}
