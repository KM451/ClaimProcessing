using Application.UnitTest.Common;
using AutoMapper;
using ClaimProcessing.Application.Shipments.Queries.GetShipments;
using ClaimProcessing.Persistance;
using Shouldly;

namespace Application.UnitTest.Shipment.Queries.GetShipments
{
    [Collection("QueryCollection")]
    public class GetShipmentsQueryHandlerTests
    {
        private readonly ClaimProcessingDbContext _context;
        private readonly IMapper _mapper;

        public GetShipmentsQueryHandlerTests(QueryTestFixtures fixtures)
        {
            _context = fixtures.Context;
            _mapper = fixtures.Mapper;
        }

        [Fact]

        public async Task GetShipments()
        {
            var handler = new GetShipmentsQueryHandler(_context, _mapper);
            var result = await handler.Handle(new GetShipmentsQuery(), CancellationToken.None);

            result.ShouldBeOfType<ShipmentsVm>();
            result.Shipments.FirstOrDefault().ShipmentId.ShouldBe(1);
        }
    }
}
