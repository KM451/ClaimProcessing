using AutoMapper;
using ClaimProcessing.Application.Common.Mappings;
using ClaimProcessing.Domain.Entities;

namespace ClaimProcessing.Application.FotoUrls.Queries.GetFotoUrlDetail
{
    public class FotoUrlDetailVm : IMapFrom<FotoUrl>
    {
        public string Path { get; set; }

        public void Mapping(Profile profile) 
        {
            profile.CreateMap<FotoUrl, FotoUrlDetailVm>();
        }
    }
}
