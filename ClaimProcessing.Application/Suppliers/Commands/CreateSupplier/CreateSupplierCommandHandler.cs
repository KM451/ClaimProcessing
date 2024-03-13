using ClaimProcessing.Application.Common.Interfaces;
using ClaimProcessing.Domain.Entities;
using ClaimProcessing.Domain.ValueObjects;
using ClaimProcessing.Shared.Suppliers.Commands.CreateSupplier;
using MediatR;

namespace ClaimProcessing.Application.Suppliers.Commands.CreateSupplier
{
    public class CreateSupplierCommandHandler(IClaimProcessingDbContext _context) : IRequestHandler<CreateSupplierCommand, int>
    {
        public async Task<int> Handle(CreateSupplierCommand request, CancellationToken cancellationToken)
        {
            var supplier = Map(request);
            
            _context.Suppliers.Add(supplier);
            await _context.SaveChangesAsync(cancellationToken);
            return supplier.Id;
        }

        private static Supplier Map(CreateSupplierCommand command)
        {
            return new Supplier
            {
                Name = command.Name,
                ContactPerson = FullName.For(command.ContactPerson),
                Address = new Address(command.Street, command.City, command.Country, command.ZipCode)
            };
        }
    }
}
