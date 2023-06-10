using MediatR;

namespace ClaimProcessing.Application.SerialNumbers.Commands.UpdateSerialNumber
{
    public class UpdateSerialNumberCommand : IRequest
    {
        public int SerialNumberId { get; set; }
        public int ClaimId { get; set; }
        public string Value { get; set; }
    }
}
