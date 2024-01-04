using System.ComponentModel.DataAnnotations;

namespace ClaimProcessing.Client.Models
{
    public class ClaimsForm
    {
        [Required]
        public string ClaimNumber { get; set; }
        [Required]
        public string OwnerType { get; set; }
        [Required]
        public string ClaimType { get; set; }
        [Required]
        public string ItemCode { get; set; }
        [Required]
        public decimal Qty { get; set; }
        [Required]
        public string? CustomerName { get; set; }
        [Required]
        public string? ItemName { get; set; }
        [Required]
        public string? ClaimDescription { get; set; }
        public string? Remarks { get; set; }
        [Required]
        public int ClaimStatus { get; set; }
        [Required]
        public int SupplierId { get; set; }
        [Required]
        public string? SaleInvoiceNo { get; set; }
        [Required]
        public DateTime? SaleDate { get; set; }
        public string? PurchaseInvoiceNo { get; set; }
        public DateTime? PurchaseDate { get; set; }
        public string? InternalDocNo { get; set; }
    }
}
