using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;

namespace ClaimProcessing.Api.Controllers
{
    [Route("api/v1/shipments")]
    [ApiController]
    [EnableCors("MyAllowSpecificOrgins")]
    public class ShipmentsController : ControllerBase
    {
        /// <summary>
        /// Get the list of executed and planned shipments of items (claims) returned to suppliers
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult GetShipments()
        {
             return Ok();
        }

        /// <summary>
        /// Get the list of claim cases shipped to the selected supplier in the seleced date
        /// </summary>
        /// <param name="shippingDate"></param>
        /// <param name="supplierId"></param>
        /// <returns></returns>
        [HttpGet("{shippingDate}/{supplierId}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult GetShipment(DateTime shippingDate, int supplierId)
        {
            return Ok();
        }
    }
}
