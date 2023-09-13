using ClaimProcessing.Application.Claims.Commands.CreateClaim;
using ClaimProcessing.Application.Claims.Commands.DeleteClaim;
using ClaimProcessing.Application.Claims.Commands.UpdateClaim;
using ClaimProcessing.Application.Claims.Commands.UpdateClaimStatus;
using ClaimProcessing.Application.Claims.Commands.UpdateRmaAvailability;
using ClaimProcessing.Application.Claims.Commands.UpdateShipmentId;
using ClaimProcessing.Application.Claims.Queries.GetAllClaimsShort;
using ClaimProcessing.Application.Claims.Queries.GetClaimAttachmentsUrls;
using ClaimProcessing.Application.Claims.Queries.GetClaimDetail;
using ClaimProcessing.Application.Claims.Queries.GetClaimFotosUrls;
using ClaimProcessing.Application.Claims.Queries.GetClaimSerialNumbers;
using ClaimProcessing.Application.Claims.Queries.GetClaimsUser;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ClaimProcessing.Api.Controllers
{

    [Route("api/v1/claims")]
    
    public class ClaimsController : BaseController
    {
         

        /// <summary>
        /// Get the list of claims optionally filtered by specified claim fields.
        /// </summary>
        /// <param name="filter">Claim can be filtered by: OwnerType, ClaimType, ItemCode, CustomerName, ClaimStatus, RmaAvailable and SupplierId.
        /// The filter phrase contains the field name, separated by space key: eq – equals or neq – not equals and after a space the desired value.
        /// You can use many filter phrases separated by a comma or you can leave the filter blank to obtain data of all claims</param>
        /// <param name="sort">The parameter allows you to sort data by Claim Id field. Allowed sort phrases 'asc' for ascending and 'desc' for descending sort.</param>
        /// <returns></returns>
        [HttpGet]
        [Authorize(Roles = "Admin,Staff1,Staff2")]
        public async Task<ActionResult<AllClaimsShortVm>> GetClaims(string? filter, string sort = "asc")
        {
            var vm = await Mediator.Send(new GetAllClaimsShortQuery() { Filter = filter, Sort = sort});
            return vm;
        }

        /// <summary>
        /// Get the current user list of claims optionally filtered by specified claim fields.
        /// </summary>
        /// <param name="filter">Claim can be filtered by: ClaimNumber, ItemCode, ClaimStatus.
        /// The filter phrase contains the field name, separated by space key: eq – equals or neq – not equals and after a space the desired value.
        /// You can use many filter phrases separated by a comma or you can leave the filter blank to obtain data of all claims</param>
        /// <param name="sort">The parameter allows you to sort data by Claim creation date field. Allowed sort phrases 'asc' for ascending and 'desc' for descending sort.</param>
        /// <returns></returns>
        [HttpGet("user")]
        [Authorize(Roles = "Admin,Staff1,Staff2,Guest")]
        public async Task<ActionResult<ClaimsUserVm>> GetUserClaims(string? filter, string sort = "asc")
        {
            var vm = await Mediator.Send(new GetClaimsUserQuery() { Filter = filter, Sort = sort });
            return vm;
        }

        /// <summary>
        /// Get the detail data of the Claim specified by Id number
        /// </summary>
        /// <param name="id">The Claim Id number</param>
        /// <returns></returns>
        [HttpGet("{id}")]
        [Authorize(Roles = "Admin,Staff1,Staff2")]
        public async Task<ActionResult<ClaimDetailVm>> GetDetails(int id)
        {
            var vm = await Mediator.Send(new GetClaimDetailQuery() { ClaimId = id });
            return vm;
        }

        /// <summary>
        /// Get the list of AttachmentUrl paths assigned to the claim with given Id number
        /// </summary>
        /// <param name="id">The Claim Id number</param>
        /// <returns></returns>
        [HttpGet("{id}/AttachmentUrls")]
        [Authorize(Roles = "Admin,Staff1,Staff2")]
        public async Task<ActionResult<ClaimAttachmentUrlsVm>> GetAttachmentUrls(int id)
        {
            var vm = await Mediator.Send(new GetClaimAttachmentUrlsQuery() { ClaimId = id });
            return vm;
        }

        /// <summary>
        /// Get the list of FotoUrl paths assigned to the claim with given Id number
        /// </summary>
        /// <param name="id">The Claim Id number</param>
        /// <returns></returns>
        [HttpGet("{id}/FotoUrls")]
        [Authorize(Roles = "Admin,Staff1,Staff2")]
        public async Task<ActionResult<ClaimFotoUrlsVm>> GetFotoUrls(int id)
        {
            var vm = await Mediator.Send(new GetClaimFotoUrlsQuery() { ClaimId = id });
            return vm;
        }

        /// <summary>
        /// Get the list of SerialNumbers assigned to the claim with given Id number
        /// </summary>
        /// <param name="id">The Claim Id number</param>
        /// <returns></returns>
        [HttpGet("{id}/SerialNumbers")]
        [Authorize(Roles = "Admin,Staff1,Staff2")]
        public async Task<ActionResult<ClaimSerialNumbersVm>> GetSerialNumbers(int id)
        {
            var vm = await Mediator.Send(new GetClaimSerialNumbersQuery() { ClaimId = id });
            return vm;
        }

        /// <summary>
        /// Create the new Claim
        /// </summary>
        /// <param name="command">The new Claim data</param>
        /// <returns></returns>
        [HttpPost]
        [Authorize(Roles = "Admin,Staff1")]
        public async Task<IActionResult> CreateClaim(CreateClaimCommand command)
        {
            var result = await Mediator.Send(command);
            return Ok(result);
        }

        /// <summary>
        /// Update the Claim data specified by Id number or create the new if given Id not exists.
        /// </summary>
        /// <param name="command">The claim data</param>
        /// <param name="id">The Claim Id number</param>
        /// <returns></returns>
        [HttpPut("{id}")]
        [Authorize(Roles = "Admin,Staff1")]
        public async Task<IActionResult> UpdateClaim(UpdateClaimCommand command, int id)
        {
            command.SetId(id);
            var result = await Mediator.Send(command);
            return Ok(result);
        }

        /// <summary>
        /// Update the ClaimStatus field of Claim specified by Id number
        /// </summary>
        /// <param name="id">The Claim Id number</param>
        /// <param name="claimStatus">The ClaimStatus value</param>
        /// <returns></returns>
        [HttpPatch("{id}/ClaimStatus")]
        [Authorize(Roles = "Admin,Staff1")]
        public async Task<IActionResult> UpdateClaimStatus(int id, int claimStatus)
        {
            var command = new UpdateClaimStatusCommand { ClaimId=id, ClaimStatus=claimStatus};
            await Mediator.Send(command);
            return Ok();
        }

        /// <summary>
        /// Update the RmaAvailable field of Claim specified by Id number
        /// </summary>
        /// <param name="id">The Claim Id number</param>
        /// <param name="rmaAvailability">The RmaAvailable state</param>
        /// <returns></returns>
        [HttpPatch("{id}/RmaAvailable")]
        [Authorize(Roles = "Admin,Staff1")]
        public async Task<IActionResult> UpdateRmaAvailability(int id, bool rmaAvailability)
        {
            var command = new UpdateRmaAvailabilityCommand { ClaimId = id, RmaAvailable = rmaAvailability };     
            await Mediator.Send(command);
            return Ok();
        }

        /// <summary>
        /// Update the ShipmentId field of Claim specified by Id number
        /// </summary>
        /// <param name="id">The Claim Id number</param>
        /// <param name="shipmentId">The Shipment Id number</param>
        /// <returns></returns>
        [HttpPatch("{id}/ShipmentId")]
        [Authorize(Roles = "Admin,Staff1")]
        public async Task<IActionResult> UpdateShipmentId(int id, int shipmentId)
        {
            var command = new UpdateShipmentIdCommand { ClaimId = id, ShipmentId = shipmentId };
            await Mediator.Send(command);
            return Ok();
        }

        /// <summary>
        /// Delete the Claim specified by Id number
        /// </summary>
        /// <param name="id">The Claim Id number</param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin,Staff1")]
        public async Task<IActionResult> DeleteClaim(int id)
        {
            var command = new DeleteClaimCommand { ClaimId = id };
            await Mediator.Send(command);
            return NoContent();
        }

    }
}



