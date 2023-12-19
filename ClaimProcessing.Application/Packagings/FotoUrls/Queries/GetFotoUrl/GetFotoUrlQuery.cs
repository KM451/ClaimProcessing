using MediatR;

namespace ClaimProcessing.Application.FotoUrls.Queries.GetFotoUrl
{
    public class GetFotoUrlQuery : IRequest<FotoUrlVm>
    {
        public int FotoUrlId { get; set; }
    }
}
