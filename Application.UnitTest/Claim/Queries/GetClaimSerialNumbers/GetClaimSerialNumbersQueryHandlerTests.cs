using Application.UnitTest.Common;
using AutoMapper;
using ClaimProcessing.Application.Claims.Queries.GetClaimSerialNumbers;
using ClaimProcessing.Persistance;
using Shouldly;

namespace Application.UnitTest.Claim.Queries.GetClaimSerialNumbers
{
    [Collection("QueryCollection")]
    public class GetClaimSerialNumbersQueryHandlerTests
    {
        private readonly ClaimProcessingDbContext _context;
        private readonly IMapper _mapper;

        public GetClaimSerialNumbersQueryHandlerTests(QueryTestFixtures fixtures)
        {
            _context = fixtures.Context;
            _mapper = fixtures.Mapper;
        }

        [Fact]

        public async Task GetSerialNumbersByClaimId()
        {
            var handler = new GetClaimSerialNumbersQueryHandler(_context, _mapper);
            var result = await handler.Handle(new GetClaimSerialNumbersQuery { ClaimId = 2 }, CancellationToken.None);

            result.ShouldBeOfType<ClaimSerialNumbersVm>();
            result.SerialNumbers.FirstOrDefault().Value.ShouldBe("123456789");
        }
    }
}
