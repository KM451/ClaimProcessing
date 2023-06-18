using AutoMapper;
using ClaimProcessing.Application.Common.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace ClaimProcessing.Application.Claims.Queries.GetClaimDetail
{
    public class GetClaimDetailQueryHandler : IRequestHandler<GetClaimDetailQuery, ClaimDetailVm>
    {
        private readonly IClaimProcessingDbContext _context;
        private IMapper _mapper;

        public GetClaimDetailQueryHandler(IClaimProcessingDbContext claimProcessingDbContext, IMapper mapper)
        {
            _context = claimProcessingDbContext;
            _mapper = mapper;
        }
        public async Task<ClaimDetailVm> Handle(GetClaimDetailQuery request, CancellationToken cancellationToken)
        {
            var claim = await _context.Claims
                .Where(p => p.StatusId != 0 && p.Id == request.ClaimId)
                .Include(c => c.Supplier)
                .Include(c => c.SaleDetail)
                .Include(c => c.PurchaseDetail)
                .Include(c => c.Shipment)
                .FirstOrDefaultAsync(cancellationToken);

            var claimVm = _mapper.Map<ClaimDetailVm>(claim);
                       

            return claimVm;
        }
    }
}
