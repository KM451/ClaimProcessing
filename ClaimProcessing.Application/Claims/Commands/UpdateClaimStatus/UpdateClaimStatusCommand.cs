using AutoMapper;
using ClaimProcessing.Application.Common.Mappings;
using ClaimProcessing.Domain.Entities;
using MediatR;

namespace ClaimProcessing.Application.Claims.Commands.UpdateClaimStatus
{
    public class UpdateClaimStatusCommand : IRequest, IMapFrom<UpdateClaimStatusCommand>
    {
        public int ClaimId { get; set; }
        public int ClaimStatus { get; set; }
        

        public void Mapping(Profile profile)
        {
            profile.CreateMap<UpdateClaimStatusCommand, Claim>();
        }

    }
}
