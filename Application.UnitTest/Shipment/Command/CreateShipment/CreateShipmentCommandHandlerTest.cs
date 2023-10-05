using Application.UnitTest.Common;
using ClaimProcessing.Application.Shipments.Commands.CreateShipment;
using Microsoft.EntityFrameworkCore;
using Shouldly;

namespace Application.UnitTest.Shipment.Command.CreateShipment
{
    public class CreateShipmentCommandHandlerTest : CommandTestBase
    {
        private readonly CreateShipmentCommandHandler _handler;

        public CreateShipmentCommandHandlerTest()
            : base()
        {
            _handler = new CreateShipmentCommandHandler(_context, _mapper);
        }

        [Fact]
        public async Task Handle_GivenValidRequest_ShouldInsertShipment()
        {
            var command = new CreateShipmentCommand()
            {
                ShipmentDate = new DateTime(2000, 1, 1),
                Speditor = "test",
                ShippingDocumentNo = "test",
                TotalWeight = 10,
                SupplierId = 3
            };

            var result = await _handler.Handle(command, CancellationToken.None);
            var dir = await _context.Shipments.FirstAsync(x => x.Id == result, CancellationToken.None);
            dir.ShouldNotBeNull();
            
        }
    }
}
