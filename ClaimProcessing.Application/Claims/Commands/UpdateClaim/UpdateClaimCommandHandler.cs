using ClaimProcessing.Application.Common.Interfaces;
using ClaimProcessing.Domain.Exceptions;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace ClaimProcessing.Application.Claims.Commands.UpdateClaim
{
    public class UpdateClaimCommandHandler : IRequestHandler<UpdateClaimCommand>
    {
        private readonly IClaimProcessingDbContext _context;
        public UpdateClaimCommandHandler(IClaimProcessingDbContext claimProcessingDbContext)
        {
            _context = claimProcessingDbContext;
        }
        public async Task Handle(UpdateClaimCommand request, CancellationToken cancellationToken)
        {
            var claim = await _context.Claims.Where(c => c.Id == request.ClaimId).FirstOrDefaultAsync(cancellationToken);

            if (claim == null)
            {
                throw new NullException(request.ClaimId);
            }
            else
            {
                claim.OwnerType = request.OwnerType;
                claim.ClaimType = request.ClaimType;
                claim.ItemCode = request.ItemCode;
                claim.Qty = request.Qty;
                claim.CustomerName = request.CustomerName;
                claim.ItemName = request.ItemName;
                claim.ClaimDescription = request.ClaimDescription;
                claim.Remarks = request.Remarks;
                claim.ClaimStatus = request.ClaimStatus;
                claim.SupplierId = request.SupplierId;
                claim.SaleDetail.SaleInvoiceNo = request?.SaleInvoiceNo ?? "";
                claim.SaleDetail.SaleDate = request?.SaleDate ?? DateTime.MinValue;
                claim.PurchaseDetail.PurchaseInvoiceNo = request?.PurchaseInvoiceNo ?? "";
                claim.PurchaseDetail.PurchaseDate = request?.PurchaseDate ?? DateTime.MinValue;
                claim.PurchaseDetail.InternalDocNo = request?.InternalDocNo ?? "";

                _context.Claims.Update(claim);
                await _context.SaveChangesAsync(cancellationToken);
            }

            
        }
    }
}
