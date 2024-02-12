using Application.UnitTest.Common;
using ClaimProcessing.Application.Claims.Queries.GetClaimFotosUrls;
using ClaimProcessing.Persistance;
using ClaimProcessing.Shared.Claims.Queries.GetClaimFotosUrls;
using Shouldly;

namespace Application.UnitTest.Claim.Queries.GetClaimFotosUrls
{
    [Collection("QueryCollection")]
    public class GetClaimFotosUrlsQueryHandlerTests
    {
        private readonly ClaimProcessingDbContext _context;

        public GetClaimFotosUrlsQueryHandlerTests(QueryTestFixtures fixtures)
        {
            _context = fixtures.Context;
        }

        [Fact]

        public async Task GetFotosUrlsByClaimId()
        {
            var handler = new GetClaimFotoUrlsQueryHandler(_context);
            var result = await handler.Handle(new GetClaimFotoUrlsQuery { ClaimId = 2 }, CancellationToken.None);

            result.ShouldBeOfType<ClaimFotoUrlsVm>();
            result.FotoUrls.FirstOrDefault().Path.ShouldBe("C:\\Windows\\System");
        }
    }
}
