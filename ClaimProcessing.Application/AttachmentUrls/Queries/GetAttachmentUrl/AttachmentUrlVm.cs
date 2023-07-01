using AutoMapper;
using ClaimProcessing.Application.Common.Mappings;
using ClaimProcessing.Domain.Entities;

namespace ClaimProcessing.Application.AttachmentUrls.Queries.GetAttachmentUrl
{
    public class AttachmentUrlVm : IMapFrom<AttachmentUrl>
    {
        public string Path { get; set; }


        public void Mapping(Profile profile)
        {
            profile.CreateMap<AttachmentUrl, AttachmentUrlVm>();
        }
    }
}
