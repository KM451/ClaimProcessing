using AutoMapper;
using ClaimProcessing.Application.Common.Exceptions;
using ClaimProcessing.Application.Common.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace ClaimProcessing.Application.Shipments.Commands.UpdateShipment
{
    public class UpdateShipmentCommandHandler : IRequestHandler<UpdateShipmentCommand>
    {
        private readonly IClaimProcessingDbContext _context;
        private IMapper _mapper;
        public UpdateShipmentCommandHandler(IClaimProcessingDbContext claimProcessingDbContext, IMapper mapper)
        {
            _context = claimProcessingDbContext;
            _mapper = mapper;
        }

        public async Task Handle(UpdateShipmentCommand request, CancellationToken cancellationToken)
        {
            var shipment = await _context.Shipments.Where(s => s.Id == request.ShipmentId).FirstOrDefaultAsync(cancellationToken);
           
            if (shipment == null)
            {
                throw new NullException(request.ShipmentId);
            }
            else
            {
                _mapper.Map(request, shipment);

                await _context.SaveChangesAsync(cancellationToken);
            }
        }
    }
}

