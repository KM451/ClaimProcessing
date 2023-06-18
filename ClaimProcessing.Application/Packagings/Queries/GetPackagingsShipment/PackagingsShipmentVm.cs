using AutoMapper;
using ClaimProcessing.Application.Common.Mappings;
using ClaimProcessing.Domain.Entities;

namespace ClaimProcessing.Application.Packagings.Queries.GetPackagingsShipment
{
    public class PackagingsShipmentVm : IMapFrom<Packaging>
    {
        public ICollection<PackagingsShipmentDto> Packagings { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<Packaging, PackagingsShipmentVm>()
                .ForMember(p => p.Packagings, map => map.MapFrom(src => new PackagingsShipmentDto
                {
                    Type = src.Type,
                    Dimensions = src.Dimensions.ToString(),
                    Weight = src.Weight
                }));
        }
    }
}
