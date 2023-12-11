using ClaimProcessing.Application.Claims.Commands.UpdateClaim;
using ClaimProcessing.Application.Packagings.Commands.UpdatePackaging;
using Newtonsoft.Json;
using Shouldly;
using System.Text;
using WebApi.IntegrationTests.Common;
using Xunit;

namespace WebApi.IntegrationTests.Controllers.Packagings
{
    public class PutPackaging_Tests(CustomWebApplicationFactory<Program> _factory)
        : IClassFixture<CustomWebApplicationFactory<Program>>
    {
        [Fact]
        public async Task PutGivenPackaging_ReturnsUpdatedPackagingIdValue()
        {
            var client = await _factory.GetAuthenticatedClientAsync();

            UpdatePackagingCommand packaging = new()
            {
                Depth = 2,
                Width = 2,
                Height = 2,
                Weight = 2,
                Type = "box",
                Notes = "",
                ShipmentId = 1,
            };

            var id = 1;
            packaging.SetId(id);
            var jsonValue = JsonConvert.SerializeObject(packaging);
            var content = new StringContent(jsonValue, Encoding.UTF8, "application/json");

            var response = await client.PutAsync($"/api/v1/packagings/{id}", content);
            response.EnsureSuccessStatusCode();
            var idResponse = await response.Content.ReadAsStringAsync();
            idResponse.ShouldBe(id.ToString());
        }

        [Fact]
        public async Task PutGivenNotExistingClaim_ReturnsIdValueOfNewClaim()
        {
            var client = await _factory.GetAuthenticatedClientAsync();

            UpdatePackagingCommand packaging = new()
            {
                Depth = 2,
                Width = 2,
                Height = 2,
                Weight = 2,
                Type = "box",
                Notes = "",
                ShipmentId = 1,
            };

            var id = 10;
            packaging.SetId(id);
            var jsonValue = JsonConvert.SerializeObject(packaging);
            var content = new StringContent(jsonValue, Encoding.UTF8, "application/json");

            var response = await client.PutAsync($"/api/v1/packagings/{id}", content);
            response.EnsureSuccessStatusCode();
            var idResponse = await response.Content.ReadAsStringAsync();
            int.Parse(idResponse).ShouldBeGreaterThan(2);
            int.Parse(idResponse).ShouldBeLessThan(id);
        }
    }
}