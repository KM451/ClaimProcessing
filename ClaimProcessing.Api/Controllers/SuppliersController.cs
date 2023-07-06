using ClaimProcessing.Application.Suppliers.Commands.CreateSupplier;
using ClaimProcessing.Application.Suppliers.Commands.DeleteSupplier;
using ClaimProcessing.Application.Suppliers.Commands.UpdateSupplier;
using ClaimProcessing.Application.Suppliers.Queries.GetSupplierClaims;
using ClaimProcessing.Application.Suppliers.Queries.GetSupplierDetail;
using ClaimProcessing.Application.Suppliers.Queries.GetSuppliers;
using ClaimProcessing.Application.Suppliers.Queries.GetSupplierShipments;
using Microsoft.AspNetCore.Mvc;

namespace ClaimProcessing.Api.Controllers
{
    [Route("api/v1/suppliers")]
    public class SuppliersController : BaseController
    {
        [HttpGet]
        public async Task<ActionResult<SuppliersVm>> GetSuppliers()
        {
            var vm = await Mediator.Send(new GetSuppliersQuery());
            return vm;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<SupplierDetailVm>> GetDetails(int id)
        {
            var vm = await Mediator.Send(new GetSupplierDetailQuery() { SupplierId = id });
            return vm;
        }

        [HttpGet("{id}/Claims")]
        public async Task<ActionResult<SupplierClaimsVm>> GetSupplierClaims(int id)
        {
            var vm = await Mediator.Send(new GetSupplierClaimsQuery() { SupplierId = id });
            return vm;
        }

        [HttpGet("{id}/Shipments")]
        public async Task<ActionResult<SupplierShipmentsVm>> GetSupplierShipments(int id)
        {
            var vm = await Mediator.Send(new GetSupplierShipmentsQuery() { SupplierId = id });
            return vm;
        }

        [HttpPost]
        public async Task<IActionResult> CreateSupplier(CreateSupplierCommand command)
        {
            var result = await Mediator.Send(command);
            return Ok(result);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateSupplier(UpdateSupplierVm vm, int id)
        {
            var command = new UpdateSupplierCommand();
            command = (UpdateSupplierCommand)vm;
            command.SupplierId = id;
            var result = await Mediator.Send(command);
            return Ok(result);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteSupplier(int id)
        {
            var command = new DeleteSupplierCommand { SupplierId = id };

            await Mediator.Send(command);
            return NoContent();
        }
    }
}
