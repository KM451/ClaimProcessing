using ClaimProcessing.Application.Common.Interfaces;
using ClaimProcessing.Shared.Claims.Queries.GetClaimSerialNumbers;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace ClaimProcessing.Application.Claims.Queries.GetClaimSerialNumbers
{
    public class GetClaimSerialNumbersQueryHandler(IClaimProcessingDbContext _context) : IRequestHandler<GetClaimSerialNumbersQuery, ClaimSerialNumbersVm>
    {
        public async Task<ClaimSerialNumbersVm> Handle(GetClaimSerialNumbersQuery request, CancellationToken cancellationToken)
        {
            var serialNumbers = await _context.SerialNumbers
                .Where(a => a.StatusId != 0 && a.ClaimId == request.ClaimId)
                .ToListAsync(cancellationToken);

            var serialNumbersVm = new ClaimSerialNumbersVm
            {
                SerialNumbers = serialNumbers.Select(src => new ClaimSerialNumbersDto { Value = src.Value }).ToList()
            };

            return serialNumbersVm;
        }
    }
}
