using MediatR;

namespace ClaimProcessing.Shared.SerialNumbers.Queries.GetSerialNumber
{
    public class DeleteSerialNumberCommand : IRequest
    {
        public int SerialNumberId { get; set; }
    }
}
