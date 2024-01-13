using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Data;

namespace ClaimProcessing.Api.Controllers
{
    [Route("api/hc")]
    [Authorize]
    public class HealthChecksController : BaseController
    {
        [HttpGet]

        public async Task<ActionResult<string>> GetAsync()
        {
            return "Healthy";
        }
    }
}
