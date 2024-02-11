using ClaimProcessing.Application.Common.Interfaces;
using ClaimProcessing.Shared.Claims.Queries.GetClaimDetail;
using ClaimProcessing.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace ClaimProcessing.Application.Claims.Queries.GetClaimDetail
{
    public class GetClaimDetailQueryHandler(IClaimProcessingDbContext _context) : IRequestHandler<GetClaimDetailQuery, ClaimDetailVm>
    {
        
        public async Task<ClaimDetailVm> Handle(GetClaimDetailQuery request, CancellationToken cancellationToken)
        {
            var claim = await _context.Claims
                .Where(p => p.StatusId != 0 && p.Id == request.ClaimId)
                .Include(c => c.SaleDetail)
                .Include(c => c.PurchaseDetail)
                .FirstOrDefaultAsync(cancellationToken);

            if(claim == null) { return null; }

            var claimVm = Map(claim);
                       
            return claimVm;
        }

        private ClaimDetailVm Map(Claim claim)
        {
            return new ClaimDetailVm
            {
                ClaimId = claim.Id,
                ClaimNumber = claim.ClaimNumber,
                OwnerType = claim.OwnerType,
                ClaimType = claim.ClaimType,
                ItemCode = claim.ItemCode,
                Qty = claim.Qty,
                SupplierId = claim.SupplierId,
                CustomerName = claim.CustomerName,
                ItemName = claim.ItemName,
                ClaimDescription = claim.ClaimDescription,
                Remarks = claim.Remarks,
                ClaimStatus = claim.ClaimStatus,
                RmaAvailable = claim.RmaAvailable,
                SaleInvoiceNo = claim.SaleDetail?.SaleInvoiceNo,
                SaleDate = claim.SaleDetail?.SaleDate,
                PurchaseInvoiceNo = claim.PurchaseDetail?.PurchaseInvoiceNo,
                PurchaseDate = claim.PurchaseDetail?.PurchaseDate,
                InternalDocNo = claim.PurchaseDetail?.InternalDocNo,
                ShipmentId = claim.ShipmentId
            };
        }
    }
}
