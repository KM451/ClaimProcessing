using Shouldly;
using WebApi.IntegrationTests.Common;
using Xunit;

namespace WebApi.IntegrationTests.Controllers.Suppliers
{
    public class GetSupplierCities_Tests(CustomWebApplicationFactory<Program> _factory)
        : IClassFixture<CustomWebApplicationFactory<Program>>
    {
        [Fact]
        public async Task TestReturnsCollectionOfCities()
        {
            var client = await _factory.GetAuthenticatedClientAsync();
            var zip = "87-100";
            var response = await client.GetAsync($"/api/v1/suppliers/{zip}/City");
            var vm = await Utilities.GetResponseContent<List<string>>(response);
            response.EnsureSuccessStatusCode();
            vm.Count.ShouldBeGreaterThan(0);
        }

        [Fact]
        public async Task TestReturnsEptyCollectionOfCities()
        {
            var client = await _factory.GetAuthenticatedClientAsync();
            var zip = "87-000";
            var response = await client.GetAsync($"/api/v1/suppliers/{zip}/City");
            var vm = await Utilities.GetResponseContent<List<string>>(response);
            response.EnsureSuccessStatusCode();
            vm.Count.ShouldBe(0);
        }
    }
}