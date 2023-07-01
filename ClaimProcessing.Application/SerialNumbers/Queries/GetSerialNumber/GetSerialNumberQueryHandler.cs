using AutoMapper;
using ClaimProcessing.Application.Common.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace ClaimProcessing.Application.SerialNumbers.Queries.GetSerialNumber
{
    public class GetSerialNumberQueryHandler : IRequestHandler<GetSerialNumberQuery, SerialNumberVm>
    {
        private readonly IClaimProcessingDbContext _context;
        private IMapper _mapper;
        public GetSerialNumberQueryHandler(IClaimProcessingDbContext claimProcessingDbContext, IMapper mapper)
        {
            _context = claimProcessingDbContext;
            _mapper = mapper;
        }
        public async Task<SerialNumberVm> Handle(GetSerialNumberQuery request, CancellationToken cancellationToken)
        {
            var serialNumber = await _context.SerialNumbers
                .Where(s => s.StatusId != 0 && s.Id == request.SerialNumberId)
                .FirstOrDefaultAsync(cancellationToken);
            var serialNumberVm = _mapper.Map<SerialNumberVm>(serialNumber);
            return serialNumberVm;
        }
    }
}
