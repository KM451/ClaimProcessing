using MediatR;

namespace ClaimProcessing.Shared.Claims.Commands.DeleteClaim
{
    public class DeleteClaimCommand : IRequest
    {
        public int ClaimId { get; set; }
    }
}
