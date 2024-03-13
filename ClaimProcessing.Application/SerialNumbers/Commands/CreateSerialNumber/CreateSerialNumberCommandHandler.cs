using ClaimProcessing.Application.Common.Interfaces;
using ClaimProcessing.Domain.Entities;
using ClaimProcessing.Shared.SerialNumbers.Commands.CreateSerialNumber;
using MediatR;

namespace ClaimProcessing.Application.SerialNumbers.Commands.CreateSerialNumber
{
    public class CreateSerialNumberCommandHandler(IClaimProcessingDbContext _context) : IRequestHandler<CreateSerialNumberCommand, int>
    {
        public async Task<int> Handle(CreateSerialNumberCommand request, CancellationToken cancellationToken)
        {
            var serialNumber = new SerialNumber
            {
                Value = request.Value,
                ClaimId = request.ClaimId
            };

            _context.SerialNumbers.Add(serialNumber);
            await _context.SaveChangesAsync(cancellationToken);
            return serialNumber.Id;
        }

        
    }
}
