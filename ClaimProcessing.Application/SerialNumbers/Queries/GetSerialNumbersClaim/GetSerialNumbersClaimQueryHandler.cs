using AutoMapper;
using ClaimProcessing.Application.Common.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace ClaimProcessing.Application.SerialNumbers.Queries.GetSerialNumbersClaim
{
    public class GetSerialNumbersClaimQueryHandler : IRequestHandler<GetSerialNumbersClaimQuery, SerialNumbersClaimVm>
    {
        private readonly IClaimProcessingDbContext _context;
        private IMapper _mapper;
        public GetSerialNumbersClaimQueryHandler(IClaimProcessingDbContext claimProcessingDbContext, IMapper mapper)
        {
            _context = claimProcessingDbContext;
            _mapper = mapper;
        }
        public async Task<SerialNumbersClaimVm> Handle(GetSerialNumbersClaimQuery request, CancellationToken cancellationToken)
        {
            var seriaNumbers = await _context.SerialNumbers
                .Where(a => a.StatusId != 0 && a.ClaimId == request.ClaimId)
                .ToListAsync(cancellationToken);

            var serialNumbersVm = _mapper.Map<SerialNumbersClaimVm>(seriaNumbers);

            return serialNumbersVm;
        }
    }
}
