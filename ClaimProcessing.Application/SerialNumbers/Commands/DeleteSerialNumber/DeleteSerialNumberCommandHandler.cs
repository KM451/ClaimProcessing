using ClaimProcessing.Application.Common.Exceptions;
using ClaimProcessing.Application.Common.Interfaces;
using ClaimProcessing.Shared.SerialNumbers.Queries.GetSerialNumber;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace ClaimProcessing.Application.SerialNumbers.Commands.DeleteSerialNumber
{
    public class DeleteSerialNumberCommandHandler(IClaimProcessingDbContext _context) : IRequestHandler<DeleteSerialNumberCommand>
    {
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
