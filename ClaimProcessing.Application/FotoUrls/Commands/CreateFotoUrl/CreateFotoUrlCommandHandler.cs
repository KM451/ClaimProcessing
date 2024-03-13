using ClaimProcessing.Application.Common.Interfaces;
using ClaimProcessing.Domain.Entities;
using ClaimProcessing.Shared.FotoUrls.Commands.CreateFotoUrl;
using MediatR;
using Microsoft.AspNetCore.Hosting;

namespace ClaimProcessing.Application.FotoUrls.Commands.CreateFotoUrl
{
    public class CreateFotoUrlCommandHandler(IClaimProcessingDbContext _context, IFileStore _fileStore,
                                             IHostingEnvironment _webHostEnvironment) : IRequestHandler<CreateFotoUrlCommand>
    {
        public async Task Handle(CreateFotoUrlCommand request, CancellationToken cancellationToken)
        {

            var x = _webHostEnvironment.WebRootPath;
            var folderRoot = Path.Combine(x, "Content", request.ClaimId.ToString(), "Fotos");
            if (!Directory.Exists(folderRoot))
                Directory.CreateDirectory(folderRoot);

            var fullPath = _fileStore.SafeWriteFile(request.Content, request.FileName, folderRoot);
            _context.FotoUrls.Add(new FotoUrl { ClaimId = request.ClaimId, Path = fullPath });
            await _context.SaveChangesAsync(cancellationToken);     
        }
    }
}
