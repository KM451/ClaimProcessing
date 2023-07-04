using ClaimProcessing.Application.Claims.Commands.CreateClaim;
using ClaimProcessing.Application.Claims.Commands.UpdateClaim;
using ClaimProcessing.Application.Claims.Commands.UpdateShipmentId;
using ClaimProcessing.Application.Claims.Queries.GetAllClaimsShort;
using ClaimProcessing.Application.Claims.Queries.GetClaimAttachmentsUrls;
using ClaimProcessing.Application.Claims.Queries.GetClaimDetail;
using ClaimProcessing.Application.Shipments.Commands.CreateShipment;
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

        [HttpPatch("{id}")]
        public async Task<IActionResult> UpdateShipment(UpdateShipmentCommand command, int id)
        {
            command.ShipmentId = id;
            await Mediator.Send(command);
            return Ok();
        }
    }
}
