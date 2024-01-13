using ClaimProcessing.Application.Common.Interfaces;
using ClaimProcessing.Domain.Entities;
using ClaimProcessing.Shared.Suppliers.Queries.GetSuppliers;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace ClaimProcessing.Application.Suppliers.Queries.GetSuppliers
{
    public class GetSuppliersQueryHandler(IClaimProcessingDbContext _context) : IRequestHandler<GetSuppliersQuery, SuppliersVm>
    {
        public async Task<SuppliersVm> Handle(GetSuppliersQuery request, CancellationToken cancellationToken)
        {
            var suppliers = await _context.Suppliers
                .Where(p => p.StatusId != 0)
                .ToListAsync(cancellationToken);

            var suppliersVm = new SuppliersVm
            {
                Suppliers = suppliers.Select(src => Map(src)).ToList()
            };

            return suppliersVm;
        }

        private SuppliersDto Map(Supplier supplier)
        {
            return new SuppliersDto
            {
                SupplierId = supplier.Id,
                Name = supplier.Name,
                City = supplier.Address.City
            };
        }
    }
}
