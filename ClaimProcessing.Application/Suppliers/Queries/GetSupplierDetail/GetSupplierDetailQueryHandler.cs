using ClaimProcessing.Application.Common.Interfaces;
using ClaimProcessing.Domain.Entities;
using ClaimProcessing.Shared.Suppliers.Queries.GetSupplierDetail;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace ClaimProcessing.Application.Suppliers.Queries.GetSupplierDetail
{
    public class GetSupplierDetailQueryHandler(IClaimProcessingDbContext _context) : IRequestHandler<GetSupplierDetailQuery, SupplierDetailVm>
    {
        public async Task<SupplierDetailVm> Handle(GetSupplierDetailQuery request, CancellationToken cancellationToken)
        {
            var supplier = await _context.Suppliers
                .Where(p => p.StatusId != 0 && p.Id == request.SupplierId)
                .FirstOrDefaultAsync(cancellationToken);

            if(supplier == null) { return null; }

            var suppliertVm = Map(supplier);

            return suppliertVm;
        }
        private static SupplierDetailVm Map(Supplier supplier)
        {
            return new SupplierDetailVm
            {
                Name = supplier.Name,
                Address = supplier.Address.ToString(),
                ContactPerson = supplier.ContactPerson.ToString()
            };
        }
    }
}
