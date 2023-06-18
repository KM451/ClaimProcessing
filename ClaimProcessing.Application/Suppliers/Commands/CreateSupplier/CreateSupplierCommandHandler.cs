using ClaimProcessing.Application.Common.Interfaces;
using ClaimProcessing.Domain.Entities;
using ClaimProcessing.Domain.ValueObjects;
using MediatR;

namespace ClaimProcessing.Application.Suppliers.Commands.CreateSupplier
{
    public class CreateSupplierCommandHandler : IRequestHandler<CreateSupplierCommand, int>
    {
        private readonly IClaimProcessingDbContext _context;
        public CreateSupplierCommandHandler(IClaimProcessingDbContext claimProcessingDbContext)
        {
            _context = claimProcessingDbContext;
        }
        public async Task<int> Handle(CreateSupplierCommand request, CancellationToken cancellationToken)
        {
            Supplier supplier = new()
            {
                Name = request.Name,
                Address = new Address(request.Street, request.City, request.Country, request.ZipCode),
                ContactPerson = FullName.For(request.ContactPerson)
            };

            _context.Suppliers.Add(supplier);
            await _context.SaveChangesAsync(cancellationToken);
            return supplier.Id;
        }
    }
}
