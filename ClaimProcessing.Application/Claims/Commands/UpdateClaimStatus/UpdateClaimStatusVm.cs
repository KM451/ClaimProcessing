namespace ClaimProcessing.Application.Claims.Commands.UpdateClaimStatus
{
    public class UpdateClaimStatusVm
    {
        public BonfiClaim data { get; set; }
    }

    public class ClaimData
    {
        public string status { get; set; }
        public string subject { get; set; }
        public string materialNumber { get; set; }
        public string serialNumber { get; set; }
        public string documentStartWarranty { get; set; }
        public string dateDocumentStartWarranty { get; set; }
        public string defectLocation { get; set; }
        public string defectType { get; set; }
        public string problemDescription { get; set; }
        public string detectionPhase { get; set; }
        public string businessUnit { get; set; }
        public string productPurchasedFrom { get; set; }
        public string createdDate { get; set; }
        public string claimId { get; set; }
        public string quantity { get; set; }
    }

    public class BonfiClaim
    {
        public ClaimData claim { get; set; }
    }
}
