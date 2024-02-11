using MediatR;

namespace ClaimProcessing.Shared.FotoUrls.Queries.GetFotoUrl
{
    public class GetFotoUrlQuery : IRequest<FotoUrlVm>
    {
        public int FotoUrlId { get; set; }
    }
}
