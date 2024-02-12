namespace ClaimProcessing.Shared.Packagings.Queries.GetPackaging
{
    public class PackagingVm 
    {
        public string Type { get; set; }
        public string Dimensions{ get; set; }
        public decimal Weight { get; set; }
        public string Notes { get; set; }
        public int ShipmentId { get; set; }
    }
}
