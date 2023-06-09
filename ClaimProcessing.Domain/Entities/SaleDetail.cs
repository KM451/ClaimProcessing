﻿using ClaimProcessing.Domain.Common;

namespace ClaimProcessing.Domain.Entities
{
    public class SaleDetail : AuditableEntity
    {
        public string SaleInvoiceNo { get; set; }
        public DateTime SaleDate { get; set; }
        public int ClaimId { get; set; }
        public Claim Claim { get; set; }
    }
}
