using MediatR;

namespace ClaimProcessing.Application.FotoUrls.Commands.DeleteFotoUrl
{
    public class DeleteFotoUrlCommand : IRequest
    {
        public int FotoUrlId { get; set; }
    }
}
