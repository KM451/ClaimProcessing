using MediatR;

namespace ClaimProcessing.Shared.Claims.Commands.UpdateClaimRemarks
{
    public class UpdateClaimRemarksCommand : IRequest<string>
    {
        public int ClaimId { get; set; }
        public string Remarks { get; set; }

    }
}
