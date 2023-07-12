using ClaimProcessing.Application.SerialNumbers.Commands.CreateSerialNumber;
using ClaimProcessing.Application.SerialNumbers.Commands.DeleteSerialNumber;
using ClaimProcessing.Application.SerialNumbers.Queries.GetSerialNumber;
using Microsoft.AspNetCore.Mvc;

namespace ClaimProcessing.Api.Controllers
{
    [Route("api/v1/serial-numbers")]
    public class SerialNumbersController : BaseController
    {
        /// <summary>
        /// Get the SerialNumber specified by Id number
        /// </summary>
        /// <param name="id">Id of SerialNumber</param>
        /// <returns></returns>
        [HttpGet("{id}")]
        public async Task<ActionResult<SerialNumberVm>> GetSerialNumber(int id)
        {
            var vm = await Mediator.Send(new GetSerialNumberQuery() { SerialNumberId = id });
            return vm;
        }

        /// <summary>
        /// Add the item SerialNumber to the Claim
        /// </summary>
        /// <param name="command">Data of the new SerialNumber object</param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> CreateSerialNumber(CreateSerialNumberCommand command)
        {
            var result = await Mediator.Send(command);
            return Ok(result);
        }

        /// <summary>
        /// Delete the SerialNumber specified by Id number
        /// </summary>
        /// <param name="id">Id number of SerialNumber</param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteSerialNumber(int id)
        {
            var command = new DeleteSerialNumberCommand { SerialNumberId = id };
            await Mediator.Send(command);
            return NoContent();
        }
    }
}
