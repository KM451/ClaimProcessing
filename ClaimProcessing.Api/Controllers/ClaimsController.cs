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
            new ClaimsForView{Id=1, OwnerType="internal", ClaimType="technical", ItemCode="IC001", Qty=2, SupplierId=1},
            new ClaimsForView{Id=2, OwnerType="external", ClaimType="technical", ItemCode="IC002", Qty=1, SupplierId=2},
            new ClaimsForView{Id=3, OwnerType="internal", ClaimType="logistic", ItemCode="IC003", Qty=20, SupplierId=1},
            new ClaimsForView{Id=4, OwnerType="internal", ClaimType="technical", ItemCode="IC004", Qty=1, SupplierId=1}
        };

        /// <summary>
        /// Get the list of claims to a supplier optionally filtered by specified claim fields.
        /// </summary>
        /// <param name="filter">Claim fields that can be filtered: OwnerType, ClaimType, ItemCode, Customer, ClaimStatus and RmaAvailable
        /// The filter phrase contains the field name, separated by space key: eq – equals or neq – not equals and after a space the desired value.
        /// You can use many filter phrases separated by a comma or you can leave the filter blank to obtain data of all claims</param>
        /// <param name="sort">The parameter allows you to sort data by Claim Id field. Allowed sort phrases 'asc' for ascending and 'desc' for descending sort. </param>
        /// <returns></returns>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult GetClaims(string? filter, string sort = "asc")
        {
            List<ClaimsForView>? result = new List<ClaimsForView>();


            if (filter == null)
            {
                result = sort == "desc" ? claimsForView.OrderByDescending(i => i.Id).ToList() : claimsForView.OrderBy(i => i.Id).ToList();
                return result.Count() > 0 ? Ok(result) : NotFound();
            }

            try
            {
                var phrases = filter.Split(",");
                result = claimsForView;
                for (int i = 0; i < phrases.Length; i++)
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
                            result = result = key == "eq" ? result.Where(i => i.RmaAvailable == (value == "true" ? true : false)).ToList() : result.Where(i => i.RmaAvailable != (value == "true" ? true : false)).ToList();
                            break;
                        default: return BadRequest();
                    }
                }
                result = sort == "desc" ? result.OrderByDescending(i => i.Id).ToList() : result.OrderBy(i => i.Id).ToList();
                return result.Count() > 0 ? Ok(result) : NotFound();
            }
            catch (Exception ex)
            {
                return BadRequest();
            }
        }

        /// <summary>
        /// Get the detail data of the claim with given it ID number
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult GetShipment2(int id)
        {
           
            if (claimsForView.FirstOrDefault(i => i.Id == id) == null)
            {
                return NotFound();
            }
            return Ok(claimsForView.FirstOrDefault(i => i.Id == id));
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

        /// <summary>
        /// Add or modify the serial number of claimed item.
        /// </summary>
        /// <param name="id">Claim Id number</param>
        /// <param name="serialNo">he object with a value of serial number of claimed item.</param>
        /// <returns></returns>
        [HttpPut("{id}/serials")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public ActionResult PutSerialNo(int id, SerialsForView serialNo)
        {
            return NoContent();
        }

        /// <summary>
        /// Add or modify the path to the photo associated with claim.
        /// </summary>
        /// <param name="id">Claim Id number</param>
        /// <param name="fotoUrl">The object with a path to the photo associated with claim.</param>
        /// <returns></returns>
        [HttpPut("{id}/fotoUrl")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public ActionResult PutFotoUrl(int id, FotoUrlsForView fotoUrl)
        {
            return NoContent();
        }

        /// <summary>
        /// Add or modify the path to the file associated with claim.
        /// </summary>
        /// <param name="id">Claim Id number</param>
        /// <param name="attachmentUrl">The object with a path to the file associated with claim.</param>
        /// <returns></returns>
        [HttpPut("{id}/attachmentUrl")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public ActionResult AttachmentUrl(int id, AttachmentUrlsForView attachmentUrl)
        {
            return NoContent();
        }
    }
}



