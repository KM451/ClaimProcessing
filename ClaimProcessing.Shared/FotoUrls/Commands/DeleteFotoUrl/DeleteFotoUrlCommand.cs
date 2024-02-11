using MediatR;

namespace ClaimProcessing.Shared.FotoUrls.Commands.DeleteFotoUrl
{
    public class DeleteFotoUrlCommand : IRequest
    {
        public int FotoUrlId { get; set; }
    }
}
