using ClaimProcessing.Application.Suppliers.Queries.GetSupplierDetail;
using Shouldly;
using System.Net;
using WebApi.IntegrationTests.Common;
using Xunit;

namespace WebApi.IntegrationTests.Controllers.Suppliers
{
    public class GetSupplierDetail_Tests(CustomWebApplicationFactory<Program> _factory)
        : IClassFixture<CustomWebApplicationFactory<Program>>
    {

        [Fact]
        public async Task GivenSupplierId_ReturnsSupplierDetail()
        {
            var client = await _factory.GetAuthenticatedClientAsync();

            string id = "1";
            var response = await client.GetAsync($"/api/v1/suppliers/{id}");
            var vm = await Utilities.GetResponseContent<SupplierDetailVm>(response);
            response.EnsureSuccessStatusCode();
            vm.Name.ShouldBe("Supplier");
        }

        [Fact]
        public async Task GivenWrongSupplierId_Returns204()
        {
            var client = await _factory.GetAuthenticatedClientAsync();

            string id = "10";
            var response = await client.GetAsync($"/api/v1/suppliers/{id}");
            var vm = await Utilities.GetResponseContent<SupplierDetailVm>(response);
            response.EnsureSuccessStatusCode();
            response.StatusCode.ShouldBe(HttpStatusCode.NoContent);
        }
    }
}
