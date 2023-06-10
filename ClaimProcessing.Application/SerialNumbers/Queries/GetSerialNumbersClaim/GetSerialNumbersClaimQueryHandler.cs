using ClaimProcessing.Application.Common.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace ClaimProcessing.Application.SerialNumbers.Queries.GetSerialNumbersClaim
{
    public class GetSerialNumbersClaimQueryHandler : IRequestHandler<GetSerialNumbersClaimQuery, SerialNumbersClaimVm>
    {
        private readonly IClaimProcessingDbContext _context;
        public GetSerialNumbersClaimQueryHandler(IClaimProcessingDbContext claimProcessingDbContext)
        {
            _context = claimProcessingDbContext;
        }
        public async Task<SerialNumbersClaimVm> Handle(GetSerialNumbersClaimQuery request, CancellationToken cancellationToken)
        {
            var seriaNumbers = await _context.SerialNumbers
                .Where(a => a.StatusId != 0 && a.ClaimId == request.ClaimId)
                .ToListAsync(cancellationToken);

            var serialNumbersVm = new SerialNumbersClaimVm
            {
                SerialNumbers = seriaNumbers.Select(s => new SerialNumbersClaimDto
                {
                    SerialNumberId = s.Id,
                    Value = s.Value
                }).ToList()
            };

            return serialNumbersVm;
        }
    }
}
