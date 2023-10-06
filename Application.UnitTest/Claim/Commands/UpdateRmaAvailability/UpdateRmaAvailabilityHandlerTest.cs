using Application.UnitTest.Common;
using ClaimProcessing.Application.Claims.Commands.UpdateRmaAvailability;
using Microsoft.EntityFrameworkCore;
using Shouldly;

namespace Application.UnitTest.Claim.Command.UpdateRmaAvailability
{
    public class UpdateRmaAvailabilityHandlerTest : CommandTestBase
    {
        private readonly UpdateRmaAvailabilityCommandHandler _handler;

        public UpdateRmaAvailabilityHandlerTest()
            :base()
        {
            _handler = new UpdateRmaAvailabilityCommandHandler(_context, Mapper);
        }


        [Fact]
        public async Task Handle_GivenValidRequest_ShouldUpdateClaimRmaAvailability()
        {
            var command = new UpdateRmaAvailabilityCommand()
            {
                ClaimId = 1,
                RmaAvailable = true
            };

            await _handler.Handle(command, CancellationToken.None);
            var ra = await _context.Claims.FirstAsync(x => x.Id == command.ClaimId, CancellationToken.None);

            ra.RmaAvailable.ShouldBe(true);
        }
    }
}

