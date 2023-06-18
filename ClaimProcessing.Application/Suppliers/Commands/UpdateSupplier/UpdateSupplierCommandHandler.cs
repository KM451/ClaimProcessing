using ClaimProcessing.Application.Common.Interfaces;
using ClaimProcessing.Domain.Exceptions;
using ClaimProcessing.Domain.ValueObjects;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace ClaimProcessing.Application.Suppliers.Commands.UpdateSupplier
{
    public class UpdateSupplierCommandHandler : IRequestHandler<UpdateSupplierCommand>
    {
        private readonly IClaimProcessingDbContext _context;
        public UpdateSupplierCommandHandler(IClaimProcessingDbContext claimProcessingDbContext)
        {
            _context = claimProcessingDbContext;
        }
        public async Task Handle(UpdateSupplierCommand request, CancellationToken cancellationToken)
        {
            var supplier = await _context.Suppliers.Where(s => s.Id == request.SupplierId).FirstOrDefaultAsync(cancellationToken);
            if (supplier == null)
            {
                throw new NullException(request.SupplierId);
            }
            else
            {
                supplier.Name = request.Name;
                supplier.Address = new Address(request.Street, request.City, request.Country, request.ZipCode);
                supplier.ContactPerson = FullName.For(request.ContactPerson);

                _context.Suppliers.Update(supplier);
                await _context.SaveChangesAsync(cancellationToken);
            }
        }
    }
}
