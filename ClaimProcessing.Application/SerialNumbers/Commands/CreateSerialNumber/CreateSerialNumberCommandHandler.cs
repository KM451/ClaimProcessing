using AutoMapper;
using ClaimProcessing.Application.Common.Interfaces;
using ClaimProcessing.Domain.Entities;
using MediatR;

namespace ClaimProcessing.Application.SerialNumbers.Commands.CreateSerialNumber
{
    public class CreateSerialNumberCommandHandler : IRequestHandler<CreateSerialNumberCommand, int>
    {
        private readonly IClaimProcessingDbContext _context;
        private IMapper _mapper;
        public CreateSerialNumberCommandHandler(IClaimProcessingDbContext claimProcessingDbContext, IMapper mapper)
        {
            _context = claimProcessingDbContext;
            _mapper = mapper;
        }
        public async Task<int> Handle(CreateSerialNumberCommand request, CancellationToken cancellationToken)
        {
            var serialNumber = _mapper.Map<SerialNumber>(request);
            
            _context.SerialNumbers.Add(serialNumber);
            await _context.SaveChangesAsync(cancellationToken);
            return serialNumber.Id;
        }
    }
}
