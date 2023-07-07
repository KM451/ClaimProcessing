using ClaimProcessing.Application.Common.Exceptions;
using ClaimProcessing.Application.Common.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace ClaimProcessing.Application.SerialNumbers.Commands.DeleteSerialNumber
{
    public class DeleteSerialNumberCommandHandler : IRequestHandler<DeleteSerialNumberCommand>
    {
        private readonly IClaimProcessingDbContext _context;
        public DeleteSerialNumberCommandHandler(IClaimProcessingDbContext claimProcessingDbContext)
        {
            _context = claimProcessingDbContext;
        }

        public async Task Handle(DeleteSerialNumberCommand request, CancellationToken cancellationToken)
        {
            var serialNumber = await _context.SerialNumbers.Where(s => s.Id == request.SerialNumberId).FirstOrDefaultAsync(cancellationToken);

            if (serialNumber == null)
            {
                throw new NullException(request.SerialNumberId);
            }

            _context.SerialNumbers.Remove(serialNumber);
            await _context.SaveChangesAsync(cancellationToken);

        }
    }
}
