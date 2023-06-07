using ClaimProcessing.Application.Common.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace ClaimProcessing.Application.Claims.Queries.GetClaimDetail
{
    public class GetClaimDetailQueryHandler : IRequestHandler<GetClaimDetailQuery, ClaimDetailVm>
    {
        private readonly IClaimProcessingDbContext _context;
        public GetClaimDetailQueryHandler(IClaimProcessingDbContext claimProcessingDbContext)
        {
            _context = claimProcessingDbContext;        
        }
        public async Task<ClaimDetailVm> Handle(GetClaimDetailQuery request, CancellationToken cancellationToken)
        {
            var claim = await _context.Claims
                .Where(p=>p.StatusId!=0 && p.Id == request.ClaimId)
                .Include(c=>c.Supplier)
                .Include(c=>c.SaleDetail)
                .Include(c=>c.PurchaseDetail)
                .Include(c=>c.Shipment)
                .FirstOrDefaultAsync(cancellationToken);

            var claimVm = new ClaimDetailVm
            {
                OwnerType = claim.OwnerType,
                ClaimType = claim.ClaimType,
                ItemCode = claim.ItemCode,
                Qty = claim.Qty,
                SupplierName = claim.Supplier.Name,
                CustomerName = claim.CustomerName,
                SaleInvoiceNo = claim.SaleDetail.SaleInvoiceNo,
                SaleDate = claim.SaleDetail.SaleDate,
                PurchaseInvoiceNo = claim.PurchaseDetail.PurchaseInvoiceNo,
                PurchaseDate = claim.PurchaseDetail.PurchaseDate,
                InternalDocNo = claim.PurchaseDetail.InternalDocNo,
                ItemName = claim.ItemName,
                ClaimDescription = claim.ClaimDescription,
                Remarks = claim.Remarks,
                ClaimStatus = claim.ClaimStatus,
                RmaAvailable = claim.RmaAvailable,
                ShipmentDate = claim.Shipment.ShipmentDate,

            };

            return claimVm;
        }
    }
}
