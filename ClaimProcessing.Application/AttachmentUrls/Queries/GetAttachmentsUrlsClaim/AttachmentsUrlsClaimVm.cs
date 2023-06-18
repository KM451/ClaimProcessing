using AutoMapper;
using ClaimProcessing.Application.AttachmentUrls.Queries.GetAttachmentUrlDetail;
using ClaimProcessing.Application.Common.Mappings;
using ClaimProcessing.Domain.Entities;

namespace ClaimProcessing.Application.AttachmentUrls.Queries.GetAttachmentsUrlsClaim
{
    public class AttachmentsUrlsClaimVm : IMapFrom<AttachmentUrl>
    {
        public ICollection<AttachmentsUrlsClaimDto> AttachmentUrls { get; set; }
        public void Mapping(Profile profile)
        {
            profile.CreateMap<AttachmentUrl, AttachmentsUrlsClaimVm>()
                .ForMember(a => a.AttachmentUrls, map => map.MapFrom(src => new AttachmentsUrlsClaimDto
                {
                    AttachmentUrlId = src.Id,
                    Path = src.Path
                }));
        }
    }
}
