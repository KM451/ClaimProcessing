using AutoMapper;
using ClaimProcessing.Application.Common.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace ClaimProcessing.Application.SerialNumbers.Queries.GetSerialNumberDetail
{
    public class GetSerialNumberDetailQueryHandler : IRequestHandler<GetSerialNumberDetailQuery, SerialNumberDetailVm>
    {
        private readonly IClaimProcessingDbContext _context;
        private IMapper _mapper;
        public GetSerialNumberDetailQueryHandler(IClaimProcessingDbContext claimProcessingDbContext, IMapper mapper)
        {
            _context = claimProcessingDbContext;
            _mapper = mapper;
        }
        public async Task<SerialNumberDetailVm> Handle(GetSerialNumberDetailQuery request, CancellationToken cancellationToken)
        {
            var serialNumber = await _context.SerialNumbers
                .Where(s => s.StatusId != 0 && s.Id == request.SerialNumberId)
                .FirstOrDefaultAsync(cancellationToken);
            var serialNumberVm = _mapper.Map<SerialNumberDetailVm>(serialNumber);
            return serialNumberVm;
        }
    }
}
