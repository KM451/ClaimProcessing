using Application.UnitTest.Common;
using ClaimProcessing.Application.Shipments.Commands.DeleteShipment;
using ClaimProcessing.Shared.Shipments.Commands.DeleteShipment;
using Microsoft.EntityFrameworkCore;
using Shouldly;

namespace Application.UnitTest.Shipment.Command.DeleteShipment
{
    public class DeleteShipmentCommandHandlerTest : CommandTestBase
    {
        private readonly DeleteShipmentCommandHandler _handler;

        public DeleteShipmentCommandHandlerTest()
            : base()
        {
            _handler = new DeleteShipmentCommandHandler(_context);
        }

        [Fact]
        public async Task Handle_GivenValidRequest_ShouldDeleteShipment()
        {
            var command = new DeleteShipmentCommand()
            {
                ShipmentId = 1
            };

            await _handler.Handle(command, CancellationToken.None);
            var dir = await _context.Shipments.FirstAsync(x => x.Id == command.ShipmentId && x.StatusId == 0, CancellationToken.None);
            dir.ShouldNotBeNull();
        }
    }
}
