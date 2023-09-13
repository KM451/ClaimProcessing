using MediatR;

namespace ClaimProcessing.Application.Claims.Queries.GetClaimsUser
{
    public class GetClaimsUserQuery : IRequest<ClaimsUserVm>
    {
        public string? Filter { get; set; }
        public string Sort { get; set; }
    }
}
