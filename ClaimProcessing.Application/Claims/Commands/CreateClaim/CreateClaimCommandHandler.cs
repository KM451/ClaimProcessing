using ClaimProcessing.Application.Common.Interfaces;
using ClaimProcessing.Domain.Entities;
using MediatR;

namespace ClaimProcessing.Application.Claims.Commands.CreateClaim
{
    public class CreateClaimCommandHandler : IRequestHandler<CreateClaimCommand, int>
    {
        private readonly IClaimProcessingDbContext _context;
        public CreateClaimCommandHandler(IClaimProcessingDbContext claimProcessingDbContext)
        {
            _context = claimProcessingDbContext;
        }
        public async Task<int> Handle(CreateClaimCommand request, CancellationToken cancellationToken)
        {
            Claim claim = new()
            {
                OwnerType = request.OwnerType,
                ClaimType = request.ClaimType,
                ItemCode = request.ItemCode,
                Qty = request.Qty,
                CustomerName = request.CustomerName,
                ItemName = request.ItemName,
                ClaimDescription = request.ClaimDescription,
                Remarks = request.Remarks,
                ClaimStatus = "created",
                RmaAvailable = false,
                SupplierId = request.SupplierId,
            };
            _context.Claims.Add(claim);

            if (request.PurchaseInvoiceNo != null) 
            {
                PurchaseDetail purchaseDetail = new()
                {
                    PurchaseInvoiceNo = request.PurchaseInvoiceNo,
                    PurchaseDate = request?.PurchaseDate ?? DateTime.MinValue,
                    InternalDocNo = request?.InternalDocNo,
                    ClaimId = claim.Id,
                };
                _context.PurchaseDetails.Add(purchaseDetail);
            }
            if (request.SaleInvoiceNo != null)
            {
                SaleDetail saleDetail = new()
                {
                    SaleInvoiceNo = request.SaleInvoiceNo,
                    SaleDate = request?.SaleDate ?? DateTime.MinValue,
                    ClaimId = claim.Id,
                };
                _context.SaleDetails.Add(saleDetail);
            }
            await _context.SaveChangesAsync(cancellationToken);

            return claim.Id;
        }
    }
}
