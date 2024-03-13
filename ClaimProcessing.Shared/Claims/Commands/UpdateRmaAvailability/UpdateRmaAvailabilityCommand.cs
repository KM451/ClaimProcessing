using MediatR;

namespace ClaimProcessing.Shared.Claims.Commands.UpdateRmaAvailability
{
    public class UpdateRmaAvailabilityCommand : IRequest
    {
        public int ClaimId { get; set; }
        public bool RmaAvailable { get; set; }

    }
}
