using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClaimProcessing.Application.FotoUrls.Queries.GetFotoUrlDetail
{
    public class GetFotoUrlDetailQuery : IRequest<FotoUrlDetailVm>
    {
        public int FotoUrlId { get; set; }
    }
}
