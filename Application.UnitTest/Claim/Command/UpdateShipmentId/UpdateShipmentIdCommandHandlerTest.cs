using Application.UnitTest.Common;
using ClaimProcessing.Application.Claims.Commands.UpdateShipmentId;
using Microsoft.EntityFrameworkCore;
using Shouldly;

namespace Application.UnitTest.Claim.Command.UpdateShipmentId
{
    public class UpdateShipmentIdCommandHandlerTest : CommandTestBase
    {
        private readonly UpdateShipmentIdCommandHandler _handler;

        public UpdateShipmentIdCommandHandlerTest()
            : base()
        {
            _handler = new UpdateShipmentIdCommandHandler(_context, _mapper);
        }

        [Fact]
        public async Task Handle_GivenValidRequest_ShouldUpdateShipmentId()
        {
            var command = new UpdateShipmentIdCommand()
            {
                ClaimId = 1,
                ShipmentId = 11
            };

            await _handler.Handle(command, CancellationToken.None);
            var si = await _context.Claims.FirstAsync(x => x.Id == command.ClaimId, CancellationToken.None);

            si.ShipmentId.ShouldBe(11);
        }
    }
}
