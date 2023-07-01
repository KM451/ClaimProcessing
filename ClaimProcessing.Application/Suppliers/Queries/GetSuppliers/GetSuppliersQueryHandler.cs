using AutoMapper;
using ClaimProcessing.Application.Common.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace ClaimProcessing.Application.Suppliers.Queries.GetSuppliers
{
    public class GetSuppliersQueryHandler : IRequestHandler<GetSuppliersQuery, SuppliersVm>
    {
        private readonly IClaimProcessingDbContext _context;
        private IMapper _mapper;

        public GetSuppliersQueryHandler(IClaimProcessingDbContext claimProcessingDbContext, IMapper mapper)
        {
            _context = claimProcessingDbContext;
            _mapper = mapper;
        }
        public async Task<SuppliersVm> Handle(GetSuppliersQuery request, CancellationToken cancellationToken)
        {
            var suppliers = await _context.Suppliers
                .Where(p => p.StatusId != 0)
                .ToListAsync(cancellationToken);

            var suppliersVm = new SuppliersVm
            {
                Suppliers = suppliers.Select(src => _mapper.Map<SuppliersDto>(src)).ToList()
            };

            return suppliersVm;
        }
    }
}
