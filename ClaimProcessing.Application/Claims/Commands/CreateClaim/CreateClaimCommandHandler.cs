using ClaimProcessing.Application.Common.Interfaces;
using ClaimProcessing.Domain.Entities;
using ClaimProcessing.Shared.Claims.Commands.CreateClaim;
using MediatR;


namespace ClaimProcessing.Application.Claims.Commands.CreateClaim
{
    public class CreateClaimCommandHandler(IClaimProcessingDbContext _context) : IRequestHandler<CreateClaimCommand, int>
    {
        public async Task<int> Handle(CreateClaimCommand request, CancellationToken cancellationToken)
        {
            var claim = Map(request);

            _context.Claims.Add(claim);
            await _context.SaveChangesAsync(cancellationToken);

            return claim.Id;
        }

        private Claim Map(CreateClaimCommand command)
        {
            return new Claim
            {
                ClaimNumber = command.ClaimNumber,
                OwnerType = command.OwnerType,
                ClaimType = command.ClaimType,
                ItemCode = command.ItemCode,
                Qty = command.Qty,
                CustomerName = command.CustomerName,
                ItemName = command.ItemName,
                ClaimDescription = command.ClaimDescription,
                Remarks = command.Remarks,
                ClaimStatus = command.ClaimStatus,
                SupplierId = command.SupplierId,
                SaleDetail = new SaleDetail 
                { 
                    SaleInvoiceNo = command.SaleInvoiceNo, 
                    SaleDate = command.SaleDate 
                },
                PurchaseDetail = new PurchaseDetail
                {
                    PurchaseInvoiceNo = command.PurchaseInvoiceNo,
                    PurchaseDate = command.PurchaseDate,
                    InternalDocNo = command.InternalDocNo
                }
            };
        }
    }
}
