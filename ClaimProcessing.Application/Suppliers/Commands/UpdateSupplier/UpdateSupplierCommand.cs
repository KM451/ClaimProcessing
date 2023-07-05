using AutoMapper;
using ClaimProcessing.Application.Common.Mappings;
using ClaimProcessing.Domain.Entities;
using ClaimProcessing.Domain.ValueObjects;
using MediatR;

namespace ClaimProcessing.Application.Suppliers.Commands.UpdateSupplier
{
    public class UpdateSupplierCommand : IRequest<int>, IMapFrom<UpdateSupplierCommand>
    {
        public int SupplierId { get; set; }
        public string Name { get; set; }
        public string Street { get; set; }
        public string City { get; set; }
        public string Country { get; set; }
        public string ZipCode { get; set; }
        public string ContactPerson { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<UpdateSupplierCommand, Supplier>()
                .ForMember(s => s.Id, map => map.MapFrom(src => src.SupplierId))
                .ForMember(s => s.Address, map => map.MapFrom(src => new Address(src.Street, src.City, src.Country, src.ZipCode)))
                .ForMember(s => s.ContactPerson, map => map.MapFrom(src => FullName.For(src.ContactPerson)));
        }
    }
}
