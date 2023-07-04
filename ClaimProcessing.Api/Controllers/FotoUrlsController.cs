using ClaimProcessing.Application.Claims.Commands.DeleteClaim;
using ClaimProcessing.Application.FotoUrls.Commands.CreateFotoUrl;
using ClaimProcessing.Application.FotoUrls.Commands.DeleteFotoUrl;
using ClaimProcessing.Application.FotoUrls.Queries.GetFotoUrl;
using Microsoft.AspNetCore.Mvc;

namespace ClaimProcessing.Api.Controllers
{
    [Route("api/v1/foto-urls")]
    public class FotoUrlsController : BaseController
    {
        [HttpGet("{id}")]
        public async Task<ActionResult<FotoUrlVm>> GetFotoUrl(int id)
        {
            var vm = await Mediator.Send(new GetFotoUrlQuery() { FotoUrlId = id });
            return vm;
        }

        [HttpPost]
        public async Task<IActionResult> CreateFotoUrl(CreateFotoUrlCommand command)
        {
            var result = await Mediator.Send(command);
            return Ok(result);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteFotoUrl(int id)
        {
            var command = new DeleteFotoUrlCommand { FotoUrlId = id };
            await Mediator.Send(command);
            return NoContent();
        }
    }
}
