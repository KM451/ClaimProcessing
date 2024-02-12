using MediatR;

namespace ClaimProcessing.Shared.FotoUrls.Commands.CreateFotoUrl
{
    public class CreateFotoUrlCommand : IRequest
    {
        public int ClaimId { get; set; }
        public string FileName { get; set; }
        public byte[] Content { get; set; }

    }
}
