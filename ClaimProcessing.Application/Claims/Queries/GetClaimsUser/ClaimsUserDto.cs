using AutoMapper;
using ClaimProcessing.Application.Common.Mappings;
using ClaimProcessing.Domain.Entities;

namespace ClaimProcessing.Application.Claims.Queries.GetClaimsUser
{
    public class ClaimsUserDto : IMapFrom<Claim>
    {
        public string ClaimNumber { get; set; }
        public DateTime ClaimCreationDate { get; set; }
        public string ItemCode { get; set; }
        public string ItemName { get; set; }
        public int ClaimStatus { get; set; }
      
        public void Mapping(Profile profile)
        {
            profile.CreateMap<Claim, ClaimsUserDto>()
                .ForMember(s => s.ClaimCreationDate, map => map.MapFrom(src => src.Created));
        }
    }
}
