using Application.UnitTest.Common;
using AutoMapper;
using ClaimProcessing.Application.Claims.Queries.GetClaimFotosUrls;
using ClaimProcessing.Persistance;
using Shouldly;

namespace Application.UnitTest.Claim.Queries.GetClaimFotosUrls
{
    [Collection("QueryCollection")]
    public class GetClaimFotosUrlsQueryHandlerTests
    {
        private readonly ClaimProcessingDbContext _context;
        private readonly IMapper _mapper;

        public GetClaimFotosUrlsQueryHandlerTests(QueryTestFixtures fixtures)
        {
            _context = fixtures.Context;
            _mapper = fixtures.Mapper;
        }

        [Fact]

        public async Task GetFotosUrlsByClaimId()
        {
            var handler = new GetClaimFotoUrlsQueryHandler(_context, _mapper);
            var result = await handler.Handle(new GetClaimFotoUrlsQuery { ClaimId = 2 }, CancellationToken.None);

            result.ShouldBeOfType<ClaimFotoUrlsVm>();
            result.FotoUrls.FirstOrDefault().Path.ShouldBe("C:\\Windows\\System");
        }
    }
}
