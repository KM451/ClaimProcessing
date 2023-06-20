using AutoMapper;
using ClaimProcessing.Application.AttachmentUrls.Commands.CreateAttachmentUrl;
using ClaimProcessing.Application.Common.Mappings;
using ClaimProcessing.Domain.Entities;
using MediatR;

namespace ClaimProcessing.Application.AttachmentUrls.Commands.UpdateAttachmentUrl
{
    public class UpdateAttachmentUrlCommand : IRequest, IMapFrom<CreateAttachmentUrlCommand>
    {
        public int AttachmentUrlId { get; set; }
        public int ClaimId { get; set; }
        public string Path { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<CreateAttachmentUrlCommand, AttachmentUrl>();
        }
    }
}
