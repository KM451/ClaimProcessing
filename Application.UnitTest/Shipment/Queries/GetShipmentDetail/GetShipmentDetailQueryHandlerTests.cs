using Application.UnitTest.Common;
using AutoMapper;
using ClaimProcessing.Application.Shipments.Queries.GetShipmentDetail;
using ClaimProcessing.Persistance;
using Shouldly;

namespace Application.UnitTest.Shipment.Queries.GetShipmentDetail
{
    [Collection("QueryCollection")]
    public class GetShipmentDetailQueryHandlerTests
    {
        private readonly ClaimProcessingDbContext _context;
        private readonly IMapper _mapper;

        public GetShipmentDetailQueryHandlerTests(QueryTestFixtures fixtures)
        {
            _context = fixtures.Context;
            _mapper = fixtures.Mapper;
        }

        [Fact]

        public async Task GetShipmentDetailById()
        {
            var handler = new GetShipmentDetailQueryHandler(_context, _mapper);
            var result = await handler.Handle(new GetShipmentDetailQuery { ShipmentId = 1 }, CancellationToken.None);

            result.ShouldBeOfType<ShipmentDetailVm>();
            result.ShippingDocumentNo.ShouldBe("A1234XYZ");
        }
    }
}
