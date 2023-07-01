using MediatR;

namespace ClaimProcessing.Application.Claims.Queries.GetClaimSerialNumbers
{
    public class GetClaimSerialNumbersQuery : IRequest<ClaimSerialNumbersVm>
    {
        public int ClaimId { get; set; }
    }
}
