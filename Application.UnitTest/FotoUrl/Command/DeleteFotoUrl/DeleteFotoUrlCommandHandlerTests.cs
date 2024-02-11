using Application.UnitTest.Common;
using ClaimProcessing.Application.FotoUrls.Commands.DeleteFotoUrl;
using ClaimProcessing.Shared.FotoUrls.Commands.DeleteFotoUrl;
using Microsoft.EntityFrameworkCore;
using Shouldly;

namespace Application.UnitTest.FotoUrl.Command.DeleteFotoUrl
{
    public class DeleteFotoUrlCommandHandlerTests : CommandTestBase
    {
        private readonly DeleteFotoUrlCommandHandler _handler;

        public DeleteFotoUrlCommandHandlerTests()
            : base()
        {
            _handler = new DeleteFotoUrlCommandHandler(_context);
        }

        [Fact]
        public async Task Handle_GivenValidRequest_ShouldDeleteFotoUrl()
        {
            var command = new DeleteFotoUrlCommand()
            {
                FotoUrlId = 1
            };

            await _handler.Handle(command, CancellationToken.None);
            var dir = await _context.FotoUrls.FirstAsync(x => x.Id == 1 && x.StatusId == 0, CancellationToken.None);
            dir.ShouldNotBeNull();
        }

    }


}
