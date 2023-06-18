using MediatR;

namespace ClaimProcessing.Application.SerialNumbers.Commands.DeleteSerialNumber
{
    public class DeleteSerialNumberCommand : IRequest
    {
        public int SerialNumberId { get; set; }
    }
}
