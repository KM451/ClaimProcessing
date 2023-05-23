using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;

namespace ClaimProcessing.Api.Controllers
{
    [Route("api/v1/suppliers")]
    [ApiController]
    [EnableCors("MyAllowSpecificOrgins")]
    public class SuppliersController : ControllerBase
    {
        private static List<SuppliersForView> suppliersForView = new List<SuppliersForView>
        {
            new SuppliersForView(),
            new SuppliersForView()
            
        };

        /// <summary>
        /// Get the detailed list of suppliers
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult GetSuppliers()
        {
            return Ok(suppliersForView);
        }

        /// <summary>
        /// Get data of supplier specified by Id number
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult GetSupplier(int id)
        {
            if (suppliersForView.FirstOrDefault(i => i.Id == id) == null)
            {
                return NotFound();
            }
            return Ok(suppliersForView.FirstOrDefault(i => i.Id == id));
        }

        /// <summary>
        /// Add the new supplier
        /// </summary>
        /// <param name="supplierForView"></param>
        /// <returns></returns>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public ActionResult PostSupplier(SuppliersForView supplierForView)
        {
            suppliersForView.Add(supplierForView);
            return Ok(supplierForView);
        }

        /// <summary>
        /// Modify data of existing supplier
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpPatch("{id}/")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public ActionResult PatchSupplier(int id)
        {
            var supplier = suppliersForView.FirstOrDefault(i => i.Id == id);
            if (supplier == null)
            {
                return NotFound();
            }

            return Ok(supplier);
        }
    }
}
