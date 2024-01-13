using Application.UnitTest.Common;
using ClaimProcessing.Application.Shipments.Queries.GetShipments;
using ClaimProcessing.Persistance;
using ClaimProcessing.Shared.Shipments.Queries.GetShipments;
using Shouldly;

namespace Application.UnitTest.Shipment.Queries.GetShipments
{
    [Collection("QueryCollection")]
    public class GetShipmentsQueryHandlerTests
    {
        private readonly ClaimProcessingDbContext _context;

        public GetShipmentsQueryHandlerTests(QueryTestFixtures fixtures)
        {
            _context = fixtures.Context;
        }

        [Fact]

        public async Task GetShipments()
        {
            var handler = new GetShipmentsQueryHandler(_context);
            var result = await handler.Handle(new GetShipmentsQuery(), CancellationToken.None);

            result.ShouldBeOfType<ShipmentsVm>();
            result.Shipments.FirstOrDefault().ShipmentId.ShouldBe(1);
        }
    }
}
