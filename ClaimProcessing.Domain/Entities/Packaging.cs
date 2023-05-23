using ClaimProcessing.Domain.Common;
using ClaimProcessing.Domain.ValueObjects;

namespace ClaimProcessing.Domain.Entities
{
    public class Packaging : AuditableEntity
    {
        public string Type { get; set; }
        public Dimensions Dimensions { get; set; }
        public decimal Weight { get; set; }
        public string Notes { get; set; }
        public int ShipmentId { get; set; }
        public Shipment Shipment { get; set; }
    }
}
