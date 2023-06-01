using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;

namespace ClaimProcessing.Api.Controllers
{
    [Route("api/v1/shipments")]
    [ApiController]
    [EnableCors("MyAllowSpecificOrgins")]
    public class ShipmentsController : ControllerBase
    {

        public static List<ShipmentForView> shipmentsForView = new List<ShipmentForView>
        {
            new ShipmentForView{Id=1, ShipmentDate= new DateTime(2023,08,01), SupplierId=1, TotalWeight=0, PackagingDetails=new List<PackagingForView>()},
            new ShipmentForView{Id=2, ShipmentDate= new DateTime(2023,09,01), SupplierId=2, TotalWeight=0, PackagingDetails=new List<PackagingForView>()},
            new ShipmentForView{Id=3, ShipmentDate= new DateTime(2023,10,01), SupplierId=1, TotalWeight=0, PackagingDetails=new List<PackagingForView>()}

        };


        /// <summary>
        /// Get the list of executed and planned shipments of items (claims) returned to suppliers
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult GetShipments()
        {

             return Ok(shipmentsForView);
        }

        /// <summary>
        /// Get the detail data of the shipment with given it ID number
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult GetShipment(int id)
        {
            if (shipmentsForView.FirstOrDefault(i => i.Id == id) == null)
            {
                return NotFound();
            }
            return Ok(shipmentsForView.FirstOrDefault(i => i.Id == id));
        }

        /// <summary>
        /// Create the new shipment
        /// </summary>
        /// <param name="shipmentForView"></param>
        /// <returns></returns>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        public ActionResult PostShipment(ShipmentForView shipmentForView)
        {
            if (shipmentsForView.FirstOrDefault(i => i.Id == shipmentForView.Id) != null)
            {
                return Conflict(shipmentForView);
            }
            shipmentsForView.Add(shipmentForView);
            return Created("api/Claim", shipmentForView);
        }

        /// <summary>
        /// Modify the existing shipment data
        /// </summary>
        /// <param name="shipmentForView"></param>
        /// <returns></returns>
        [HttpPatch("{id}/")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult PatchShipment(int id)
        {
            var shipment = shipmentsForView.FirstOrDefault(i => i.Id == id);
            if (shipment == null)
            {
                return NotFound();
            }

            return Ok(shipment);
        }

        /// <summary>
        /// Add or modify information about packaging of items returned to supplier.
        /// </summary>
        /// <param name="id">Shipment Id number</param>
        /// <param name="packaging">Details of the unit packaging with claims, prepared for shipment.</param>
        /// <returns></returns>
        [HttpPut("{id}/packaging")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public ActionResult PutPackaging(int id, PackagingForView packaging)
        {
            return NoContent();
        }
    }
}
