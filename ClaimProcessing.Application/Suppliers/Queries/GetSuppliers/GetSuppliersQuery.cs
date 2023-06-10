using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClaimProcessing.Application.Suppliers.Queries.GetSuppliers
{
    public class GetSuppliersQuery : IRequest<SuppliersVm>
    {
    }
}
