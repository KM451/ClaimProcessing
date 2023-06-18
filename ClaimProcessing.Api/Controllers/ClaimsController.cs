using ClaimProcessing.Application.Claims.Queries.GetClaimDetail;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;

namespace ClaimProcessing.Api.Controllers
{

    [Route("api/v1/claims")]
    //[EnableCors("MyAllowSpecificOrgins")]
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

    }
}



