using AutoMapper;
using ClaimProcessing.Application.Common.Mappings;
using ClaimProcessing.Domain.Entities;
using MediatR;

namespace ClaimProcessing.Application.Claims.Commands.UpdateRmaAvailability
{
    public class UpdateRmaAvailabilityCommand : IRequest, IMapFrom<UpdateRmaAvailabilityCommand>
    {
        public int ClaimId { get; set; }
        public bool RmaAvailable { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<UpdateRmaAvailabilityCommand, Claim>(MemberList.Source)
                .ForSourceMember(s => s.ClaimId, opts => opts.DoNotValidate());
        }
    }
}
