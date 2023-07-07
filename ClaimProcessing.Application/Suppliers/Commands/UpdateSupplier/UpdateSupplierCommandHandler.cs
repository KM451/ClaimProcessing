using AutoMapper;
using ClaimProcessing.Application.Common.Interfaces;
using ClaimProcessing.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace ClaimProcessing.Application.Suppliers.Commands.UpdateSupplier
{
    public class UpdateSupplierCommandHandler : IRequestHandler<UpdateSupplierCommand, int>
    {
        private readonly IClaimProcessingDbContext _context;
        private IMapper _mapper;
        public UpdateSupplierCommandHandler(IClaimProcessingDbContext claimProcessingDbContext, IMapper mapper)
        {
            _context = claimProcessingDbContext;
            _mapper = mapper;
        }
        public async Task<int> Handle(UpdateSupplierCommand request, CancellationToken cancellationToken)
        {
            var supplier = await _context.Suppliers.Where(s => s.Id == request.SupplierId).FirstOrDefaultAsync(cancellationToken);
            if (supplier == null)
            {
                supplier = _mapper.Map<Supplier>(request);
                supplier.Id = 0;
                _context.Suppliers.Add(supplier);
            }
            else
            {
                _mapper.Map(request, supplier);
            }

            await _context.SaveChangesAsync(cancellationToken);
            return supplier.Id;
        }
    }
}
