using Application.UnitTest.Common;
using ClaimProcessing.Application.Shipments.Commands.UpdateShipment;
using Microsoft.EntityFrameworkCore;
using Shouldly;

namespace Application.UnitTest.Shipment.Command.UpdateShipment
{
    public class UpdateShipmentCommandHandlerTest : CommandTestBase
    {
        private readonly UpdateShipmentCommandHandler _handler;

        public UpdateShipmentCommandHandlerTest()
            : base()
        {
            _handler = new UpdateShipmentCommandHandler(_context, Mapper);
        }

        [Fact]
        public async Task Handle_GivenValidRequest_ShouldUpdateShipment()
        {
            var command = new UpdateShipmentCommand()
            {
                ShipmentDate = new DateTime(2001, 2, 3),
                Speditor = "test1",
                ShippingDocumentNo = "test1",
                TotalWeight = 11,
                SupplierId = 4
            };

            var result = await _handler.Handle(command, CancellationToken.None);
            var s = await _context.Shipments.FirstAsync(x => x.Id == result, CancellationToken.None);
            s.ShipmentDate.ShouldBe(command.ShipmentDate);
            s.Speditor.ShouldBe(command.Speditor);
            s.TotalWeight.ShouldBe(command.TotalWeight);
            s.SupplierId.ShouldBe(command.SupplierId);
            s.ShippingDocumentNo.ShouldBe(command.ShippingDocumentNo);
        }
    }
}
