using ClaimProcessing.Application.Claims.Commands.CreateClaim;
using ClaimProcessing.Application.Suppliers.Commands.CreateSupplier;
using ClaimProcessing.Domain.ValueObjects;
using Newtonsoft.Json;
using Shouldly;
using System.Net;
using System.Text;
using WebApi.IntegrationTests.Common;
using Xunit;

namespace WebApi.IntegrationTests.Controllers.Suppliers
{
    public class PostSupplier_Tests(CustomWebApplicationFactory<Program> _factory)
        : IClassFixture<CustomWebApplicationFactory<Program>>
    {
        [Fact]
        public async Task PostGivenSupplier_ReturnsIdValue()
        {
            var client = await _factory.GetAuthenticatedClientAsync();

            CreateSupplierCommand supplier = new()
            {
                Name = "name",
                Street = "street",
                City = "city",
                Country = "country",
                ZipCode = "zip",
                ContactPerson = "zulu gula"
            };

            var jsonValue = JsonConvert.SerializeObject(supplier);
            var content = new StringContent(jsonValue, Encoding.UTF8, "application/json");

            var response = await client.PostAsync($"/api/v1/suppliers", content);

            response.EnsureSuccessStatusCode();
            var id = await response.Content.ReadAsStringAsync();
            int.Parse(id).ShouldBeGreaterThan(2);
        }

        [Fact]
        public async Task PostEmptyClaim_Returns400()
        {
            var client = await _factory.GetAuthenticatedClientAsync();

            CreateSupplierCommand supplier = new();

            var jsonValue = JsonConvert.SerializeObject(supplier);
            var content = new StringContent(jsonValue, Encoding.UTF8, "application/json");

            var response = await client.PostAsync($"/api/v1/claims", content);

            response.StatusCode.ShouldBe(HttpStatusCode.BadRequest);

        }
    }
}
