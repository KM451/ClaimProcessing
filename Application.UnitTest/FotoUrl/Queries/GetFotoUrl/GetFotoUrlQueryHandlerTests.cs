using Application.UnitTest.Common;
using AutoMapper;
using ClaimProcessing.Application.FotoUrls.Queries.GetFotoUrl;
using ClaimProcessing.Persistance;
using ClaimProcessing.Shared.FotoUrls.Queries.GetFotoUrl;
using Shouldly;

namespace Application.UnitTest.FotoUrl.Queries.GetFotoUrl
{
    [Collection("QueryCollection")]
    public class GetFotoUrlQueryHandlerTests
    {
        private readonly ClaimProcessingDbContext _context;

        public GetFotoUrlQueryHandlerTests(QueryTestFixtures fixtures)
        {
            _context = fixtures.Context;
        }

        [Fact]

        public async Task GetFotoUrlById()
        {
            var handler = new GetFotoUrlQueryHandler(_context);
            var result = await handler.Handle(new GetFotoUrlQuery { FotoUrlId = 1 }, CancellationToken.None);

            result.ShouldBeOfType<FotoUrlVm>();
            result.Path.ShouldBe("C:\\Windows\\System");
        }
    }
}
