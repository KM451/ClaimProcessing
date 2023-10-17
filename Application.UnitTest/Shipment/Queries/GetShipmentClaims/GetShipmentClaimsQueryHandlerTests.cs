using Application.UnitTest.Common;
using AutoMapper;
using ClaimProcessing.Application.Shipments.Queries.GetShipmentClaims;
using ClaimProcessing.Persistance;
using Shouldly;

namespace Application.UnitTest.Shipment.Queries.GetShipmentClaims
{
    [Collection("QueryCollection")]
    public class GetShipmentClaimsQueryHandlerTests
    {
        private readonly ClaimProcessingDbContext _context;
        private readonly IMapper _mapper;

        public GetShipmentClaimsQueryHandlerTests(QueryTestFixtures fixtures)
        {
            _context = fixtures.Context;
            _mapper = fixtures.Mapper;
        }

        [Fact]

        public async Task GetClaimsByShipmentId()
        {
            var handler = new GetShipmentClaimsQueryHandler(_context, _mapper);
            var result = await handler.Handle(new GetShipmentClaimsQuery { ShipmentId = 2 }, CancellationToken.None);

            result.ShouldBeOfType<ShipmentClaimsVm>();
            result.ShipmentClaims.Select(x => x.ClaimId).FirstOrDefault().ShouldBe(1);
        }
    }
}
