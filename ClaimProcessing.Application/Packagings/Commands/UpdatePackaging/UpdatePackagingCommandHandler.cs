using AutoMapper;
using ClaimProcessing.Application.Common.Interfaces;
using ClaimProcessing.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace ClaimProcessing.Application.Packagings.Commands.UpdatePackaging
{
    public class UpdatePackagingCommandHandler : IRequestHandler<UpdatePackagingCommand, int>
    {
        private readonly IClaimProcessingDbContext _context;
        private IMapper _mapper;

        public UpdatePackagingCommandHandler(IClaimProcessingDbContext claimProcessingDbContext, IMapper mapper)
        {
            _context = claimProcessingDbContext;
            _mapper = mapper;
        }

        public async Task<int> Handle(UpdatePackagingCommand request, CancellationToken cancellationToken)
        {
            var packaging = await _context.Packagings.Where(p => p.Id == request.PackagingId).FirstOrDefaultAsync(cancellationToken);

            if (packaging == null)
            {
                request.PackagingId = 0;
                packaging = _mapper.Map<Packaging>(request);
                _context.Packagings.Add(packaging);
            }
            else
            {
                _mapper.Map(request, packaging);
            }

            await _context.SaveChangesAsync(cancellationToken);

            return packaging.Id;
        }
    }
}
