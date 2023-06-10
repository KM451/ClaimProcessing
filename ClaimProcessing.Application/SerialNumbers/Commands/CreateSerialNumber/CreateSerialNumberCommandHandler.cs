using ClaimProcessing.Application.Common.Interfaces;
using ClaimProcessing.Domain.Entities;
using MediatR;

namespace ClaimProcessing.Application.SerialNumbers.Commands.CreateSerialNumber
{
    public class CreateSerialNumberCommandHandler : IRequestHandler<CreateSerialNumberCommand, int>
    {
        private readonly IClaimProcessingDbContext _context;
        public CreateSerialNumberCommandHandler(IClaimProcessingDbContext claimProcessingDbContext)
        {
            _context = claimProcessingDbContext;
        }
        public async Task<int> Handle(CreateSerialNumberCommand request, CancellationToken cancellationToken)
        {
            SerialNumber serialNumber = new()
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
