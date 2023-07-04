using AutoMapper;
using ClaimProcessing.Application.Common.Mappings;
using ClaimProcessing.Domain.Entities;

namespace ClaimProcessing.Application.SerialNumbers.Queries.GetSerialNumber
{
    public class SerialNumberVm : IMapFrom<SerialNumber>
    {
        public string Value { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<SerialNumber, SerialNumberVm>();
        }
    }
}
