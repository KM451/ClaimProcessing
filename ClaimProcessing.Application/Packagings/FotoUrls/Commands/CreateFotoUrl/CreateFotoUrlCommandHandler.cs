using AutoMapper;
using ClaimProcessing.Application.Common.Interfaces;
using ClaimProcessing.Domain.Entities;
using MediatR;

namespace ClaimProcessing.Application.FotoUrls.Commands.CreateFotoUrl
{
    public class CreateFotoUrlCommandHandler : IRequestHandler<CreateFotoUrlCommand, int>
    {
        private readonly IClaimProcessingDbContext _context;
        private IMapper _mapper;
        public CreateFotoUrlCommandHandler(IClaimProcessingDbContext claimProcessingDbContext, IMapper mapper)
        {
            _context = claimProcessingDbContext;
            _mapper = mapper;
        }
        public async Task<int> Handle(CreateFotoUrlCommand request, CancellationToken cancellationToken)
        {
            var fotoUrl = _mapper.Map<FotoUrl>(request);
            
            _context.FotoUrls.Add(fotoUrl);
            await _context.SaveChangesAsync(cancellationToken);
            return fotoUrl.Id;
        }
    }
}
