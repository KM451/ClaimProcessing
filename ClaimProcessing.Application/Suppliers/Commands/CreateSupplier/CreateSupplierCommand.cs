using AutoMapper;
using ClaimProcessing.Application.Common.Mappings;
using ClaimProcessing.Domain.Entities;
using ClaimProcessing.Domain.ValueObjects;
using MediatR;

namespace ClaimProcessing.Application.Suppliers.Commands.CreateSupplier
{
    public class CreateSupplierCommand : IRequest<int>, IMapFrom<CreateSupplierCommand>
    {
        public string Name { get; set; }
        public string Street { get; set; }
        public string City { get; set; }
        public string Country { get; set; }
        public string ZipCode { get; set; }
        public string ContactPerson { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<CreateSupplierCommand, Supplier>(MemberList.None)
                .ForMember(s => s.Address, map => map.MapFrom(src => new Address(src.Street, src.City, src.Country, src.ZipCode)))
                .ForMember(s => s.ContactPerson, map => map.MapFrom(src => FullName.For(src.ContactPerson)));       
        }
    }
}
