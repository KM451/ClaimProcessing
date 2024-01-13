using ClaimProcessing.Shared.Claims.Commands.CreateClaim;
using Newtonsoft.Json;
using Shouldly;
using System.Net;
using System.Text;
using WebApi.IntegrationTests.Common;
using Xunit;

namespace WebApi.IntegrationTests.Controllers.Claims
{
    public class PostClaim_Tests(CustomWebApplicationFactory<Program> _factory)
        : IClassFixture<CustomWebApplicationFactory<Program>>
    {
        [Fact]
        public async Task PostGivenClaim_ReturnsIdValue()
        {
            var client = await _factory.GetAuthenticatedClientAsync();

            CreateClaimCommand claim = new()
            {
                ClaimNumber = "C22/23",
                OwnerType = "o3",
                ClaimType = "c3",
                ItemCode = "00000",
                Qty = 1,
                CustomerName = "Customer",
                ItemName = "item",
                ClaimDescription = "description",
                Remarks = null,
                ClaimStatus = 2,
                SupplierId = 1,
                SaleInvoiceNo = "S123",
                SaleDate = DateTime.Now,
                PurchaseInvoiceNo = "P123",
                PurchaseDate = DateTime.Now,
                InternalDocNo = "I123"
            };

            var jsonValue = JsonConvert.SerializeObject(claim);
            var content = new StringContent(jsonValue, Encoding.UTF8, "application/json");

            var response = await client.PostAsync($"/api/v1/claims", content);

            response.EnsureSuccessStatusCode();
            var id = await response.Content.ReadAsStringAsync();
            int.Parse(id).ShouldBeGreaterThan(2);
        }

        [Fact]
        public async Task PostEmptyClaim_Returns400()
        {
            var client = await _factory.GetAuthenticatedClientAsync();

            CreateClaimCommand claim = new();
            
            var jsonValue = JsonConvert.SerializeObject(claim);
            var content = new StringContent(jsonValue, Encoding.UTF8, "application/json");

            var response = await client.PostAsync($"/api/v1/claims", content);

            response.StatusCode.ShouldBe(HttpStatusCode.BadRequest);
            
        }
    }
}
