using ClaimProcessing.Application.Suppliers.Queries.GetSuppliers;
using Shouldly;
using WebApi.IntegrationTests.Common;
using Xunit;

namespace WebApi.IntegrationTests.Controllers.Suppliers
{
    public class GetSuppliers_Tests(CustomWebApplicationFactory<Program> _factory)
        : IClassFixture<CustomWebApplicationFactory<Program>>
    {
        [Fact]
        public async Task TestReturnsCollectionOfSuppliers()
        {
            var client = await _factory.GetAuthenticatedClientAsync();

            var response = await client.GetAsync($"/api/v1/suppliers");
            var vm = await Utilities.GetResponseContent<SuppliersVm>(response);
            response.EnsureSuccessStatusCode();
            vm.Suppliers.Count.ShouldBeGreaterThan(0);
        }
    }
}
