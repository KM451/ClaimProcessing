namespace ClaimProcessing.Shared.Claims.Queries.GetClaimsUser
{
    public class ClaimsUserDto
    {
        public string ClaimNumber { get; set; }
        public DateTime ClaimCreationDate { get; set; }
        public string ItemCode { get; set; }
        public string ItemName { get; set; }
        public int ClaimStatus { get; set; }

    }
}
