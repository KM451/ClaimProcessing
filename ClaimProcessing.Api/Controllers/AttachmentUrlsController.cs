using ClaimProcessing.Application.AttachmentUrls.Commands.CreateAttachmentUrl;
using ClaimProcessing.Application.AttachmentUrls.Queries.GetAttachmentUrl;
using Microsoft.AspNetCore.Mvc;

namespace ClaimProcessing.Api.Controllers
{
    [Route("api/v1/attachment-urls")]
    public class AttachmentUrlsController : BaseController
    {
        [HttpGet("{id}")]
        public async Task<ActionResult<AttachmentUrlVm>> GetAttachmentUrl(int id)
        {
            var vm = await Mediator.Send(new GetAttachmentUrlQuery() { AttachmentUrlId = id });
            return vm;
        }

        [HttpPost]
        public async Task<IActionResult> CreateAttachmentUrl(CreateAttachmentUrlCommand command)
        {
            var result = await Mediator.Send(command);
            return Ok(result);
        }
    }
}
