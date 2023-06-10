using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClaimProcessing.Application.Claims.Queries.GetClaimsSupplier
{
    public class GetClaimsSupplierQuery : IRequest<ClaimsSupplierVm>
    {
        public int SupplierId { get; set; }
    }
}
