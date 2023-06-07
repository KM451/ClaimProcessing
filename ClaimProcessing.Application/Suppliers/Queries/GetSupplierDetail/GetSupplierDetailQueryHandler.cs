using ClaimProcessing.Application.Common.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClaimProcessing.Application.Suppliers.Queries.GetSupplierDetail
{
    public class GetSupplierDetailQueryHandler : IRequestHandler<GetSupplierDetailQuery, SupplierDetailVm>
    {
        private readonly IClaimProcessingDbContext _context;
        public GetSupplierDetailQueryHandler(IClaimProcessingDbContext claimProcessingDbContext)
        {
            _context = claimProcessingDbContext;
        }
        public async Task<SupplierDetailVm> Handle(GetSupplierDetailQuery request, CancellationToken cancellationToken)
        {
            var supplier = await _context.Suppliers
                .Where(p => p.StatusId != 0 && p.Id == request.SupplierId)
                .FirstOrDefaultAsync(cancellationToken);

            var suppliertVm = new SupplierDetailVm
            {
                Name = supplier.Name,
                Address = supplier.Address.ToString(),
                ContactPerson = supplier.ContactPerson.ToString(),
            };

            return suppliertVm;
        }
    }
}
