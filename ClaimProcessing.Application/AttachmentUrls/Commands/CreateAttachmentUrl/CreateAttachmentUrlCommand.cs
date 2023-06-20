using AutoMapper;
using ClaimProcessing.Application.Common.Mappings;
using ClaimProcessing.Domain.Entities;
using MediatR;

namespace ClaimProcessing.Application.AttachmentUrls.Commands.CreateAttachmentUrl
{
    public class CreateAttachmentUrlCommand : IRequest<int>, IMapFrom<CreateAttachmentUrlCommand>
    {
        public int ClaimId { get; set; }
        public string Path { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<CreateAttachmentUrlCommand, AttachmentUrl>();
        }
    }
}
