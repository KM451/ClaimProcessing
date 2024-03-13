using Application.UnitTest.Common;
using ClaimProcessing.Application.Claims.Queries.GetClaimSerialNumbers;
using ClaimProcessing.Persistance;
using ClaimProcessing.Shared.Claims.Queries.GetClaimSerialNumbers;
using Shouldly;

namespace Application.UnitTest.Claim.Queries.GetClaimSerialNumbers
{
    [Collection("QueryCollection")]
    public class GetClaimSerialNumbersQueryHandlerTests
    {
        private readonly ClaimProcessingDbContext _context;

        public GetClaimSerialNumbersQueryHandlerTests(QueryTestFixtures fixtures)
        {
            _context = fixtures.Context;
        }

        [Fact]
        public async Task GetSerialNumbersByClaimId()
        {
            var handler = new GetClaimSerialNumbersQueryHandler(_context);
            var result = await handler.Handle(new GetClaimSerialNumbersQuery { ClaimId = 2 }, CancellationToken.None);

            result.ShouldBeOfType<ClaimSerialNumbersVm>();
            result.SerialNumbers.FirstOrDefault().Value.ShouldBe("123456789");
        }
    }
}
