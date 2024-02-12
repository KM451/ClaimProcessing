using ClaimProcessing.Application.Common.Interfaces;
using ClaimProcessing.Shared.SerialNumbers.Queries.GetSerialNumber;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace ClaimProcessing.Application.SerialNumbers.Queries.GetSerialNumber
{
    public class GetSerialNumberQueryHandler(IClaimProcessingDbContext _context) : IRequestHandler<GetSerialNumberQuery, SerialNumberVm>
    {
        public async Task<SerialNumberVm> Handle(GetSerialNumberQuery request, CancellationToken cancellationToken)
        {
            var serialNumber = await _context.SerialNumbers
                .Where(s => s.StatusId != 0 && s.Id == request.SerialNumberId)
                .FirstOrDefaultAsync(cancellationToken);

            if (serialNumber == null) { return null; }

            var serialNumberVm = new SerialNumberVm { Value = serialNumber.Value };
            return serialNumberVm;
        }
    }
}
