using AutoMapper;
using ClaimProcessing.Application.Common.Mappings;
using ClaimProcessing.Domain.Entities;

namespace ClaimProcessing.Application.Claims.Queries.GetClaimAttachmentsUrls
{
    public class ClaimAttachmentUrlsDto : IMapFrom<AttachmentUrl>
    {
        public string Path { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<AttachmentUrl, ClaimAttachmentUrlsDto>();
        }
    }
}
