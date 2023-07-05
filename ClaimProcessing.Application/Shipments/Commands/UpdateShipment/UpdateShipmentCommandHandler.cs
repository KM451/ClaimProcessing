using AutoMapper;
using ClaimProcessing.Application.Common.Interfaces;
using ClaimProcessing.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace ClaimProcessing.Application.Shipments.Commands.UpdateShipment
{
    public class UpdateShipmentCommandHandler : IRequestHandler<UpdateShipmentCommand, int>
    {
        private readonly IClaimProcessingDbContext _context;
        private IMapper _mapper;
        public UpdateShipmentCommandHandler(IClaimProcessingDbContext claimProcessingDbContext, IMapper mapper)
        {
            _context = claimProcessingDbContext;
            _mapper = mapper;
        }

        public async Task<int> Handle(UpdateShipmentCommand request, CancellationToken cancellationToken)
        {
            var shipment = await _context.Shipments.Where(s => s.Id == request.ShipmentId).FirstOrDefaultAsync(cancellationToken);
           
            if (shipment == null)
            {
                request.ShipmentId = 0;
                shipment = _mapper.Map<Shipment>(request);
                _context.Shipments.Add(shipment);
            }
            else
            {
                _mapper.Map(request, shipment); 
            }

            await _context.SaveChangesAsync(cancellationToken);
            return shipment.Id;
        }
    }
}

