using ClaimProcessing.Application.Claims.Commands.CreateClaim;
using ClaimProcessing.Application.Claims.Commands.UpdateClaim;
using ClaimProcessing.Application.Claims.Commands.UpdateClaimStatus;
using ClaimProcessing.Application.Claims.Commands.UpdateRmaAvailability;
using ClaimProcessing.Application.Claims.Commands.UpdateShipmentId;
using ClaimProcessing.Application.Claims.Queries.GetAllClaimsShort;
using ClaimProcessing.Application.Claims.Queries.GetClaimAttachmentsUrls;
using ClaimProcessing.Application.Claims.Queries.GetClaimDetail;
using ClaimProcessing.Application.Claims.Queries.GetClaimFotosUrls;
using ClaimProcessing.Application.Claims.Queries.GetClaimSerialNumbers;
using Microsoft.AspNetCore.Mvc;

namespace ClaimProcessing.Api.Controllers
{

    [Route("api/v1/claims")]

    public class ClaimsController : BaseController
    {
        [HttpGet]
        public async Task<ActionResult<AllClaimsShortVm>> GetClaims()
        {
            var vm = await Mediator.Send(new GetAllClaimsShortQuery());
            return vm;
        }

        /// <summary>
        /// Get the detail data of the claim with given it ID number
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}")]
        public async Task<ActionResult<ClaimDetailVm>> GetDetails(int id)
        {
            var vm = await Mediator.Send(new GetClaimDetailQuery() { ClaimId = id });
            return vm;
        }

        [HttpGet("{id}/AttachmentUrls")]
        public async Task<ActionResult<ClaimAttachmentUrlsVm>> GetAttachmentUrls(int id)
        {
            var vm = await Mediator.Send(new GetClaimAttachmentUrlsQuery() { ClaimId = id });
            return vm;
        }

        [HttpGet("{id}/FotoUrls")]
        public async Task<ActionResult<ClaimFotoUrlsVm>> GetFotoUrls(int id)
        {
            var vm = await Mediator.Send(new GetClaimFotoUrlsQuery() { ClaimId = id });
            return vm;
        }

        [HttpGet("{id}/SerialNumbers")]
        public async Task<ActionResult<ClaimSerialNumbersVm>> GetSerialNumbers(int id)
        {
            var vm = await Mediator.Send(new GetClaimSerialNumbersQuery() { ClaimId = id });
            return vm;
        }

        /// <summary>
        /// Create the new claim case
        /// </summary>
        /// <param name="command"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> CreateClaim(CreateClaimCommand command)
        {
            var result = await Mediator.Send(command);
            return Ok(result);
        }

        [HttpPatch("{id}")]
        public async Task<IActionResult> UpdateClaim(UpdateClaimCommand command, int id)
        {
            command.ClaimId = id;
            await Mediator.Send(command);
            return Ok();
        }

        [HttpPatch("{id}/ClaimStatus")]
        public async Task<IActionResult> UpdateClaimStatus(int id, int claimStatus)
        {
            var command = new UpdateClaimStatusCommand { ClaimId=id, ClaimStatus=claimStatus};
            await Mediator.Send(command);
            return Ok();
        }

        [HttpPatch("{id}/RmaAvailable")]
        public async Task<IActionResult> UpdateRmaAvailability(int id, bool rmaAvailability)
        {
            var command = new UpdateRmaAvailabilityCommand { ClaimId = id, RmaAvailable = rmaAvailability };     
            await Mediator.Send(command);
            return Ok();
        }

        [HttpPatch("{id}/ShipmentId")]
        public async Task<IActionResult> UpdateShipmentId(int id, int shipmentId)
        {
            var command = new UpdateShipmentIdCommand { ClaimId = id, ShipmentId = shipmentId };
            await Mediator.Send(command);
            return Ok();
        }

    }
}



