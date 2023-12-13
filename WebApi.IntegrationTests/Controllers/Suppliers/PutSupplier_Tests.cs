using ClaimProcessing.Application.Suppliers.Commands.UpdateSupplier;
using Newtonsoft.Json;
using Shouldly;
using System.Text;
using WebApi.IntegrationTests.Common;
using Xunit;

namespace WebApi.IntegrationTests.Controllers.Suppliers
{
    public class PutSupplier_Tests(CustomWebApplicationFactory<Program> _factory)
        : IClassFixture<CustomWebApplicationFactory<Program>>
    {
        [Fact]
        public async Task PutGivenExistingSupplier_ReturnsUpdatedSupplierIdValue()
        {
            var client = await _factory.GetAuthenticatedClientAsync();

            UpdateSupplierCommand supplier = new()
            {
                Name = "Supplier",
                Street = "street",
                City = "city",
                Country = "country",
                ZipCode = "zip",
                ContactPerson = "zulu gula"
            };

            string id = "1";

            var jsonValue = JsonConvert.SerializeObject(supplier);
            var content = new StringContent(jsonValue, Encoding.UTF8, "application/json");

            var response = await client.PutAsync($"/api/v1/suppliers/{id}", content);
            response.EnsureSuccessStatusCode();
            var idResponse = await response.Content.ReadAsStringAsync();
            idResponse.ShouldBe(id);
        }

        [Fact]
        public async Task PutGivenNotExistingSupplier_ReturnsIdValueOfNewSupplier()
        {
            var client = await _factory.GetAuthenticatedClientAsync();

            UpdateSupplierCommand supplier = new()
            {
                Name = "Supplier",
                Street = "street",
                City = "city",
                Country = "country",
                ZipCode = "zip",
                ContactPerson = "zulu gula"
            };

            string id = "10";

            var jsonValue = JsonConvert.SerializeObject(supplier);
            var content = new StringContent(jsonValue, Encoding.UTF8, "application/json");

            var response = await client.PutAsync($"/api/v1/suppliers/{id}", content);
            response.EnsureSuccessStatusCode();
            var idResponse = await response.Content.ReadAsStringAsync();
            int.Parse(idResponse).ShouldBeGreaterThan(2);
            int.Parse(idResponse).ShouldBeLessThan(int.Parse(id));
        }
    }
}
