using Application.UnitTest.Common;
using ClaimProcessing.Application.Packagings.Queries.GetPackaging;
using ClaimProcessing.Persistance;
using ClaimProcessing.Shared.Packagings.Queries.GetPackaging;
using Shouldly;

namespace Application.UnitTest.Packaging.Queries.GetPackaging
{
    [Collection("QueryCollection")]
    public class GetPackagingQueryHandlerTests
    {
        private readonly ClaimProcessingDbContext _context;

        public GetPackagingQueryHandlerTests(QueryTestFixtures fixtures)
        {
            _context = fixtures.Context;
        }

        [Fact]

        public async Task GetPackagingById()
        {
            var handler = new GetPackagingQueryHandler(_context);
            var result = await handler.Handle(new GetPackagingQuery { PackagingId = 1 }, CancellationToken.None);

            result.ShouldBeOfType<PackagingVm>();
            result.Type.ShouldBe("box");
        }
    }
}
