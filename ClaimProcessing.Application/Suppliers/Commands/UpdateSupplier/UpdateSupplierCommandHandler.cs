using ClaimProcessing.Application.Common.Interfaces;
using ClaimProcessing.Domain.Entities;
using ClaimProcessing.Domain.ValueObjects;
using ClaimProcessing.Shared.Suppliers.Commands.UpdateSupplier;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace ClaimProcessing.Application.Suppliers.Commands.UpdateSupplier
{
    public class UpdateSupplierCommandHandler(IClaimProcessingDbContext _context) : IRequestHandler<UpdateSupplierCommand, int>
    {
        public async Task<int> Handle(UpdateSupplierCommand request, CancellationToken cancellationToken)
        {
            var supplier = await _context.Suppliers.Where(s => s.Id == request.SupplierId).FirstOrDefaultAsync(cancellationToken);
            if (supplier == null)
            {
                supplier = Map(request);
                _context.Suppliers.Add(supplier);
            }
            else
            {
                supplier.Name = request.Name;
                supplier.ContactPerson = FullName.For(request.ContactPerson);
                supplier.Address = new Address(request.Street, request.City, request.Country, request.ZipCode);
            }
            await _context.SaveChangesAsync(cancellationToken);
            return supplier.Id;
        }

        private static Supplier Map(UpdateSupplierCommand command)
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
