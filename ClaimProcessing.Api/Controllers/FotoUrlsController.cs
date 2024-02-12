using ClaimProcessing.Application.FotoUrls.Commands.CreateFotoUrl;
using ClaimProcessing.Application.FotoUrls.Commands.DeleteFotoUrl;
using ClaimProcessing.Application.FotoUrls.Queries.GetFotoUrl;
using ClaimProcessing.Shared.FotoUrls.Commands.CreateFotoUrl;
using ClaimProcessing.Shared.FotoUrls.Commands.DeleteFotoUrl;
using ClaimProcessing.Shared.FotoUrls.Queries.GetFotoUrl;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ClaimProcessing.Api.Controllers
{
    [Route("api/v1/foto-urls")]
    public class FotoUrlsController : BaseController
    {
        /// <summary>
        /// Get the FotoUrl path specified by it Id number
        /// </summary>
        /// <param name="id">Id of FotoUrl</param>
        /// <returns></returns>
        [HttpGet("{id}")]
        [Authorize(Policy = "ApiUser12")]
        public async Task<ActionResult<FotoUrlVm>> GetFotoUrl(int id)
        {
            var vm = await Mediator.Send(new GetFotoUrlQuery() { FotoUrlId = id });
            return vm;
        }

        
        /// <summary>
        /// Add the FotoUrl path to the Claim
        /// </summary>
        /// <param name="command">Data of the new FotoUrl object</param>
        /// <returns></returns>
        [HttpPost]
        [Authorize(Policy = "ApiUser1")]
        public async Task<IActionResult> CreateFotoUrl(CreateFotoUrlCommand command)
        {
            await Mediator.Send(command);
            return Ok();
        }

       
        /// <summary>
        /// Delete the FotoUrl path specified by Id number
        /// </summary>
        /// <param name="id">FotoUrl Id number</param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        [Authorize(Policy = "ApiUser1")]
        public async Task<IActionResult> DeleteFotoUrl(int id)
        {
            var command = new DeleteFotoUrlCommand { FotoUrlId = id };
            await Mediator.Send(command);
            return NoContent();
        }
    }
}
