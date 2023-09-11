using Microsoft.AspNetCore.Mvc;

namespace ClaimProcessing.Api.Controllers
{
    [Route("api/hc")]
    
    public class HealthChecksController : BaseController
    {
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]

        public async Task<ActionResult<string>> GetAsync()
        {
            return "Healthy";
        }
    }
}
