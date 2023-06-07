namespace ClaimProcessing.Application.Claims.Queries.GetAllClaimsShort
{
    public class AllClaimsShortDto
    {
        public int ClaimId { get; set; }
        public DateTime ClaimCreationDate { get; set; }
        public string SupplierName { get; set;}
        public string ItemCode { get; set;}
        public string ClaimStatus { get; set; }
    }
}
