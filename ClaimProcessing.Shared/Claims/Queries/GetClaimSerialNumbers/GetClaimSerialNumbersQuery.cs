using MediatR;

namespace ClaimProcessing.Shared.Claims.Queries.GetClaimSerialNumbers
{
    public class GetClaimSerialNumbersQuery : IRequest<ClaimSerialNumbersVm>
    {
        public int ClaimId { get; set; }
    }
}
