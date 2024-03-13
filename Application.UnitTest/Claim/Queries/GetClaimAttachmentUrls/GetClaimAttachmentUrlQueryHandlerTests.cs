using Application.UnitTest.Common;
using ClaimProcessing.Application.Claims.Queries.GetClaimAttachmentsUrls;
using ClaimProcessing.Persistance;
using ClaimProcessing.Shared.Claims.Queries.GetClaimAttachmentUrls;
using Shouldly;

namespace Application.UnitTest.Claim.Queries.GetClaimAttachmentUrls
{
    [Collection("QueryCollection")]
    public class GetClaimAttachmentUrlQueryHandlerTests
    {
        private readonly ClaimProcessingDbContext _context;

        public GetClaimAttachmentUrlQueryHandlerTests(QueryTestFixtures fixtures)
        {
            _context = fixtures.Context;
        }

        [Fact]

        public async Task GetAttachmentUrlsByClaimId()
        {
            var handler = new GetClaimAttachmentUrlsQueryHandler(_context);
            var result = await handler.Handle(new GetClaimAttachmentUrlsQuery { ClaimId = 1}, CancellationToken.None);
            var result2 = await handler.Handle(new GetClaimAttachmentUrlsQuery { ClaimId = 2}, CancellationToken.None);

            result.ShouldBeOfType<ClaimAttachmentUrlsVm>();
            result2.ShouldBeOfType<ClaimAttachmentUrlsVm>();
            result.AttachmentUrls.Count.ShouldBe(0);
            result2.AttachmentUrls.Count.ShouldBe(1);
            result2.AttachmentUrls.FirstOrDefault().Path.ShouldBe("C:\\Windows\\System32");

        }
    }
}
