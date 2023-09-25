using Application.UnitTest.Common;
using ClaimProcessing.Application.Claims.Commands.DeleteClaim;
using Microsoft.EntityFrameworkCore;
using Shouldly;

namespace Application.UnitTest.Claim.Command.DeleteClaim
{
    public class DeleteClaimCommandHandlerTest : CommandTestBase
    {
        private readonly DeleteClaimCommandHandler _handler;

        public DeleteClaimCommandHandlerTest()
            : base()
        {
            _handler = new DeleteClaimCommandHandler(_context);
        }

        [Fact]
        public async Task Handle_GivenValidRequest_ShouldDeleteClaim()
        {
            var command = new DeleteClaimCommand()
            {
                ClaimId = 1
            };

            await _handler.Handle(command, CancellationToken.None);
            var dir = await _context.Claims.FirstAsync(x => x.Id == command.ClaimId && x.StatusId == 0, CancellationToken.None);
            dir.ShouldNotBeNull();
        }
    }
}
