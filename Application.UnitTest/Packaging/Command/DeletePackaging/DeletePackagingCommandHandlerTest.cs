using Application.UnitTest.Common;
using ClaimProcessing.Application.Packagings.Commands.DeletePackaging;
using Microsoft.EntityFrameworkCore;
using Shouldly;

namespace Application.UnitTest.Packaging.Command.DeletePackaging
{
    public class DeletePackagingCommandHandlerTest : CommandTestBase
    {
        private readonly DeletePackagingCommandHandler _handler;

        public DeletePackagingCommandHandlerTest()
            : base()
        {
            _handler = new DeletePackagingCommandHandler(_context);
        }

        [Fact]
        public async Task Handle_GivenValidRequest_ShouldDeletePackaging()
        {
            var command = new DeletePackagingCommand()
            {
                PackagingId = 1
            };

            await _handler.Handle(command, CancellationToken.None);
            var dir = await _context.Packagings.FirstAsync(x => x.Id == command.PackagingId && x.StatusId == 0, CancellationToken.None);
            dir.ShouldNotBeNull();
        }
    }
}