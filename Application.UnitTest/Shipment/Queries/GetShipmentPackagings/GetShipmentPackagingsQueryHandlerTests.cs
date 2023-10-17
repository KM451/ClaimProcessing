using Application.UnitTest.Common;
using AutoMapper;
using ClaimProcessing.Application.Shipments.Queries.GetShipmentPackagings;
using ClaimProcessing.Persistance;
using Shouldly;

namespace Application.UnitTest.Shipment.Queries.GetShipmentPackagings
{
    [Collection("QueryCollection")]
    public class GetShipmentPackagingsQueryHandlerTests
    {
        private readonly ClaimProcessingDbContext _context;
        private readonly IMapper _mapper;

        public GetShipmentPackagingsQueryHandlerTests(QueryTestFixtures fixtures)
        {
            _context = fixtures.Context;
            _mapper = fixtures.Mapper;
        }

        [Fact]

        public async Task GetPackagingsByShipmentId()
        {
            var handler = new GetShipmentPackagingsQueryHandler(_context, _mapper);
            var result = await handler.Handle(new GetShipmentPackagingsQuery { ShipmentId = 1 }, CancellationToken.None);

            result.ShouldBeOfType<ShipmentPackagingsVm>();
            result.Packagings.Select(p => p.Weight).FirstOrDefault().ShouldBe(20);
        }
    }
}
