using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClaimProcessing.Application.Claims.Commands.DeleteClaim
{
    public class DeleteClaimCommand : IRequest
    {
        public int ClaimId { get; set; }
    }
}
