namespace ClaimProcessing.Api
{
    public class PackagingForView
    {
        public int Id { get; set; }
        public int ShipmentId { get; set; }
        public string Type { get; set; }
        public string Dimensions { get; set; }
        public double Weight { get; set; }
        public string? Notes { get; set; }
    }
}
