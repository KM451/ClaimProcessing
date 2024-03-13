using MediatR;

namespace ClaimProcessing.Shared.SerialNumbers.Queries.GetSerialNumber
{
    public class GetSerialNumberQuery : IRequest<SerialNumberVm>
    {
        public int SerialNumberId { get; set; }
    }
}
