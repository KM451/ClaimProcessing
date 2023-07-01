using AutoMapper;
using ClaimProcessing.Application.Common.Exceptions;
using ClaimProcessing.Application.Common.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace ClaimProcessing.Application.Packagings.Commands.UpdatePackaging
{
    public class UpdatePackagingCommandHandler : IRequestHandler<UpdatePackagingCommand>
    {
        private readonly IClaimProcessingDbContext _context;
        private IMapper _mapper;

        public UpdatePackagingCommandHandler(IClaimProcessingDbContext claimProcessingDbContext, IMapper mapper)
        {
            _context = claimProcessingDbContext;
            _mapper = mapper;
        }

        public async Task Handle(UpdatePackagingCommand request, CancellationToken cancellationToken)
        {
            var packaging = await _context.Packagings.Where(p => p.Id == request.PackagingId).FirstOrDefaultAsync(cancellationToken);

            if (packaging == null)
            {
                throw new NullException(request.PackagingId);
            }
            else
            {
                _mapper.Map(request, packaging);
               
                await _context.SaveChangesAsync(cancellationToken);
            }
        }
    }
}
