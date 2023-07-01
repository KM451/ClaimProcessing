using AutoMapper;
using ClaimProcessing.Application.Common.Exceptions;
using ClaimProcessing.Application.Common.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClaimProcessing.Application.Claims.Commands.UpdateShipmentId
{
    public class UpdateShipmentIdCommandHandler : IRequestHandler<UpdateShipmentIdCommand>
    {
        private readonly IClaimProcessingDbContext _context;
        private IMapper _mapper;
        public UpdateShipmentIdCommandHandler(IClaimProcessingDbContext claimProcessingDbContext, IMapper mapper)
        {
            _context = claimProcessingDbContext;
            _mapper = mapper;
        }

        public async Task Handle(UpdateShipmentIdCommand request, CancellationToken cancellationToken)
        {
            var claim = await _context.Claims.Where(c => c.Id == request.ClaimId).FirstOrDefaultAsync(cancellationToken);

            if (claim == null)
            {
                throw new NullException(request.ClaimId);
            }
            else
            {
                claim = _mapper.Map(request, claim);

                await _context.SaveChangesAsync(cancellationToken);
            }
        }
    }
}
