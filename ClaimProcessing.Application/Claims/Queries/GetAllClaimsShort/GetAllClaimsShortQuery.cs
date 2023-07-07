using MediatR;

namespace ClaimProcessing.Application.Claims.Queries.GetAllClaimsShort
{
    public class GetAllClaimsShortQuery : IRequest<AllClaimsShortVm>
    {
        public string? Filter { get; set; }
        public string Sort { get; set; }

    }
}
