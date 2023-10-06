using Application.UnitTest.Common;
using AutoMapper;
using ClaimProcessing.Application.Claims.Queries.GetAllClaimsShort;
using ClaimProcessing.Persistance;
using Shouldly;

namespace Application.UnitTest.Claim.Queries.GetAllClaimsShort
{
    [Collection("QueryCollection")]
    public class GetAllClaimsShortQueryHandlerTests
    {
        private readonly ClaimProcessingDbContext _context;
        private readonly IMapper _mapper;

        public GetAllClaimsShortQueryHandlerTests(QueryTestFixtures fixtures)
        {
            _context = fixtures.Context;
            _mapper = fixtures.Mapper;
        }

        [Fact]

        public async Task GetAllClaimsShort()
        {
            var handler = new GetAllClaimsShortQueryHandler(_context, _mapper);
            var result = await handler.Handle(new GetAllClaimsShortQuery(), CancellationToken.None);

            result.ShouldBeOfType<AllClaimsShortVm>();
            result.Claims.Count.ShouldBe(2);
        }

        [Fact]

        public async Task GetAllClaimsShortWithFilter()
        {
            var handler = new GetAllClaimsShortQueryHandler(_context, _mapper);
            var filter = "ItemCode eq 12A34C";
            var result = await handler.Handle(new GetAllClaimsShortQuery { Filter = filter}, CancellationToken.None);

            result.Claims.Count.ShouldBe(1);
            result.Claims.FirstOrDefault().ItemCode.ShouldBe("12A34C");
        }
    }
}
