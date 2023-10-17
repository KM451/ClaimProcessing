using Application.UnitTest.Common;
using AutoMapper;
using ClaimProcessing.Application.SerialNumbers.Queries.GetSerialNumber;
using ClaimProcessing.Persistance;
using Shouldly;

namespace Application.UnitTest.SerialNumber.Queries.GetSerialNumber
{
    [Collection("QueryCollection")]
    public class GetSerialNumberQueryHandlerTests
    {
        private readonly ClaimProcessingDbContext _context;
        private readonly IMapper _mapper;

        public GetSerialNumberQueryHandlerTests(QueryTestFixtures fixtures)
        {
            _context = fixtures.Context;
            _mapper = fixtures.Mapper;
        }

        [Fact]

        public async Task GetSerialNumberById()
        {
            var handler = new GetSerialNumberQueryHandler(_context, _mapper);
            var result = await handler.Handle(new GetSerialNumberQuery { SerialNumberId = 1 }, CancellationToken.None);

            result.ShouldBeOfType<SerialNumberVm>();
            result.Value.ShouldBe("123456789");
        }
    }
}
