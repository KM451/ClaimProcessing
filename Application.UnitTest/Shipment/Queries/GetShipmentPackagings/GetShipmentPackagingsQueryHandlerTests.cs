using Application.UnitTest.Common;
using ClaimProcessing.Application.Shipments.Queries.GetShipmentPackagings;
using ClaimProcessing.Persistance;
using ClaimProcessing.Shared.Shipments.Queries.GetShipmentPackagings;
using Shouldly;

namespace Application.UnitTest.Shipment.Queries.GetShipmentPackagings
{
    [Collection("QueryCollection")]
    public class GetShipmentPackagingsQueryHandlerTests
    {
        private readonly ClaimProcessingDbContext _context;

        public GetShipmentPackagingsQueryHandlerTests(QueryTestFixtures fixtures)
        {
            _context = fixtures.Context;
        }

        [Fact]

        public async Task GetPackagingsByShipmentId()
        {
            var handler = new GetShipmentPackagingsQueryHandler(_context);
            var result = await handler.Handle(new GetShipmentPackagingsQuery { ShipmentId = 1 }, CancellationToken.None);

            result.ShouldBeOfType<ShipmentPackagingsVm>();
            result.Packagings.Select(p => p.Weight).FirstOrDefault().ShouldBe(20);
        }
    }
}
