using MediatR;

namespace ClaimProcessing.Application.SerialNumbers.Commands.CreateSerialNumber
{
    public class CreateSerialNumberCommand : IRequest<int>
    {
        public int ClaimId { get; set; }
        public string Value { get; set; }
    }
}
