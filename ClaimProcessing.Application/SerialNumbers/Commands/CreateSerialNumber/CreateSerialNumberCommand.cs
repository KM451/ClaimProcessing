using AutoMapper;
using ClaimProcessing.Application.Common.Mappings;
using ClaimProcessing.Domain.Entities;
using MediatR;

namespace ClaimProcessing.Application.SerialNumbers.Commands.CreateSerialNumber
{
    public class CreateSerialNumberCommand : IRequest<int>, IMapFrom<CreateSerialNumberCommand>
    {
        public int ClaimId { get; set; }
        public string Value { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<CreateSerialNumberCommand, SerialNumber>(MemberList.Source)
                .ForSourceMember(c => c.ClaimId, m => m.DoNotValidate());
        }
    }
}
