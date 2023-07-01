using AutoMapper;
using ClaimProcessing.Application.Common.Mappings;
using ClaimProcessing.Domain.Entities;

namespace ClaimProcessing.Application.Claims.Queries.GetClaimSerialNumbers
{
    public class ClaimSerialNumbersDto : IMapFrom<SerialNumber>
    {
        public string Value { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<SerialNumber, ClaimSerialNumbersDto>();
        }
    }
}
