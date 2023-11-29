using ClaimProcessing.Application.AttachmentUrls.Commands.CreateAttachmentUrl;
using ClaimProcessing.Application.AttachmentUrls.Commands.DeleteAttachmentUrl;
using ClaimProcessing.Application.AttachmentUrls.Queries.GetAttachmentUrl;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ClaimProcessing.Api.Controllers
{
    [Route("api/v1/attachment-urls")]
    
    public class AttachmentUrlsController : BaseController
    {
        /// <summary>
        /// Get the AttachmentUrl path specified by Id number
        /// </summary>
        /// <param name="id">Id of AttachmentUrl</param>
        /// <returns></returns>
        [HttpGet("{id}")]
        [Authorize(Roles = "Admin,Staff1,Staff2")]
        
        public async Task<ActionResult<AttachmentUrlVm>> GetAttachmentUrl(int id)
        {
            var vm = await Mediator.Send(new GetAttachmentUrlQuery() { AttachmentUrlId = id });
            return vm;
        }

        /// <summary>
        /// Add the AttachmentUrl path to the Claim
        /// </summary>
        /// <param name="command">Data of the new AttachmentUrl object</param>
        /// <returns></returns>
        [HttpPost]
        [Authorize(Roles = "Admin,Staff1")]
        public async Task<IActionResult> CreateAttachmentUrl(CreateAttachmentUrlCommand command)
        {
            var result = await Mediator.Send(command);
            return Ok(result);
        }

        /// <summary>
        /// Delete the AttachmentUrl path specified by Id number
        /// </summary>
        /// <param name="id">Id number of AttachmentUrl </param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin,Staff1")]
        public async Task<IActionResult> DeleteAttachmentUrl(int id)
        {
            var command = new DeleteAttachmentUrlCommand { AttachmentUrlId = id };

            await Mediator.Send(command);
            return NoContent();
        }
    }
}
