using ClaimProcessing.Application.Shipments.Commands.CreateShipment;
using ClaimProcessing.Application.Shipments.Commands.DeleteShipment;
using ClaimProcessing.Application.Shipments.Commands.UpdateShipment;
using ClaimProcessing.Application.Shipments.Queries.GetShipmentClaims;
using ClaimProcessing.Application.Shipments.Queries.GetShipmentDetail;
using ClaimProcessing.Application.Shipments.Queries.GetShipmentPackagings;
using ClaimProcessing.Application.Shipments.Queries.GetShipments;
using Microsoft.AspNetCore.Mvc;

namespace ClaimProcessing.Api.Controllers
{
    [Route("api/v1/shipments")]
    public class ShipmentsController : BaseController
    {
        [HttpGet]
        public async Task<ActionResult<ShipmentsVm>> GetShipments()
        {
            var vm = await Mediator.Send(new GetShipmentsQuery());
            return vm;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ShipmentDetailVm>> GetShipmentDetail(int id)
        {
            var vm = await Mediator.Send(new GetShipmentDetailQuery() { ShipmentId = id });
            return vm;
        }

        [HttpGet("{id}/Claims")]
        public async Task<ActionResult<ShipmentClaimsVm>> GetShipmentClaims(int id)
        {
            var vm = await Mediator.Send(new GetShipmentClaimsQuery() { ShipmentId = id });
            return vm;
        }

        [HttpGet("{id}/Packagings")]
        public async Task<ActionResult<ShipmentPackagingsVm>> GetShipmentPackagings(int id)
        {
            var vm = await Mediator.Send(new GetShipmentPackagingsQuery() { ShipmentId = id });
            return vm;
        }

        [HttpPost]
        public async Task<IActionResult> CreateShipment(CreateShipmentCommand command)
        {
            var result = await Mediator.Send(command);
            return Ok(result);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateShipment(UpdateShipmentVm vm, int id)
        {
            var command = new UpdateShipmentCommand();
            command = (UpdateShipmentCommand)vm;
            command.ShipmentId = id;
            var result = await Mediator.Send(command);
            return Ok(result);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteShipment(int id)
        {
            var command = new DeleteShipmentCommand { ShipmentId = id };

            await Mediator.Send(command);
            return NoContent();
        }
    }
}
