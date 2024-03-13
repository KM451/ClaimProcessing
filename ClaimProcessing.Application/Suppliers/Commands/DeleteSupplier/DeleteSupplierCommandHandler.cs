using ClaimProcessing.Application.Common.Exceptions;
using ClaimProcessing.Application.Common.Interfaces;
using ClaimProcessing.Domain.ValueObjects;
using ClaimProcessing.Shared.Suppliers.Commands.DeleteSupplier;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace ClaimProcessing.Application.Suppliers.Commands.DeleteSupplier
{
    public class DeleteSupplierCommandHandler : IRequestHandler<DeleteSupplierCommand>
    {
        private readonly IClaimProcessingDbContext _context;
        public DeleteSupplierCommandHandler(IClaimProcessingDbContext claimProcessingDbContext)
        {
            _context = claimProcessingDbContext;
        }

        public async Task Handle(DeleteSupplierCommand request, CancellationToken cancellationToken)
        {
            var supplier = await _context.Suppliers.Where(s => s.Id == request.SupplierId).FirstOrDefaultAsync(cancellationToken);
            if (supplier == null)
            {
                throw new NullException(request.SupplierId);
            }

            var address = new Address(supplier.Address.Street, supplier.Address.City, supplier.Address.Country, supplier.Address.ZipCode);
            var contactPerson = new FullName(supplier.ContactPerson.FirstName, supplier.ContactPerson.LastName);

            _context.Suppliers.Remove(supplier);
            supplier.Address = address;
            supplier.ContactPerson = contactPerson;

            await _context.SaveChangesAsync(cancellationToken);

        }
    }
}
