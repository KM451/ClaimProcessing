using ClaimProcessing.Application.Common.Interfaces;
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
            _context.Suppliers.Remove(supplier);
            await _context.SaveChangesAsync(cancellationToken);
        }
    }
}
