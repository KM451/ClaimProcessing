using Application.UnitTest.Common;
using AutoMapper;
using ClaimProcessing.Application.AttachmentUrls.Queries.GetAttachmentUrl;
using ClaimProcessing.Persistance;
using Shouldly;

namespace Application.UnitTest.AttachmentUrl.Queries.GetAttachmentUrl
{
    [Collection("QueryCollection")]
    public class GetAttachmentUrlQueryHandlerTests
    {
        private readonly ClaimProcessingDbContext _context;
        private readonly IMapper _mapper;

        public GetAttachmentUrlQueryHandlerTests(QueryTestFixtures fixtures)
        {
            _context = fixtures.Context;
            _mapper = fixtures.Mapper;
        }

        [Fact]

        public async Task GetAttachmentUrlById()
        {
            var handler = new GetAttachmentUrlQueryHandler(_context, _mapper);
            var result = await handler.Handle(new GetAttachmentUrlQuery { AttachmentUrlId = 1 }, CancellationToken.None);

            result.ShouldBeOfType<AttachmentUrlVm>();
            result.Path.ShouldBe("C:\\Windows\\System32");
        }

    }
}
