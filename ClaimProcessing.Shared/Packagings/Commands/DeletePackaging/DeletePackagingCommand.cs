using MediatR;

namespace ClaimProcessing.Shared.Packagings.Commands.UpdatePackaging
{
    public class DeletePackagingCommand : IRequest
    {
        public int PackagingId { get; set; }
    }
}
