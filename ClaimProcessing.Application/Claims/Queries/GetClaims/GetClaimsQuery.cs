using MediatR;

namespace ClaimProcessing.Application.Claims.Queries.GetClaims
{
    public class GetClaimsQuery : IRequest<ClaimsVm>
    {
    }
}
