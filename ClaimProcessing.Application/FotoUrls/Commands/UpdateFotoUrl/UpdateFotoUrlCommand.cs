using MediatR;

namespace ClaimProcessing.Application.FotoUrls.Commands.UpdateFotoUrl
{
    public class UpdateFotoUrlCommand : IRequest
    {
        public int FotoUrlId { get; set; }
        public int ClaimId { get; set; }
        public string Path { get; set; }
    }
}
