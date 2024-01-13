using Application.UnitTest.Common;
using ClaimProcessing.Application.Shipments.Queries.GetShipmentClaims;
using ClaimProcessing.Persistance;
using ClaimProcessing.Shared.Shipments.Queries.GetShipmentClaims;
using Shouldly;

namespace Application.UnitTest.Shipment.Queries.GetShipmentClaims
{
    [Collection("QueryCollection")]
    public class GetShipmentClaimsQueryHandlerTests
    {
        private readonly ClaimProcessingDbContext _context;

        public GetShipmentClaimsQueryHandlerTests(QueryTestFixtures fixtures)
        {
            _context = fixtures.Context;
        }

        [Fact]

        public async Task GetClaimsByShipmentId()
        {
            var handler = new GetShipmentClaimsQueryHandler(_context);
            var result = await handler.Handle(new GetShipmentClaimsQuery { ShipmentId = 2 }, CancellationToken.None);

            result.ShouldBeOfType<ShipmentClaimsVm>();
            result.ShipmentClaims.Select(x => x.ClaimId).FirstOrDefault().ShouldBe(1);
        }
    }
}
