using AutoMapper;
using ClaimProcessing.Application.Common.Mappings;
using ClaimProcessing.Domain.Entities;

namespace ClaimProcessing.Application.SerialNumbers.Queries.GetSerialNumbersClaim
{
    public class SerialNumbersClaimVm : IMapFrom<SerialNumber>
    {
        public ICollection<SerialNumbersClaimDto> SerialNumbers { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<SerialNumber, SerialNumbersClaimVm>()
                .ForMember(s => s.SerialNumbers, map => map.MapFrom(src => new SerialNumbersClaimDto
                {
                    SerialNumberId = src.Id,
                    Value = src.Value
                }));
        }
    }
}
