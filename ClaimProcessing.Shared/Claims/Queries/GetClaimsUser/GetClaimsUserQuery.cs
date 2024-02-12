using MediatR;

namespace ClaimProcessing.Shared.Claims.Queries.GetClaimsUser
{
    public class GetClaimsUserQuery : IRequest<ClaimsUserVm>
    {
        public string? Filter { get; set; }
        public string Sort { get; set; }
    }
}
