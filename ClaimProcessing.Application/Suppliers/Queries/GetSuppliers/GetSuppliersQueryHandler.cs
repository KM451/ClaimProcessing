using ClaimProcessing.Application.Common.Interfaces;
using ClaimProcessing.Application.Shipments.Queries.GetShipments;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClaimProcessing.Application.Suppliers.Queries.GetSuppliers
{
    public class GetSuppliersQueryHandler : IRequestHandler<GetSuppliersQuery, SuppliersVm>
    {
        private readonly IClaimProcessingDbContext _context;

        public GetSuppliersQueryHandler(IClaimProcessingDbContext claimProcessingDbContext)
        {
            _context = claimProcessingDbContext;
        }
        public async Task<SuppliersVm> Handle(GetSuppliersQuery request, CancellationToken cancellationToken)
        {
            var suppliers = await _context.Suppliers
                .Where(p => p.StatusId != 0)
                .ToListAsync(cancellationToken);

            var suppliersVm = new SuppliersVm
            {
                Suppliers = suppliers.Select(s => new SuppliersDto
                {
                    SupplierId = s.Id,
                    Name = s.Name,
                    City = s.Address.City,
                }).ToList()
            };
            return suppliersVm;
        }
    }
}
