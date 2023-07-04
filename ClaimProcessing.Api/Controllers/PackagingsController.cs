using ClaimProcessing.Application.FotoUrls.Commands.DeleteFotoUrl;
using ClaimProcessing.Application.Packagings.Commands.CreatePackaging;
using ClaimProcessing.Application.Packagings.Commands.DeletePackaging;
using ClaimProcessing.Application.Packagings.Commands.UpdatePackaging;
using ClaimProcessing.Application.Packagings.Queries.GetPackaging;
using Microsoft.AspNetCore.Mvc;

namespace ClaimProcessing.Api.Controllers
{
    [Route("api/v1/packagings")]
    public class PackagingsController : BaseController
    {
        [HttpGet("{id}")]
        public async Task<ActionResult<PackagingVm>> GetPackaging(int id)
        {
            var vm = await Mediator.Send(new GetPackagingQuery() { PackagingId = id });
            return vm;
        }

        [HttpPost]
        public async Task<IActionResult> CreatePackaging(CreatePackagingCommand command)
        {
            var result = await Mediator.Send(command);
            return Ok(result);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdatePackaging(UpdatePackagingVm vm, int id)
        {
            var command = new UpdatePackagingCommand();
            command = (UpdatePackagingCommand)vm;
            command.PackagingId = id;
            var result = await Mediator.Send(command);
            return Ok(result);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePackaging(int id)
        {
            var command = new DeletePackagingCommand { PackagingId = id };
            await Mediator.Send(command);
            return NoContent();
        }
    }
    
}
