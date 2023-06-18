using MediatR;

namespace ClaimProcessing.Application.Packagings.Commands.UpdatePackaging
{
    public class UpdatePackagingCommand : IRequest
    {
        public int PackagingId { get; set; }
        public string Type { get; set; }
        public double Height { get; set; }
        public double Width { get; set; }
        public double Depth { get; set; }
        public decimal Weight { get; set; }
        public string Notes { get; set; }
        public int ShipmentId { get; set; }
    }
}
