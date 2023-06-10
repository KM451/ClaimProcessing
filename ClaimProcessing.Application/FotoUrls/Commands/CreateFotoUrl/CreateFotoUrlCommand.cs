using MediatR;

namespace ClaimProcessing.Application.FotoUrls.Commands.CreateFotoUrl
{
    public class CreateFotoUrlCommand : IRequest<int>
    {
        public int ClaimId { get; set; }
        public string Path { get; set; }
    }
}
