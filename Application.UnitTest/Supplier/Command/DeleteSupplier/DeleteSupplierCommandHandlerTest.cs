using Application.UnitTest.Common;
using ClaimProcessing.Application.Suppliers.Commands.DeleteSupplier;
using Microsoft.EntityFrameworkCore;
using Shouldly;

namespace Application.UnitTest.Supplier.Command.DeleteSupplier
{
    public class DeleteSupplierCommandHandlerTest : CommandTestBase
    {
        private readonly DeleteSupplierCommandHandler _handler;

        public DeleteSupplierCommandHandlerTest()
            : base()
        {
            _handler = new DeleteSupplierCommandHandler(_context);
        }

        [Fact]
        public async Task Handle_GivenValidRequest_ShouldDeleteSupplier()
        {
            var command = new DeleteSupplierCommand()
            {
                SupplierId = 1
            };

            await _handler.Handle(command, CancellationToken.None);
            var dir = await _context.Suppliers.FirstAsync(x => x.Id == command.SupplierId && x.StatusId == 0, CancellationToken.None);
            dir.ShouldNotBeNull();
        }
    }
}
