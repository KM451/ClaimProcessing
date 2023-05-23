using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;

namespace ClaimProcessing.Api.Controllers
{

    [Route("api/v1/claims")]
    [ApiController]
    [EnableCors("MyAllowSpecificOrgins")]
    public class ClaimsController : ControllerBase
    {
        
        public static List<ClaimsForView> claimsForView = new List<ClaimsForView>
        {
            new ClaimsForView{Id=1, OwnerType="internal", ClaimType="technical", ItemCode="IC001", Qty=2, SupplierId=1, Serials = new List<string>{"123456", "789101"} },
            new ClaimsForView{Id=2, OwnerType="external", ClaimType="technical", ItemCode="IC002", Qty=1, SupplierId=2},
            new ClaimsForView{Id=3, OwnerType="internal", ClaimType="logistic", ItemCode="IC003", Qty=20, SupplierId = 1},
            new ClaimsForView{Id=4, OwnerType="internal", ClaimType="technical", ItemCode="IC004", Qty=1, SupplierId = 1}
        };


        /// <summary>
        /// Get the detailed list of existing claim cases
        /// </summary>
        /// <returns>List of cases</returns>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<IEnumerable<ClaimsForView>> GetClaims()
        {
            return Ok(claimsForView);
        }

        /// <summary>
        /// Get the detail data of the claim case with given ID number
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult GetClaim(int id)
        {
            if(claimsForView.FirstOrDefault(i => i.Id == id) == null)
            {
                return NotFound();
            }
            return Ok(claimsForView.FirstOrDefault(i => i.Id==id)); 
        }

        /// <summary>
        /// Create the new claim case
        /// </summary>
        /// <param name="claimForView"></param>
        /// <returns></returns>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        public ActionResult PostClaim(ClaimsForView claimForView)
        {
            if (claimsForView.FirstOrDefault(i => i.Id == claimForView.Id) != null)
            {
                return Conflict(claimForView);
            }
            claimsForView.Add(claimForView);
            return Created("api/Claim",claimForView);
        }

        /// <summary>
        /// Modify the existing claim case
        /// </summary>
        /// <param name="claimForView"></param>
        /// <returns></returns>
        [HttpPatch("{id}/")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult PatchClaim(int id)
        {
            var claim = claimsForView.FirstOrDefault(i => i.Id == id);
            if (claim == null)
            {
                return NotFound();
            }

            return Ok(claim);
        }

    }
}



