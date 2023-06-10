using MediatR;

namespace ClaimProcessing.Application.FotoUrls.Queries.GetFotosUrlsClaim
{
    public class GetFotosUrlsClaimQuery : IRequest<FotosUrlsClaimVm>
    {
        public int ClaimId { get; set; }
    }
}
