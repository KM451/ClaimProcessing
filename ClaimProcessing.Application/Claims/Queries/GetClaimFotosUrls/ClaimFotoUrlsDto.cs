using AutoMapper;
using ClaimProcessing.Application.Common.Mappings;
using ClaimProcessing.Domain.Entities;

namespace ClaimProcessing.Application.Claims.Queries.GetClaimFotosUrls
{
    public class ClaimFotoUrlsDto : IMapFrom<FotoUrl>
    {
        public string Path { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<FotoUrl, ClaimFotoUrlsDto>();
        }
    }
}
