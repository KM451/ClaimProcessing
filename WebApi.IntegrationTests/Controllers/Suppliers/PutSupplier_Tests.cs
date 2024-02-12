using ClaimProcessing.Application.Suppliers.Commands.UpdateSupplier;
using ClaimProcessing.Shared.Suppliers.Commands.UpdateSupplier;
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
                SupplierId = 1,
                Name = "Supplier",
                Street = "street",
                City = "city",
                Country = "country",
                ZipCode = "zip",
                ContactPerson = "zulu gula"
            };

            var jsonValue = JsonConvert.SerializeObject(supplier);
            var content = new StringContent(jsonValue, Encoding.UTF8, "application/json");

            var response = await client.PutAsync($"/api/v1/suppliers", content);
            response.EnsureSuccessStatusCode();
            var idResponse = await response.Content.ReadAsStringAsync();
            idResponse.ShouldBe(supplier.SupplierId.ToString());
        }

        [Fact]
        public async Task PutGivenNotExistingSupplier_ReturnsIdValueOfNewSupplier()
        {
            var client = await _factory.GetAuthenticatedClientAsync();

            UpdateSupplierCommand supplier = new()
            {
                SupplierId = 10,
                Name = "Supplier",
                Street = "street",
                City = "city",
                Country = "country",
                ZipCode = "zip",
                ContactPerson = "zulu gula"
            };

            var jsonValue = JsonConvert.SerializeObject(supplier);
            var content = new StringContent(jsonValue, Encoding.UTF8, "application/json");

            var response = await client.PutAsync($"/api/v1/suppliers", content);
            response.EnsureSuccessStatusCode();
            var idResponse = await response.Content.ReadAsStringAsync();
            int.Parse(idResponse).ShouldBeGreaterThan(2);
            int.Parse(idResponse).ShouldBeLessThan(supplier.SupplierId);
        }
    }
}
