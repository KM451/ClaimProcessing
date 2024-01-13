using Application.UnitTest.Common;
using ClaimProcessing.Application.Claims.Commands.UpdateClaim;
using ClaimProcessing.Shared.Claims.Commands.UpdateClaim;
using Microsoft.EntityFrameworkCore;
using Shouldly;

namespace Application.UnitTest.Claim.Command.UpdateClaim
{
    public class UpdateClaimCommandHandlerTest : CommandTestBase
    {
        private readonly UpdateClaimCommandHandler _handler;

        public UpdateClaimCommandHandlerTest()
            : base()
        {
            _handler = new UpdateClaimCommandHandler(_context);
        }

        [Fact]
        public async Task Handle_GivenValidRequest_ShouldUpdateClaim()
        {
            var command = new UpdateClaimCommand()
            {
                ClaimNumber = "test1",
                OwnerType = "test1",
                ClaimType = "test1",
                ItemCode = "test1",
                Qty = 3,
                CustomerName = "test1",
                ItemName = "test1",
                ClaimDescription = "test1",
                Remarks = "test1",
                ClaimStatus = 3,
                SupplierId = 3,
                SaleInvoiceNo = "test1",
                SaleDate = new DateTime(2000, 1, 2),
                PurchaseInvoiceNo = "test1",
                PurchaseDate = new DateTime(2000, 1, 3),
                InternalDocNo = "test1",
                RmaAvailable = true,
                ShipmentId = 3,
            };

            var result = await _handler.Handle(command, CancellationToken.None);
            var c = await _context.Claims.FirstAsync(x => x.Id == result, CancellationToken.None);

            c.ClaimNumber.ShouldBe("test1");
            c.OwnerType.ShouldBe("test1");
            c.ClaimType.ShouldBe("test1");
            c.ItemCode.ShouldBe("test1");
            c.CustomerName.ShouldBe("test1");
            c.ItemName.ShouldBe("test1");
            c.ClaimDescription.ShouldBe("test1");
            c.Remarks.ShouldBe("test1");
            c.Qty.ShouldBe(3);
            c.ClaimStatus.ShouldBe(3);
            c.SupplierId.ShouldBe(3);
            c.ShipmentId.ShouldBe(3);
            c.RmaAvailable.ShouldBe(true);

            var sd = await _context.SaleDetails.FirstAsync(x => x.ClaimId == result, CancellationToken.None);
            if (sd != null)
            {
                sd.SaleInvoiceNo.ShouldBe("test1");
                sd.SaleDate.ShouldBe(new DateTime(2000, 1, 2));
            }
            
            var pd = await _context.PurchaseDetails.FirstAsync(x => x.ClaimId == result, CancellationToken.None);
            if (pd != null)
            {
                pd.PurchaseInvoiceNo.ShouldBe("test1");
                pd.InternalDocNo.ShouldBe("test1");
                pd.PurchaseDate.ShouldBe(new DateTime(2000, 1, 3));
            }
        }
    }
}
