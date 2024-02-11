using Application.UnitTest.Common;
using ClaimProcessing.Application.Shipments.Queries.GetShipmentDetail;
using ClaimProcessing.Persistance;
using ClaimProcessing.Shared.Shipments.Queries.GetShipmentDetail;
using Shouldly;

namespace Application.UnitTest.Shipment.Queries.GetShipmentDetail
{
    [Collection("QueryCollection")]
    public class GetShipmentDetailQueryHandlerTests
    {
        private readonly ClaimProcessingDbContext _context;

        public GetShipmentDetailQueryHandlerTests(QueryTestFixtures fixtures)
        {
            _context = fixtures.Context;
        }

        [Fact]

        public async Task GetShipmentDetailById()
        {
            var handler = new GetShipmentDetailQueryHandler(_context);
            var result = await handler.Handle(new GetShipmentDetailQuery { ShipmentId = 1 }, CancellationToken.None);

            result.ShouldBeOfType<ShipmentDetailVm>();
            result.ShippingDocumentNo.ShouldBe("A1234XYZ");
        }
    }
}
