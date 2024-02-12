using MediatR;

namespace ClaimProcessing.Shared.Claims.Queries.GetAllClaimsShort
{
    public class GetAllClaimsShortQuery : IRequest<AllClaimsShortVm>
    {
        public string? Filter { get; set; }
        public string Sort { get; set; }

    }
}
