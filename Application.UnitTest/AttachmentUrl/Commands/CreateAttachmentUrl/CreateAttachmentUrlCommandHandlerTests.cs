using Application.UnitTest.Common;
using ClaimProcessing.Application.AttachmentUrls.Commands.CreateAttachmentUrl;
using Microsoft.EntityFrameworkCore;
using Shouldly;

namespace Application.UnitTest.AttachmentUrl.Commands.CreateAttachmentUrl
{
    public class CreateAttachmentUrlCommandHandlerTests : CommandTestBase
    {
        private readonly CreateAttachmentUrlCommandHandler _handler;
        
        public CreateAttachmentUrlCommandHandlerTests()
            : base()
        {
            _handler = new CreateAttachmentUrlCommandHandler(_context, Mapper);       
        }

        [Fact]
        public async Task Handle_GivenValidRequest_ShouldInsertAttachmentUrl()
        {
            var command = new CreateAttachmentUrlCommand()
            {
                Path = "c:\\matrix",
                ClaimId = 1
            };

            var result = await _handler.Handle(command, CancellationToken.None);
            var dir = await _context.AttachmentUrls.FirstAsync(x => x.Id == result, CancellationToken.None);
            dir.ShouldNotBeNull();
        }
    }
}
