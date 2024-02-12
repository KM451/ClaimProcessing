﻿using ClaimProcessing.Shared.Packagings.Commands.CreatePackaging;
using ClaimProcessing.Shared.Packagings.Commands.UpdatePackaging;
using ClaimProcessing.Shared.Packagings.Queries.GetPackaging;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

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
        [Authorize(Policy = "ApiUser12")]
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
        [Authorize(Policy = "ApiUser2")]
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
        [HttpPut]
        [Authorize(Policy = "ApiUser2")]
        public async Task<IActionResult> UpdatePackaging(UpdatePackagingCommand command)
        {
            var result = await Mediator.Send(command);
            return Ok(result);
        }

        /// <summary>
        /// Delete the Packaging specified by Id number
        /// </summary>
        /// <param name="id">The Packaging Id number</param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        [Authorize(Policy = "ApiUser2")]
        public async Task<IActionResult> DeletePackaging(int id)
        {
            var command = new DeletePackagingCommand { PackagingId = id };
            await Mediator.Send(command);
            return NoContent();
        }
    }
    
}
