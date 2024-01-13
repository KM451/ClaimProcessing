using Application.UnitTest.Common;
using ClaimProcessing.Application.Claims.Queries.GetClaimDetail;
using ClaimProcessing.Persistance;
using ClaimProcessing.Shared.Claims.Queries.GetClaimDetail;
using Shouldly;

namespace Application.UnitTest.Claim.Queries.GetClaimDetail
{
    [Collection("QueryCollection")]
    public class GetClaimDetailQueryHandlerTests
    {
        private readonly ClaimProcessingDbContext _context;

        public GetClaimDetailQueryHandlerTests(QueryTestFixtures fixtures)
        {
            _context = fixtures.Context;
        }

        [Fact]

        public async Task GetClaimDetailsById()
        {
            var handler = new GetClaimDetailQueryHandler(_context);
            var result = await handler.Handle(new GetClaimDetailQuery { ClaimId=1}, CancellationToken.None);

            result.ShouldBeOfType<ClaimDetailVm>();
            result.ItemCode.ShouldBe("12A34B");
        }
    }
}
