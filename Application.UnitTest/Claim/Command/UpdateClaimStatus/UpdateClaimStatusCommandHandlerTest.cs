using Application.UnitTest.Common;
using ClaimProcessing.Application.Claims.Commands.UpdateClaimStatus;
using ClaimProcessing.Shared.Claims.Commands.UpdateClaimStatus;
using Microsoft.EntityFrameworkCore;
using Shouldly;

namespace Application.UnitTest.Claim.Command.UpdateClaimStatus
{
    public class UpdateClaimStatusCommandHandlerTest : CommandTestBase
    {
        private readonly UpdateClaimStatusCommandHandler _handler;

        public UpdateClaimStatusCommandHandlerTest()
            :base()
        {
            _handler = new UpdateClaimStatusCommandHandler(_context, _bonfi);
        }

        [Fact]
        public async Task Handle_GivenValidRequest_ShouldUpdateClaimStatus()
        {
            var command = new UpdateClaimStatusCommand()
            {
                ClaimId = 1,
                ClaimStatus = 11
            };

            await _handler.Handle(command, CancellationToken.None);
            var cs = await _context.Claims.FirstAsync(x => x.Id == command.ClaimId, CancellationToken.None);

            cs.ClaimStatus.ShouldBe(11);
        }
    }
}
