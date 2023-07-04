using AutoMapper;
using ClaimProcessing.Application.Common.Mappings;
using ClaimProcessing.Domain.Entities;

namespace ClaimProcessing.Application.Claims.Queries.GetAllClaimsShort
{
    public class AllClaimsShortDto : IMapFrom<Claim>
    {
        public int ClaimId { get; set; }
        public string ClaimNumber { get; set; }
        public DateTime ClaimCreationDate { get; set; }
        public string SupplierName { get; set;}
        public string ItemCode { get; set;}
        public int ClaimStatus { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<Claim, AllClaimsShortDto>()
                .ForMember(s => s.ClaimId, map => map.MapFrom(src => src.Id))
                .ForMember(s => s.SupplierName, map => map.MapFrom(src => src.Supplier.Name));
        }
    }
}
