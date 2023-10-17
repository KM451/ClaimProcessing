using Application.UnitTest.Common;
using AutoMapper;
using ClaimProcessing.Application.Packagings.Queries.GetPackaging;
using ClaimProcessing.Persistance;
using Shouldly;

namespace Application.UnitTest.Packaging.Queries.GetPackaging
{
    [Collection("QueryCollection")]
    public class GetPackagingQueryHandlerTests
    {
        private readonly ClaimProcessingDbContext _context;
        private readonly IMapper _mapper;

        public GetPackagingQueryHandlerTests(QueryTestFixtures fixtures)
        {
            _context = fixtures.Context;
            _mapper = fixtures.Mapper;
        }

        [Fact]

        public async Task GetPackagingById()
        {
            var handler = new GetPackagingQueryHandler(_context, _mapper);
            var result = await handler.Handle(new GetPackagingQuery { PackagingId = 1 }, CancellationToken.None);

            result.ShouldBeOfType<PackagingVm>();
            result.Type.ShouldBe("box");
        }
    }
}
