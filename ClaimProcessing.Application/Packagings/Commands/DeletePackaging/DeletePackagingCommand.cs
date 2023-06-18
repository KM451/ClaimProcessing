using MediatR;

namespace ClaimProcessing.Application.Packagings.Commands.DeletePackaging
{
    public class DeletePackagingCommand : IRequest
    {
        public int PackagingId { get; set; }
    }
}
