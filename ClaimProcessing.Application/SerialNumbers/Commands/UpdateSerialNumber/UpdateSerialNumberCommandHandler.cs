using ClaimProcessing.Application.Common.Interfaces;
using ClaimProcessing.Domain.Exceptions;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace ClaimProcessing.Application.SerialNumbers.Commands.UpdateSerialNumber
{
    public class UpdateSerialNumberCommandHandler : IRequestHandler<UpdateSerialNumberCommand>
    {
        private readonly IClaimProcessingDbContext _context;
        public UpdateSerialNumberCommandHandler(IClaimProcessingDbContext claimProcessingDbContext)
        {
            _context = claimProcessingDbContext;
        }
        public async Task Handle(UpdateSerialNumberCommand request, CancellationToken cancellationToken)
        {
            var serialNumber = await _context.SerialNumbers.Where(s => s.Id == request.SerialNumberId).FirstOrDefaultAsync(cancellationToken);

            if (serialNumber == null)
            {
                throw new NullException(request.SerialNumberId);
            }
            else
            {
                serialNumber.ClaimId = request.ClaimId;
                serialNumber.Value = request.Value;

                _context.SerialNumbers.Update(serialNumber);
                await _context.SaveChangesAsync(cancellationToken);
            }

            
        }
    }
}
