using AutoMapper;
using ClaimProcessing.Application.Common.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace ClaimProcessing.Application.Suppliers.Queries.GetSupplierDetail
{
    public class GetSupplierDetailQueryHandler : IRequestHandler<GetSupplierDetailQuery, SupplierDetailVm>
    {
        private readonly IClaimProcessingDbContext _context;
        private IMapper _mapper;

        public GetSupplierDetailQueryHandler(IClaimProcessingDbContext claimProcessingDbContext, IMapper mapper)
        {
            _context = claimProcessingDbContext;
            _mapper = mapper;
        }
        public async Task<SupplierDetailVm> Handle(GetSupplierDetailQuery request, CancellationToken cancellationToken)
        {
            var supplier = await _context.Suppliers
                .Where(p => p.StatusId != 0 && p.Id == request.SupplierId)
                .FirstOrDefaultAsync(cancellationToken);

            var suppliertVm = _mapper.Map<SupplierDetailVm>(supplier);

            return suppliertVm;
        }
    }
}
