using ClaimProcessing.Application.SerialNumbers.Commands.CreateSerialNumber;
using ClaimProcessing.Application.SerialNumbers.Commands.DeleteSerialNumber;
using ClaimProcessing.Application.SerialNumbers.Queries.GetSerialNumber;
using Microsoft.AspNetCore.Mvc;

namespace ClaimProcessing.Api.Controllers
{
    [Route("api/v1/serial-numbers")]
    public class SerialNumbersController : BaseController
    {
        [HttpGet("{id}")]
        public async Task<ActionResult<SerialNumberVm>> GetSerialNumber(int id)
        {
            var vm = await Mediator.Send(new GetSerialNumberQuery() { SerialNumberId = id });
            return vm;
        }

        [HttpPost]
        public async Task<IActionResult> CreateSerialNumber(CreateSerialNumberCommand command)
        {
            var result = await Mediator.Send(command);
            return Ok(result);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteSerialNumber(int id)
        {
            var command = new DeleteSerialNumberCommand { SerialNumberId = id };
            await Mediator.Send(command);
            return NoContent();
        }
    }
}
