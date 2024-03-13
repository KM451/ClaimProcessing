using Application.UnitTest.Common;
using ClaimProcessing.Application.SerialNumbers.Queries.GetSerialNumber;
using ClaimProcessing.Persistance;
using ClaimProcessing.Shared.SerialNumbers.Queries.GetSerialNumber;
using Shouldly;

namespace Application.UnitTest.SerialNumber.Queries.GetSerialNumber
{
    [Collection("QueryCollection")]
    public class GetSerialNumberQueryHandlerTests
    {
        private readonly ClaimProcessingDbContext _context;

        public GetSerialNumberQueryHandlerTests(QueryTestFixtures fixtures)
        {
            _context = fixtures.Context;
        }

        [Fact]

        public async Task GetSerialNumberById()
        {
            var handler = new GetSerialNumberQueryHandler(_context);
            var result = await handler.Handle(new GetSerialNumberQuery { SerialNumberId = 1 }, CancellationToken.None);

            result.ShouldBeOfType<SerialNumberVm>();
            result.Value.ShouldBe("123456789");
        }
    }
}
