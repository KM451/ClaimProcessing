using ClaimProcessing.Application.Packagings.Commands.CreatePackaging;
using ClaimProcessing.Application.Packagings.Commands.DeletePackaging;
using ClaimProcessing.Application.Packagings.Commands.UpdatePackaging;
using ClaimProcessing.Application.Packagings.Queries.GetPackaging;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Data;

namespace ClaimProcessing.Api.Controllers
{
    [Route("api/v1/packagings")]
    public class PackagingsController : BaseController
    {
        /// <summary>
        /// Get the Packaging data specified by Id number
        /// </summary>
        /// <param name="id">Id of Packaging</param>
        /// <returns></returns>
        [HttpGet("{id}")]
        [Authorize(Roles = "Admin,Staff1,Staff2")]
        public async Task<ActionResult<PackagingVm>> GetPackaging(int id)
        {
            var vm = await Mediator.Send(new GetPackagingQuery() { PackagingId = id });
            return vm;
        }

        /// <summary>
        /// Add the Packaging to the Shipment 
        /// </summary>
        /// <param name="command">Data of the new Packaging</param>
        /// <returns></returns>
        [HttpPost]
        [Authorize(Roles = "Admin,Staff2")]
        public async Task<IActionResult> CreatePackaging(CreatePackagingCommand command)
        {
            var result = await Mediator.Send(command);
            return Ok(result);
        }

        /// <summary>
        /// Update the Packaging data specified by Id number or create the new if given Id not exists.
        /// </summary>
        /// <param name="command">Data of the Packaging</param>
        /// <param name="id">The Packaging id number</param>
        /// <returns></returns>
        [HttpPut("{id}")]
        [Authorize(Roles = "Admin,Staff2")]
        public async Task<IActionResult> UpdatePackaging(UpdatePackagingCommand command, int id)
        {
            command.SetId(id);
            var result = await Mediator.Send(command);
            return Ok(result);
        }

        /// <summary>
        /// Delete the Packaging specified by Id number
        /// </summary>
        /// <param name="id">The Packaging Id number</param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin,Staff2")]
        public async Task<IActionResult> DeletePackaging(int id)
        {
            var command = new DeletePackagingCommand { PackagingId = id };
            await Mediator.Send(command);
            return NoContent();
        }
    }
    
}
