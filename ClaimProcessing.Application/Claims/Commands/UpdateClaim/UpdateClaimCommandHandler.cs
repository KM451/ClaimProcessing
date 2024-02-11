using ClaimProcessing.Application.Common.Interfaces;
using ClaimProcessing.Domain.Entities;
using ClaimProcessing.Shared.Claims.Commands.UpdateClaim;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace ClaimProcessing.Application.Claims.Commands.UpdateClaim
{
    public class UpdateClaimCommandHandler(IClaimProcessingDbContext _context) : IRequestHandler<UpdateClaimCommand, int>
    {
        public async Task<int> Handle(UpdateClaimCommand request, CancellationToken cancellationToken)
        {
            var claim = await _context.Claims.Include(x => x.SaleDetail).Include(x => x.PurchaseDetail).Where(c => c.StatusId != 0 && c.Id == request.ClaimId).FirstOrDefaultAsync(cancellationToken);

            if (claim == null)
            {
                claim = Map(request);
                claim.SaleDetail = MapS(request);
                claim.SaleDetail.ClaimId = claim.Id;
                claim.PurchaseDetail = MapP(request);
                claim.PurchaseDetail.ClaimId = claim.Id;
                _context.Claims.Add(claim);
            }
            else
            {
                claim.ClaimNumber = request.ClaimNumber;
                claim.OwnerType = request.OwnerType;
                claim.ClaimType = request.ClaimType;
                claim.ItemCode = request.ItemCode;
                claim.Qty = request.Qty;
                claim.CustomerName = request.CustomerName;
                claim.ItemName = request.ItemName;
                claim.ClaimDescription = request.ClaimDescription;
                claim.Remarks = request.Remarks;
                claim.ClaimStatus = request.ClaimStatus;
                claim.RmaAvailable = request.RmaAvailable;
                claim.ShipmentId = request.ShipmentId;
                claim.SupplierId = request.SupplierId;
                claim.SaleDetail.SaleInvoiceNo = request.SaleInvoiceNo;
                claim.SaleDetail.SaleDate = request.SaleDate;
                claim.PurchaseDetail.PurchaseInvoiceNo = request.PurchaseInvoiceNo;
                claim.PurchaseDetail.PurchaseDate = request.PurchaseDate;
                claim.PurchaseDetail.InternalDocNo = request.InternalDocNo;
            }

            await _context.SaveChangesAsync(cancellationToken);

            return claim.Id;

        }

        private Claim Map(UpdateClaimCommand claimCommand)
        {
            return new Claim
            {
                ClaimNumber = claimCommand.ClaimNumber,
                OwnerType = claimCommand.OwnerType,
                ClaimType = claimCommand.ClaimType,
                ItemCode = claimCommand.ItemCode,
                Qty = claimCommand.Qty,
                CustomerName = claimCommand.CustomerName,
                ItemName = claimCommand.ItemName,
                ClaimDescription = claimCommand.ClaimDescription,
                Remarks = claimCommand.Remarks,
                ClaimStatus = claimCommand.ClaimStatus,
                RmaAvailable = claimCommand.RmaAvailable,
                ShipmentId = claimCommand.ShipmentId,
                SupplierId = claimCommand.SupplierId,
                

            };
        }
        private PurchaseDetail MapP(UpdateClaimCommand claimCommand)
        {
            return new PurchaseDetail
            {
                PurchaseInvoiceNo = claimCommand.PurchaseInvoiceNo,
                PurchaseDate = claimCommand.PurchaseDate,
                InternalDocNo = claimCommand.InternalDocNo,
            };
        }
        private SaleDetail MapS(UpdateClaimCommand claimCommand)
        {
            return new SaleDetail
            {
                SaleInvoiceNo = claimCommand.SaleInvoiceNo,
                SaleDate = claimCommand.SaleDate,
            };
        }
    }
}
