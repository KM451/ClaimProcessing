using ClaimProcessing.Application.Shipments.Commands.CreateShipment;
using ClaimProcessing.Application.Shipments.Commands.DeleteShipment;
using ClaimProcessing.Application.Shipments.Commands.UpdateShipment;
using ClaimProcessing.Application.Shipments.Queries.GetShipmentClaims;
using ClaimProcessing.Application.Shipments.Queries.GetShipmentDetail;
using ClaimProcessing.Application.Shipments.Queries.GetShipmentPackagings;
using ClaimProcessing.Application.Shipments.Queries.GetShipments;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Data;

namespace ClaimProcessing.Api.Controllers
{
    [Route("api/v1/shipments")]
    public class ShipmentsController : BaseController
    {
        /// <summary>
        /// Get the list of Shipmets
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Authorize(Policy = "ApiUser12")]
        public async Task<ActionResult<ShipmentsVm>> GetShipments()
        {
            var vm = await Mediator.Send(new GetShipmentsQuery());
            return vm;
        }

        /// <summary>
        /// Get the detail data of the Shipment specified by Id number
        /// </summary>
        /// <param name="id">The Shipment Id number</param>
        /// <returns></returns>
        [HttpGet("{id}")]
        [Authorize(Policy = "ApiUser12")]
        public async Task<ActionResult<ShipmentDetailVm>> GetShipmentDetail(int id)
        {
            var vm = await Mediator.Send(new GetShipmentDetailQuery() { ShipmentId = id });
            return vm;
        }

        /// <summary>
        /// Get the list of Claims assigned to the Shipment specified by Id number
        /// </summary>
        /// <param name="id">Id number of the Shipment</param>
        /// <returns></returns>
        [HttpGet("{id}/Claims")]
        [Authorize(Policy = "ApiUser12")]
        public async Task<ActionResult<ShipmentClaimsVm>> GetShipmentClaims(int id)
        {
            var vm = await Mediator.Send(new GetShipmentClaimsQuery() { ShipmentId = id });
            return vm;
        }

        /// <summary>
        /// Get the list of Packagings included in the Shipment specified by Id number
        /// </summary>
        /// <param name="id">Id number of the Shipment</param>
        /// <returns></returns>
        [HttpGet("{id}/Packagings")]
        [Authorize(Policy = "ApiUser12")]
        public async Task<ActionResult<ShipmentPackagingsVm>> GetShipmentPackagings(int id)
        {
            var vm = await Mediator.Send(new GetShipmentPackagingsQuery() { ShipmentId = id });
            return vm;
        }

        /// <summary>
        /// Create the new Shipment
        /// </summary>
        /// <param name="command">The new Shipment data</param>
        /// <returns></returns>
        [HttpPost]
        [Authorize(Policy = "ApiUser2")]
        public async Task<IActionResult> CreateShipment(CreateShipmentCommand command)
        {
            var result = await Mediator.Send(command);
            return Ok(result);
        }

        /// <summary>
        /// Update the Shipment data specified by Id number or create the new if given Id not exists.
        /// </summary>
        /// <param name="command">The Shipment data</param>
        /// <param name="id">The Shipment Id number</param>
        /// <returns></returns>
        [HttpPut("{id}")]
        [Authorize(Policy = "ApiUser2")]
        public async Task<IActionResult> UpdateShipment(UpdateShipmentCommand command, int id)
        {
            command.SetId(id);
            var result = await Mediator.Send(command);
            return Ok(result);
        }

        /// <summary>
        /// Delete the Shipment specified by Id number
        /// </summary>
        /// <param name="id">Id number of the Shipment</param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        [Authorize(Policy = "ApiUser2")]
        public async Task<IActionResult> DeleteShipment(int id)
        {
            var command = new DeleteShipmentCommand { ShipmentId = id };

            await Mediator.Send(command);
            return NoContent();
        }
    }
}
