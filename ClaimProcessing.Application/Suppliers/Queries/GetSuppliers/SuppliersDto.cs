using AutoMapper;
using ClaimProcessing.Application.Common.Mappings;
using ClaimProcessing.Domain.Entities;

namespace ClaimProcessing.Application.Suppliers.Queries.GetSuppliers
{
    public class SuppliersDto : IMapFrom<Supplier>
    {
        public int SupplierId { get; set; }
        public string Name { get; set; }
        public string City { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<Supplier, SuppliersDto>()
                .ForMember(s => s.SupplierId, map => map.MapFrom(src => src.Id))
                .ForMember(s => s.City, map => map.MapFrom(src => src.Address.City));    
        }
    }
}
