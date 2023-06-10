using MediatR;

namespace ClaimProcessing.Application.SerialNumbers.Queries.GetSerialNumbersClaim
{
    public class GetSerialNumbersClaimQuery : IRequest<SerialNumbersClaimVm>
    {
        public int ClaimId { get; set; }
    }
}
