namespace ClaimProcessing.Client.Models
{
    public class AllClaimsShortDto
    {
        public int ClaimId { get; set; }
        public string ClaimNumber { get; set; }
        public DateTime ClaimCreationDate { get; set; }
        public string SupplierName { get; set; }
        public string ItemCode { get; set; }
        public int ClaimStatus { get; set; }
    }
}
