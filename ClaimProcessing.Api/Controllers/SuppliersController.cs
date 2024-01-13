using ClaimProcessing.Application.Suppliers.Commands.CreateSupplier;
using ClaimProcessing.Application.Suppliers.Commands.DeleteSupplier;
using ClaimProcessing.Application.Suppliers.Commands.UpdateSupplier;
using ClaimProcessing.Application.Suppliers.Queries.GetSupplierCity;
using ClaimProcessing.Application.Suppliers.Queries.GetSupplierClaims;
using ClaimProcessing.Application.Suppliers.Queries.GetSupplierDetail;
using ClaimProcessing.Application.Suppliers.Queries.GetSuppliers;
using ClaimProcessing.Application.Suppliers.Queries.GetSupplierShipments;
using ClaimProcessing.Shared.Suppliers.Queries.GetSuppliers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace ClaimProcessing.Api.Controllers
{
    [Route("api/v1/suppliers")]
    
    public class SuppliersController : BaseController
    {
        /// <summary>
        /// Get the list od Suppliers
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Authorize(Policy = "ApiUser12")]
        public async Task<ActionResult<SuppliersVm>> GetSuppliers()
        {
            var vm = await Mediator.Send(new GetSuppliersQuery());
            return vm;
        }

        /// <summary>
        /// Get data of Supplier specified by Id number
        /// </summary>
        /// <param name="id">Id number of Supplier</param>
        /// <returns></returns>
        [HttpGet("{id}")]
        [Authorize(Policy = "ApiUser12")]
        public async Task<ActionResult<SupplierDetailVm>> GetDetails(int id)
        {
            var vm = await Mediator.Send(new GetSupplierDetailQuery() { SupplierId = id });
            return vm;
        }

        /// <summary>
        /// Get the list of Claims to a supplier selected by ID number, created in the date or date range specified by the filter
        /// </summary>
        /// <param name="id">Id number of Supplier</param>
        /// <param name="filter">The filter should contains one of folowing keys: eq – equals, in – in, nin – not in, neq – not equals,
        /// gt – greater than, gte – greater than or equal, lt – less than, lte – less then or equals and after a space the required date
        /// value in the format 'YYYY.MM.DD' or for keys 'in' and 'nin' two dates separated by a dash sign to specify a date range, 
        /// for ex. "in 2023.01.10-2023.02.20". 
        /// If you leave it blank you get all Claims to Supplier specified by the Id.</param>
        /// <returns></returns>
        [HttpGet("{id}/Claims")]
        [Authorize(Policy = "ApiUser12")]
        public async Task<ActionResult<SupplierClaimsVm>> GetSupplierClaims(int id, string? filter)
        {
            var vm = await Mediator.Send(new GetSupplierClaimsQuery() { SupplierId = id, Filter = filter });
            return vm;
        }

        /// <summary>
        /// Get the list of Shipments to a supplier selected by ID number at the Shipment date or date range specified by the filter
        /// </summary>
        /// <param name="id">Id number of Supplier</param>
        /// <param name="filter">The filter should contains one of folowing keys: eq – equals, in – in, nin – not in, neq – not equals,
        /// gt – greater than, gte – greater than or equal, lt – less than, lte – less then or equals and after a space the required date
        /// value in the format 'YYYY.MM.DD' or for keys 'in' and 'nin' two dates separated by a dash sign to specify a date range, 
        /// for ex. "in 2023.01.10-2023.02.20". 
        /// If you leave it blank you get all Shipments to Supplier specified by the Id.</param>
        /// <returns></returns>
        [HttpGet("{id}/Shipments")]
        [Authorize(Policy = "ApiUser12")]
        public async Task<ActionResult<SupplierShipmentsVm>> GetSupplierShipments(int id, string? filter)
        {
            var vm = await Mediator.Send(new GetSupplierShipmentsQuery() { SupplierId = id, Filter = filter });
            return vm;
        }

        /// <summary>
        /// Get the list of cities assigned to given zip code
        /// </summary>
        /// <param name="kod">Zip Code</param>
        /// <returns></returns>
        [HttpGet("{kod}/City")]
        [Authorize(Policy = "ApiUser12")]
        public async Task<ActionResult<List<string>>> GetCityByZipCode(string kod)
        {
            var response = await Intami.GetCity(kod, new CancellationToken());
            var cities = new List<string>();
            if (response != "Someting bad happened")
            {
                var vm = JsonConvert.DeserializeObject<List<SupplierCityVm>>(response);

                if (vm != null)
                {
                    foreach (var item in vm)
                    {
                        if (!cities.Contains(item.miejscowosc))
                        {
                            cities.Add(item.miejscowosc);
                        }
                    }
                }
                cities.Sort();
            }
            return cities;
        }

        
        /// <summary>
        /// Create the new Supplier
        /// </summary>
        /// <param name="command">The new Supplier data</param>
        /// <returns></returns>
        [HttpPost]
        [Authorize(Policy = "ApiUser2")]
        public async Task<IActionResult> CreateSupplier(CreateSupplierCommand command)
        {
            var result = await Mediator.Send(command);
            return Ok(result);
        }

        /// <summary>
        /// Update the Supplier data specified by Id number or create the new if given Id not exists.
        /// </summary>
        /// <param name="command">The Supplier data</param>
        /// <param name="id">The Supplier Id number</param>
        /// <returns></returns>
        [HttpPut("{id}")]
        [Authorize(Policy = "ApiUser2")]
        public async Task<IActionResult> UpdateSupplier(UpdateSupplierCommand command, int id)
        {
            command.SetId(id);
            var result = await Mediator.Send(command);
            return Ok(result);
        }

        /// <summary>
        /// Delete the Supplier specified by Id number
        /// </summary>
        /// <param name="id">The Supplier Id number</param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        [Authorize(Policy = "ApiUser2")]
        public async Task<IActionResult> DeleteSupplier(int id)
        {
            var command = new DeleteSupplierCommand { SupplierId = id };

            await Mediator.Send(command);
            return NoContent();
        }
    }
}
