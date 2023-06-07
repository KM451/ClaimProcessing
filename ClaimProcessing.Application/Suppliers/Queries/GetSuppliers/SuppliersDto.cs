using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClaimProcessing.Application.Suppliers.Queries.GetSuppliers
{
    public class SuppliersDto
    {
        public int SupplierId { get; set; }
        public string Name { get; set; }
        public string City { get; set; }
    }
}
