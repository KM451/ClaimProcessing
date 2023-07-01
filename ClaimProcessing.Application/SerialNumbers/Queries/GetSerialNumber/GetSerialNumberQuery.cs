using MediatR;

namespace ClaimProcessing.Application.SerialNumbers.Queries.GetSerialNumber
{
    public class GetSerialNumberQuery : IRequest<SerialNumberVm>
    {
        public int SerialNumberId { get; set; }
    }
}
