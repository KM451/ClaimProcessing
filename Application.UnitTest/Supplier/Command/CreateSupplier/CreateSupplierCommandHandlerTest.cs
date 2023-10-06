using Application.UnitTest.Common;
using ClaimProcessing.Application.Suppliers.Commands.CreateSupplier;
using Microsoft.EntityFrameworkCore;
using Shouldly;

namespace Application.UnitTest.Supplier.Command.CreateSupplier
{
    public class CreateSupplierCommandHandlerTest : CommandTestBase
    {
        private readonly CreateSupplierCommandHandler _handler;

        public CreateSupplierCommandHandlerTest()
            : base()
        {
            _handler = new CreateSupplierCommandHandler(_context, Mapper);
        }

        [Fact]
        public async Task Handle_GivenValidRequest_ShouldInsertSupplier()
        {
            var command = new CreateSupplierCommand()
            {
                Name = "name",
                Street = "street",
                City = "city",
                Country = "country",
                ZipCode = "00-000",
                ContactPerson = "Contact Person"
            };

            var result = await _handler.Handle(command, CancellationToken.None);
            var dir = await _context.Suppliers.FirstAsync(x => x.Id == result, CancellationToken.None);
            dir.ShouldNotBeNull();

        }
    }
}
