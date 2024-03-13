using MediatR;

namespace ClaimProcessing.Shared.Claims.Commands.UpdateClaimStatus
{
    public class UpdateClaimStatusCommand : IRequest<int>
    {
        public int ClaimId { get; set; }
        public int ClaimStatus { get; set; }

    }
}
