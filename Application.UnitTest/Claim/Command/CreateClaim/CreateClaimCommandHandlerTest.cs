using Application.UnitTest.Common;
using ClaimProcessing.Application.Claims.Commands.CreateClaim;
using ClaimProcessing.Shared.Claims.Commands.CreateClaim;
using Microsoft.EntityFrameworkCore;
using Shouldly;

namespace Application.UnitTest.Claim.Command.CreateClaim
{
    public class CreateClaimCommandHandlerTest : CommandTestBase
    {
        private readonly CreateClaimCommandHandler _handler;

        public CreateClaimCommandHandlerTest()
            : base()
        {
            _handler = new CreateClaimCommandHandler(_context);
        }

        [Fact]
        public async Task Handle_GivenValidRequest_ShouldInsertClaim()
        {
            var command = new CreateClaimCommand()
            {
                ClaimNumber = "c1",
                OwnerType = "owner",
                ClaimType = "ct",
                ItemCode = " C22333",
                Qty = 1,
                CustomerName = "customer",
                ItemName = "item",
                ClaimDescription = "description",
                Remarks = "remarks",
                ClaimStatus = 3,
                SupplierId = 1,
                SaleInvoiceNo = "SI001/23",
                SaleDate = DateTime.Now,
                PurchaseInvoiceNo = "PI002/23",
                PurchaseDate = DateTime.Now,
                InternalDocNo = "ID001"
            };

            var result = await _handler.Handle(command, CancellationToken.None);
            var dir = await _context.Claims.FirstAsync(x => x.Id == result, CancellationToken.None);
            dir.ShouldNotBeNull();
        }
    }
}
