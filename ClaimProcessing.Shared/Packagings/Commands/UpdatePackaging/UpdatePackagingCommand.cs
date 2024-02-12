using MediatR;

namespace ClaimProcessing.Shared.Packagings.Commands.UpdatePackaging
{
    public class UpdatePackagingCommand : IRequest<int>
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
