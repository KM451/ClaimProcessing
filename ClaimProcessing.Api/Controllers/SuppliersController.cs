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

            new SuppliersForView{Id=1, Name="Supplier1",Address="Street 1, 11-111 City1, Country1", ContactPerson="Contact1 Person1"},
            new SuppliersForView{Id=2, Name="Supplier2",Address="Street 2, 22-222 City2, Country2", ContactPerson="Contact2 Person2"},


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
        /// <param name="id">The supplier Id number</param>
        /// <returns></returns>
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
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
        /// Get the detail data of the shipments to a supplier selected by ID number at the date or date range specified by the filter
        /// </summary>
        /// <param name="id">The supplier Id number</param>
        /// <param name="filter">The filter should contains one of folowing keys: eq – equals, in – in, nin – not in, neq – not equals,
        /// gt – greater than, gte – greater than or equal, lt – less than, lte – less then or equals and after a space the required date
        /// value in the format 'YYYY.MM.DD' or two dates separated by a dash sign to specify a date range, for ex. "in 2023.01.10-2023.02.20". 
        /// If you leave it blank you get all shipments to supplier specified by the Id.</param>
        /// <returns></returns>
        [HttpGet("{id}/shipment")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult GetShipmentsByDate(int id, string? filter)
        {
            DateTime date1 = new DateTime();
            DateTime date2 = new DateTime();
            List<ShipmentForView>? result = new List<ShipmentForView>();

            if (ShipmentsController.shipmentsForView.FirstOrDefault(i => i.SupplierId == id) == null)
            {
                return NotFound();
            }
            if (filter == null)
            {
                result = ShipmentsController.shipmentsForView.Where(i => i.SupplierId == id).ToList();
                return result.Count() > 0 ? Ok(result) : NotFound();
            }

            try
            {
                var key = filter.Split(" ")[0].ToString();
                var datePart = filter.Split(" ")[1].ToString();
                if (key == "in" || key=="nin")
                {
                    var splitDate = datePart.Split("-");
                    if (!DateTime.TryParse(splitDate[0].ToString(), out date1) || !DateTime.TryParse(splitDate[1].ToString(), out date2))
                    {
                        return BadRequest();
                    }
                }
                else
                {
                    if (!DateTime.TryParse(datePart, out date1))
                    {
                        return BadRequest();
                    }
                }
                
                switch (key)
                {
                    case "eq":  result = ShipmentsController.shipmentsForView.Where(i => i.ShipmentDate == date1 && i.SupplierId == id).ToList();
                                break;
                    case "in":  result = ShipmentsController.shipmentsForView.Where(i => i.ShipmentDate >= date1 && i.ShipmentDate <= date2 && i.SupplierId == id).ToList();
                                break;
                    case "nin": result = ShipmentsController.shipmentsForView.Where(i => i.ShipmentDate < date1 || i.ShipmentDate > date2 && i.SupplierId == id).ToList();
                                break;
                    case "neq": result = ShipmentsController.shipmentsForView.Where(i => i.ShipmentDate != date1 && i.SupplierId == id).ToList();
                                break;
                    case "gt":  result = ShipmentsController.shipmentsForView.Where(i => i.ShipmentDate > date1 && i.SupplierId == id).ToList();
                                break;
                    case "gte": result = ShipmentsController.shipmentsForView.Where(i => i.ShipmentDate >= date1 && i.SupplierId == id).ToList();
                                break;
                    case "lt":  result = ShipmentsController.shipmentsForView.Where(i => i.ShipmentDate < date1 && i.SupplierId == id).ToList();
                                break;
                    case "lte": result = ShipmentsController.shipmentsForView.Where(i => i.ShipmentDate <= date1 && i.SupplierId == id).ToList();
                                break;
                    default: return BadRequest();
                }
                return result.Count() > 0 ? Ok(result) : NotFound();
            }
            catch (IndexOutOfRangeException)
            {
                return BadRequest();
            }

        }

        /// <summary>
        /// Get the detail data of the claims to a supplier selected by ID number and filtered by specified claim fields.
        /// </summary>
        /// <param name="id">The supplier Id number</param>
        /// <param name="filter">Claim fields that can be filtered: OwnerType, ClaimType, ItemCode, Customer, ClaimStatus and RmaAvailable
        /// The filter phrase contains the field name, separated by space key: eq – equals or neq – not equals and after a space the desired value.
        /// You can use many filter phrases separated by a comma. If you leave it blank you get all shipments to supplier specified by the Id. </param>
        /// <param name="sort">The parameter allows you to sort data by Claim Id field. Allowed sort phrases 'asc' for ascending and 'desc' for descending sort. </param>
        /// <returns></returns>
        [HttpGet("{id}/claim")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult GetClaimsByFilter(int id, string? filter, string sort="asc")
        {
            List<ClaimsForView>? result = new List<ClaimsForView>();

            if (ClaimsController.claimsForView.FirstOrDefault(i => i.SupplierId == id) == null)
            {
                return NotFound();
            }
            if (filter == null)
            {
                result = ClaimsController.claimsForView.Where(i => i.SupplierId == id).ToList();
                result = sort == "desc" ? result.OrderByDescending(i => i.Id).ToList() : result.OrderBy(i => i.Id).ToList();
                return result.Count() > 0 ? Ok(result) : NotFound();
            }

            try
            {
                var phrases = filter.Split(",");
                result = ClaimsController.claimsForView.Where(i => i.SupplierId == id).ToList();
                for (int i = 0;  i < phrases.Length; i++)
                {
                    var field = phrases[i].Split(" ")[0].ToString();
                    var key = phrases[i].Split(" ")[1].ToString();
                    var value = phrases[i].Split(" ")[2].ToString();
                    

                    switch (field)
                    {
                        case "OwnerType":
                            result = key == "eq" ? result.Where(i => i.OwnerType == value).ToList() : result.Where(i => i.OwnerType != value).ToList();
                            break;
                        case "ClaimType":
                            result = key == "eq" ? result.Where(i => i.ClaimType == value).ToList() : result.Where(i => i.ClaimType != value).ToList();
                            break;
                        case "ItemCode":
                            result = result = key == "eq" ? result.Where(i => i.ItemCode == value).ToList() : result.Where(i => i.ItemCode != value).ToList();
                            break;
                        case "Customer":
                            result = result = key == "eq" ? result.Where(i => i.Customer == value).ToList() : result.Where(i => i.Customer != value).ToList();
                            break;
                        case "ClaimStatus":
                            result = result = key == "eq" ? result.Where(i => i.ClaimStatus == value).ToList() : result.Where(i => i.ClaimStatus != value).ToList();
                            break;
                        case "RmaAvailable":
                            result = result = key == "eq" ? result.Where(i => i.RmaAvailable == (value =="true"?true:false)).ToList() : result.Where(i => i.RmaAvailable != (value == "true" ? true : false)).ToList();
                            break;
                        default: return BadRequest();
                    }
                }
                result = sort == "desc" ? result.OrderByDescending(i => i.Id).ToList() : result.OrderBy(i => i.Id).ToList();
                return result.Count() > 0 ? Ok(result) : NotFound();
            }
            catch (IndexOutOfRangeException)
            {
                return BadRequest();
            }
        }

        /// <summary>
        /// Add the new supplier
        /// </summary>
        /// <param name="supplierForView">The supplier data</param>
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
        /// <param name="id">The supplier Id number</param>
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
