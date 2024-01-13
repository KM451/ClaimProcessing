using Application.UnitTest.Common;
using ClaimProcessing.Application.Claims.Queries.GetAllClaimsShort;
using ClaimProcessing.Persistance;
using ClaimProcessing.Shared.Claims.Queries.GetAllClaimsShort;
using Shouldly;

namespace Application.UnitTest.Claim.Queries.GetAllClaimsShort
{
    [Collection("QueryCollection")]
    public class GetAllClaimsShortQueryHandlerTests
    {
        private readonly ClaimProcessingDbContext _context;

        public GetAllClaimsShortQueryHandlerTests(QueryTestFixtures fixtures)
        {
            _context = fixtures.Context;
        }

        [Fact]

        public async Task GetAllClaimsShort()
        {
            var handler = new GetAllClaimsShortQueryHandler(_context);
            var result = await handler.Handle(new GetAllClaimsShortQuery(), CancellationToken.None);

            result.ShouldBeOfType<AllClaimsShortVm>();
            result.Claims.Count.ShouldBe(2);
        }

        [Fact]

        public async Task GetAllClaimsShortWithFilter()
        {
            var handler = new GetAllClaimsShortQueryHandler(_context);
            var filter = "ItemCode eq 12A34C";
            var result = await handler.Handle(new GetAllClaimsShortQuery { Filter = filter}, CancellationToken.None);

            result.Claims.Count.ShouldBe(1);
            result.Claims.FirstOrDefault().ItemCode.ShouldBe("12A34C");
        }
    }
}
