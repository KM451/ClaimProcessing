using MediatR;

namespace ClaimProcessing.Application.SerialNumbers.Queries.GetSerialNumberDetail
{
    public class GetSerialNumberDetailQuery : IRequest<SerialNumberDetailVm>
    {
        public int SerialNumberId { get; set; }
    }
}
