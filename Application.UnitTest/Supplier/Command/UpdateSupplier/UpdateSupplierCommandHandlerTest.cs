using Application.UnitTest.Common;
using ClaimProcessing.Application.Suppliers.Commands.UpdateSupplier;
using ClaimProcessing.Domain.ValueObjects;
using Microsoft.EntityFrameworkCore;
using Shouldly;

namespace Application.UnitTest.Supplier.Command.UpdateSupplier
{
    public class UpdateSupplierCommandHandlerTest : CommandTestBase
    {
        private readonly UpdateSupplierCommandHandler _handler;

        public UpdateSupplierCommandHandlerTest()
            : base()
        {
            _handler = new UpdateSupplierCommandHandler(_context, _mapper);
        }

        [Fact]
        public async Task Handle_GivenValidRequest_ShouldUpdateSupplier()
        {
            var command = new UpdateSupplierCommand()
            {
                Name = "name1",
                Street = "street1",
                City = "city1",
                Country = "country1",
                ZipCode = "01-001",
                ContactPerson = "Person Contact"
            };

            var result = await _handler.Handle(command, CancellationToken.None);
            var s = await _context.Suppliers.FirstAsync(x => x.Id == result, CancellationToken.None);
            s.Name.ShouldBe(command.Name);
            s.Address.ShouldBe(new Address(command.Street, command.City, command.Country, command.ZipCode));
            s.ContactPerson.ShouldBe(FullName.For(command.ContactPerson));
        }
    }
}
