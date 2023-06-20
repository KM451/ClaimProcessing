using AutoMapper;
using ClaimProcessing.Application.Common.Interfaces;
using ClaimProcessing.Domain.Entities;
using MediatR;

namespace ClaimProcessing.Application.Shipments.Commands.CreateShipment
{
    public class CreateShipmentCommandHandler : IRequestHandler<CreateShipmentCommand, int>
    {
        private readonly IClaimProcessingDbContext _context;
        private IMapper _mapper;
        public CreateShipmentCommandHandler(IClaimProcessingDbContext claimProcessingDbContext, IMapper mapper)
        {
            _context = claimProcessingDbContext;
            _mapper = mapper;
        }
        public async Task<int> Handle(CreateShipmentCommand request, CancellationToken cancellationToken)
        {
            var shipment = _mapper.Map<Shipment>(request);
            
            _context.Shipments.Add(shipment);
            await _context.SaveChangesAsync(cancellationToken);
            return shipment.Id;
        }
    }
}
