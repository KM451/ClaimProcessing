using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClaimProcessing.Shared.Filter
{
    public class  FilterVm
    {
        public string OwnerType { get; set; }
        public string ClaimType { get; set; }
        public string ItemCode { get; set; }
        public string CustomerName { get; set; }
        public string ClaimStatus { get; set; }
        public bool RmaAvailable { get; set; }
        public int SupplierId { get; set; }
    }
}
