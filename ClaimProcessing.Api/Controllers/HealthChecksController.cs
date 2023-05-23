using Microsoft.AspNetCore.Mvc;

namespace ClaimProcessing.Api.Controllers
{
    [Route("api/hc")]
    [ApiController]
    public class HealthChecksController : ControllerBase
    {
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
       // [ProducesResponseType(StatusCodes.Status403Forbidden,Type = typeof(ErrorModel))]

        public async Task<ActionResult<string>> GetAsync()
        {
            return "Healthy";
        }
    }
}
