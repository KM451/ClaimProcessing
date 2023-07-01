using ClaimProcessing.Application.Packagings.Commands.CreatePackaging;
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

        [HttpPatch("{id}")]
        public async Task<IActionResult> UpdatePackaging(UpdatePackagingCommand command, int id)
        {
            command.PackagingId = id;
            await Mediator.Send(command);
            return Ok();
        }
    }
    
}
