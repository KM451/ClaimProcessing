using ClaimProcessing.Application.Claims.Commands.CreateClaim;
using ClaimProcessing.Application.Claims.Queries.GetClaimDetail;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;

namespace ClaimProcessing.Api.Controllers
{

    [Route("api/v1/claims")]

    public class ClaimsController : BaseController
    {
        /// <summary>
        /// Get the detail data of the claim with given it ID number
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}")]
        public async Task<ActionResult<ClaimDetailVm>> GetDetails(int id)
        {
            var vm = await Mediator.Send(new GetClaimDetailQuery() { ClaimId = id });
            return vm;
        }

        /// <summary>
        /// Create the new claim case
        /// </summary>
        /// <param name="command"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> CreateClaim(CreateClaimCommand command)
        {
            var result = await Mediator.Send(command);
            return Ok(result);
        }

    }
}



