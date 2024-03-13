using ClaimProcessing.Shared.Packagings.Commands.UpdatePackaging;
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
                PackagingId = 1,
                Depth = 2,
                Width = 2,
                Height = 2,
                Weight = 2,
                Type = "box",
                Notes = "",
                ShipmentId = 1,
            };
            var jsonValue = JsonConvert.SerializeObject(packaging);
            var content = new StringContent(jsonValue, Encoding.UTF8, "application/json");

            var response = await client.PutAsync($"/api/v1/packagings", content);
            response.EnsureSuccessStatusCode();
            var idResponse = await response.Content.ReadAsStringAsync();
            idResponse.ShouldBe(packaging.PackagingId.ToString());
        }

        [Fact]
        public async Task PutGivenNotExistingClaim_ReturnsIdValueOfNewClaim()
        {
            var client = await _factory.GetAuthenticatedClientAsync();

            UpdatePackagingCommand packaging = new()
            {
                PackagingId = 10,
                Depth = 2,
                Width = 2,
                Height = 2,
                Weight = 2,
                Type = "box",
                Notes = "",
                ShipmentId = 1,
            };

            var jsonValue = JsonConvert.SerializeObject(packaging);
            var content = new StringContent(jsonValue, Encoding.UTF8, "application/json");

            var response = await client.PutAsync($"/api/v1/packagings", content);
            response.EnsureSuccessStatusCode();
            var idResponse = await response.Content.ReadAsStringAsync();
            int.Parse(idResponse).ShouldBeGreaterThan(2);
            int.Parse(idResponse).ShouldBeLessThan(packaging.PackagingId);
        }
    }
}