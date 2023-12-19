using AutoMapper;
using ClaimProcessing.Application.Common.Mappings;
using ClaimProcessing.Domain.Entities;
using MediatR;

namespace ClaimProcessing.Application.Claims.Commands.UpdateClaimRemarks
{
    public class UpdateClaimRemarksCommand : IMapFrom<UpdateClaimRemarksCommand>, IRequest<string>
    {
        public int ClaimId { get; set; }
        public string Remarks { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<UpdateClaimRemarksCommand, Claim>(MemberList.Source)
                .ForSourceMember(s => s.ClaimId, opts => opts.DoNotValidate());
        }
    }
}
