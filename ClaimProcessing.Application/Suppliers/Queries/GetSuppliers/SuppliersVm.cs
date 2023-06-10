using ClaimProcessing.Application.Shipments.Queries.GetShipments;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClaimProcessing.Application.Suppliers.Queries.GetSuppliers
{
    public class SuppliersVm
    {
        public ICollection<SuppliersDto> Suppliers { get; set; }
    }
}
