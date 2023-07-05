namespace ClaimProcessing.Application.Claims.Commands.UpdateClaim
{
    public class UpdateClaimVm 
    {
        public string ClaimNumber { get; set; }
        public string OwnerType { get; set; }
        public string ClaimType { get; set; }
        public string ItemCode { get; set; }
        public decimal Qty { get; set; }
        public string? CustomerName { get; set; }
        public string? ItemName { get; set; }
        public string? ClaimDescription { get; set; }
        public string? Remarks { get; set; }
        public int ClaimStatus { get; set; }
        public int SupplierId { get; set; }
        public string? SaleInvoiceNo { get; set; }
        public DateTime? SaleDate { get; set; }
        public string? PurchaseInvoiceNo { get; set; }
        public DateTime? PurchaseDate { get; set; }
        public string? InternalDocNo { get; set; }
        public bool RmaAvailable { get; set; }
        public int? ShipmentId { get; set; }

        public static explicit operator UpdateClaimCommand(UpdateClaimVm updateClaimVm)
        {
            var command = new UpdateClaimCommand
            {
                ClaimId = 0,
                ClaimNumber = updateClaimVm.ClaimNumber,
                OwnerType = updateClaimVm.OwnerType,
                ClaimType = updateClaimVm.ClaimType,
                ItemCode = updateClaimVm.ItemCode,
                Qty = updateClaimVm.Qty,
                CustomerName = updateClaimVm.CustomerName,
                ItemName = updateClaimVm.ItemName,
                ClaimDescription = updateClaimVm.ClaimDescription,
                Remarks = updateClaimVm?.Remarks,
                ClaimStatus = updateClaimVm.ClaimStatus,
                SupplierId = updateClaimVm.SupplierId,
                SaleInvoiceNo = updateClaimVm.SaleInvoiceNo,
                SaleDate = updateClaimVm.SaleDate,
                PurchaseInvoiceNo = updateClaimVm.PurchaseInvoiceNo,
                PurchaseDate = updateClaimVm.PurchaseDate,
                InternalDocNo = updateClaimVm.InternalDocNo,
                RmaAvailable = updateClaimVm.RmaAvailable,
                ShipmentId = updateClaimVm.ShipmentId
            };
            return command;
        }
    }
}
