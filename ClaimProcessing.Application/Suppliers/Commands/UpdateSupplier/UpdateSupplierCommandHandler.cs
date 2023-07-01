using AutoMapper;
using ClaimProcessing.Application.Common.Exceptions;
using ClaimProcessing.Application.Common.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace ClaimProcessing.Application.Suppliers.Commands.UpdateSupplier
{
    public class UpdateSupplierCommandHandler : IRequestHandler<UpdateSupplierCommand>
    {
        private readonly IClaimProcessingDbContext _context;
        private IMapper _mapper;
        public UpdateSupplierCommandHandler(IClaimProcessingDbContext claimProcessingDbContext, IMapper mapper)
        {
            _context = claimProcessingDbContext;
            _mapper = mapper;
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
                _mapper.Map(request, supplier);

                await _context.SaveChangesAsync(cancellationToken);
            }
        }
    }
}
