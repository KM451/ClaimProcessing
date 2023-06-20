using AutoMapper;
using ClaimProcessing.Application.Common.Interfaces;
using ClaimProcessing.Domain.Entities;
using MediatR;

namespace ClaimProcessing.Application.Claims.Commands.CreateClaim
{
    public class CreateClaimCommandHandler : IRequestHandler<CreateClaimCommand, int>
    {
        private readonly IClaimProcessingDbContext _context;
        private IMapper _mapper;
        public CreateClaimCommandHandler(IClaimProcessingDbContext claimProcessingDbContext, IMapper mapper)
        {
            _context = claimProcessingDbContext;
            _mapper = mapper;
        }
        public async Task<int> Handle(CreateClaimCommand request, CancellationToken cancellationToken)
        {
            var claim = _mapper.Map<Claim>(request);
            
            _context.Claims.Add(claim);
            await _context.SaveChangesAsync(cancellationToken);

            if (request.PurchaseInvoiceNo != null) 
            {
                var purchaseDetail = _mapper.Map<PurchaseDetail>(request);
                purchaseDetail.ClaimId = claim.Id;
               
                _context.PurchaseDetails.Add(purchaseDetail);
                await _context.SaveChangesAsync(cancellationToken);
            }
            if (request.SaleInvoiceNo != null)
            {
                var saleDetail = _mapper.Map<SaleDetail>(request);
                saleDetail.ClaimId = claim.Id;
                
                _context.SaleDetails.Add(saleDetail);
                await _context.SaveChangesAsync(cancellationToken);
            }
            
            return claim.Id;
        }
    }
}
