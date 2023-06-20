using AutoMapper;
using ClaimProcessing.Application.Common.Interfaces;
using ClaimProcessing.Domain.Entities;
using MediatR;

namespace ClaimProcessing.Application.Packagings.Commands.CreatePackaging
{
    public class CreatePackagingCommandHandler : IRequestHandler<CreatePackagingCommand, int>
    {

        private readonly IClaimProcessingDbContext _context;
        private IMapper _mapper;
        public CreatePackagingCommandHandler(IClaimProcessingDbContext claimProcessingDbContext, IMapper mapper)
        {
            _context = claimProcessingDbContext;
            _mapper = mapper;
        }
        public async Task<int> Handle(CreatePackagingCommand request, CancellationToken cancellationToken)
        {
            var packaging = _mapper.Map<Packaging>(request);
            
            _context.Packagings.Add(packaging);

            await _context.SaveChangesAsync(cancellationToken);
            return packaging.Id;
        }
    }
}
