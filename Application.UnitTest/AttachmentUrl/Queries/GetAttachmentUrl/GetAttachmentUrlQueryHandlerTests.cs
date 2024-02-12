using Application.UnitTest.Common;
using ClaimProcessing.Application.AttachmentUrls.Queries.GetAttachmentUrl;
using ClaimProcessing.Persistance;
using ClaimProcessing.Shared.AttachmentUrls.Queries.GetAttachmentUrl;
using Shouldly;

namespace Application.UnitTest.AttachmentUrl.Queries.GetAttachmentUrl
{
    [Collection("QueryCollection")]
    public class GetAttachmentUrlQueryHandlerTests
    {
        private readonly ClaimProcessingDbContext _context;

        public GetAttachmentUrlQueryHandlerTests(QueryTestFixtures fixtures)
        {
            _context = fixtures.Context;
        }

        [Fact]

        public async Task GetAttachmentUrlById()
        {
            var handler = new GetAttachmentUrlQueryHandler(_context);
            var result = await handler.Handle(new GetAttachmentUrlQuery { AttachmentUrlId = 1 }, CancellationToken.None);

            result.ShouldBeOfType<AttachmentUrlVm>();
            result.Path.ShouldBe("C:\\Windows\\System32");
        }

    }
}
