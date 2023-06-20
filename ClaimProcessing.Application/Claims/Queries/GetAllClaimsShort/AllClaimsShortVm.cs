using AutoMapper;
using ClaimProcessing.Application.AttachmentUrls.Queries.GetAttachmentsUrlsClaim;
using ClaimProcessing.Application.Common.Mappings;
using ClaimProcessing.Domain.Entities;

namespace ClaimProcessing.Application.Claims.Queries.GetAllClaimsShort
{
    public class AllClaimsShortVm : IMapFrom<Claim>
    {
        public ICollection<AllClaimsShortDto> Claims { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<Claim, AllClaimsShortVm>()
                .ForMember(a => a.Claims, map => map.MapFrom(src => new AllClaimsShortDto
                {
                    ClaimNumber = src.ClaimNumber,
                    ClaimCreationDate = src.Created,
                    SupplierName = src.Supplier.Name,
                    ItemCode = src.ItemCode,
                    ClaimStatus = src.ClaimStatus
                }));
        }
    }
}
