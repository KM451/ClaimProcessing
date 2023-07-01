using AutoMapper;
using ClaimProcessing.Application.Common.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace ClaimProcessing.Application.Claims.Queries.GetClaimSerialNumbers
{
    public class GetClaimSerialNumbersQueryHandler : IRequestHandler<GetClaimSerialNumbersQuery, ClaimSerialNumbersVm>
    {
        private readonly IClaimProcessingDbContext _context;
        private IMapper _mapper;
        public GetClaimSerialNumbersQueryHandler(IClaimProcessingDbContext claimProcessingDbContext, IMapper mapper)
        {
            _context = claimProcessingDbContext;
            _mapper = mapper;
        }
        public async Task<ClaimSerialNumbersVm> Handle(GetClaimSerialNumbersQuery request, CancellationToken cancellationToken)
        {
            var serialNumbers = await _context.SerialNumbers
                .Where(a => a.StatusId != 0 && a.ClaimId == request.ClaimId)
                .ToListAsync(cancellationToken);

            var serialNumbersVm = new ClaimSerialNumbersVm
            {
                SerialNumbers = serialNumbers.Select(src => _mapper.Map<ClaimSerialNumbersDto>(src)).ToList()
            };

            return serialNumbersVm;
        }
    }
}
