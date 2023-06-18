using AutoMapper;
using ClaimProcessing.Application.Common.Mappings;
using ClaimProcessing.Domain.Entities;

namespace ClaimProcessing.Application.Suppliers.Queries.GetSupplierDetail
{
    public class SupplierDetailVm : IMapFrom<Supplier>
    {
        public string Name { get; set; }
        public string Address { get; set; }
        public string ContactPerson { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<Supplier, SupplierDetailVm>()
                .ForMember(s => s.Address, map => map.MapFrom(src => src.Address.ToString()))
                .ForMember(s => s.ContactPerson, map => map.MapFrom(src => src.ContactPerson.ToString()));

        }
    }
}
