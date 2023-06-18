using AutoMapper;
using ClaimProcessing.Application.Common.Mappings;
using ClaimProcessing.Domain.Entities;

namespace ClaimProcessing.Application.FotoUrls.Queries.GetFotosUrlsClaim
{
    public class FotosUrlsClaimVm : IMapFrom<FotoUrl>
    {
        public ICollection<FotosUrlsClaimDto> FotoUrls { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<FotoUrl, FotosUrlsClaimVm>()
                .ForMember(f => f.FotoUrls, map => map.MapFrom(src => new FotosUrlsClaimDto
                {
                    FotoUrlId = src.Id,
                    Path = src.Path
                }));
        }
    }
}
