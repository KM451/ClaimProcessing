using AutoMapper;
using ClaimProcessing.Application.Common.Mappings;
using ClaimProcessing.Domain.Entities;

namespace ClaimProcessing.Application.Suppliers.Queries.GetSuppliers
{
    public class SuppliersVm : IMapFrom<Supplier>
    {
        public ICollection<SuppliersDto> Suppliers { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<Supplier, SuppliersVm>()
                .ForMember(s => s.Suppliers, map => map.MapFrom(src => new SuppliersDto
                {
                    SupplierId = src.Id,
                    Name = src.Name,
                    City = src.Address.City
                }));
        }
    }
}
