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
        private readonly ICurrentUserService _userService;

        public GetSupplierDetailQueryHandler(IClaimProcessingDbContext claimProcessingDbContext, IMapper mapper, ICurrentUserService userService)
        {
            _context = claimProcessingDbContext;
            _mapper = mapper;
            _userService = userService;
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
