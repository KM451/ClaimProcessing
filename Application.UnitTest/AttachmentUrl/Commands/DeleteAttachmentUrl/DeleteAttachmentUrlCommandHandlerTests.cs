using Application.UnitTest.Common;
using ClaimProcessing.Application.AttachmentUrls.Commands.DeleteAttachmentUrl;
using ClaimProcessing.Shared.AttachmentUrls.Commands.DeleteAttachmentUrl;
using Microsoft.EntityFrameworkCore;
using Shouldly;

namespace Application.UnitTest.AttachmentUrl.Commands.DeleteAttachmentUrl
{
    public class DeleteAttachmentUrlCommandHandlerTests : CommandTestBase
    {
        private readonly DeleteAttachmentUrlCommandHandler _handler;

        public DeleteAttachmentUrlCommandHandlerTests()
            :base()
        {
            _handler = new DeleteAttachmentUrlCommandHandler(_context);
        }

        [Fact]
        public async Task Handle_GivenValidRequest_ShouldDeleteAttachmentUrl()
        {
            var command = new DeleteAttachmentUrlCommand()
            {
                 AttachmentUrlId = 1
            };

            await _handler.Handle(command, CancellationToken.None);
            var dir = await _context.AttachmentUrls.FirstAsync(x => x.Id == 1 && x.StatusId == 0, CancellationToken.None);
            dir.ShouldNotBeNull();
        }

    }

    
}
