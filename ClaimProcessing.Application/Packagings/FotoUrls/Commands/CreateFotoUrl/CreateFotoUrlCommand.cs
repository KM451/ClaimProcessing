using AutoMapper;
using ClaimProcessing.Application.Common.Mappings;
using ClaimProcessing.Domain.Entities;
using MediatR;

namespace ClaimProcessing.Application.FotoUrls.Commands.CreateFotoUrl
{
    public class CreateFotoUrlCommand : IRequest<int>, IMapFrom<CreateFotoUrlCommand>
    {
        public int ClaimId { get; set; }
        public string Path { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<CreateFotoUrlCommand, FotoUrl>(MemberList.Source)
                .ForSourceMember(c => c.ClaimId, m => m.DoNotValidate());
        }
    }
}
