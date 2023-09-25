using Application.UnitTest.Common;
using AutoMapper;
using ClaimProcessing.Application.Claims.Queries.GetClaimDetail;
using ClaimProcessing.Persistance;
using Shouldly;

namespace Application.UnitTest.Claim.Queries.GetClaimDetail
{
    [Collection("QueryCollection")]
    public class GetClaimDetailQueryHandlerTests
    {
        private readonly ClaimProcessingDbContext _context;
        private readonly IMapper _mapper;

        public GetClaimDetailQueryHandlerTests(QueryTestFixtures fixtures)
        {
            _context = fixtures.Context;
            _mapper = fixtures.Mapper;
        }

        [Fact]

        public async Task GetClaimDetailsById()
        {
            var handler = new GetClaimDetailQueryHandler(_context, _mapper);
            var result = await handler.Handle(new GetClaimDetailQuery { ClaimId=1}, CancellationToken.None);

            result.ShouldBeOfType<ClaimDetailVm>();
            result.ItemCode.ShouldBe("12A34B");
        }
    }
}
