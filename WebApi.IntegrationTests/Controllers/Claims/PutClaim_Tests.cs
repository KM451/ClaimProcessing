using ClaimProcessing.Application.Claims.Commands.UpdateClaim;
using Newtonsoft.Json;
using Shouldly;
using System.Text;
using WebApi.IntegrationTests.Common;
using Xunit;

namespace WebApi.IntegrationTests.Controllers.Claims
{
    public class PutClaim_Tests(CustomWebApplicationFactory<Program> _factory)
        : IClassFixture<CustomWebApplicationFactory<Program>>
    {
        [Fact]
        public async Task PutGivenExistingClaim_ReturnsUpdatedClaimIdValue()
        {
            var client = await _factory.GetAuthenticatedClientAsync();

            UpdateClaimCommand claim = new()
            {
                ClaimNumber = "C10/23",
                OwnerType = "o2",
                ClaimType = "c11",
                ItemCode = "12A34B",
                Qty = 1,
                CustomerName = "Customer",
                ItemName = "item_updated",
                ClaimDescription = "description_updated",
                Remarks = "remarks_updated",
                ClaimStatus = 2,
                RmaAvailable = true,
                SupplierId = 1,
                SaleInvoiceNo = null,
                SaleDate = null,
                PurchaseInvoiceNo = null,
                PurchaseDate = null,
                InternalDocNo = null,
                ShipmentId = 1 
            };

            string id = "1";

            var jsonValue = JsonConvert.SerializeObject(claim);
            var content = new StringContent(jsonValue, Encoding.UTF8, "application/json");

            var response = await client.PutAsync($"/api/v1/claims/{id}", content);
            response.EnsureSuccessStatusCode();
            var idResponse = await response.Content.ReadAsStringAsync();
            idResponse.ShouldBe(id);
        }

        [Fact]
        public async Task PutGivenNotExistingClaim_ReturnsIdValueOfNewClaim()
        {
            var client = await _factory.GetAuthenticatedClientAsync();

            UpdateClaimCommand claim = new()
            {
                ClaimNumber = "C10/23",
                OwnerType = "o1",
                ClaimType = "c1",
                ItemCode = "12A34B",
                Qty = 1,
                CustomerName = "Customer",
                ItemName = "item",
                ClaimDescription = "description",
                Remarks = "remarks",
                ClaimStatus = 2,
                RmaAvailable = true,
                SupplierId = 1,
                SaleInvoiceNo = null,
                SaleDate = null,
                PurchaseInvoiceNo = null,
                PurchaseDate = null,
                InternalDocNo = null,
                ShipmentId = 2
            };

            string id = "10";

            var jsonValue = JsonConvert.SerializeObject(claim);
            var content = new StringContent(jsonValue, Encoding.UTF8, "application/json");

            var response = await client.PutAsync($"/api/v1/claims/{id}", content);
            response.EnsureSuccessStatusCode();
            var idResponse = await response.Content.ReadAsStringAsync();
            int.Parse(idResponse).ShouldBeGreaterThan(2);
            int.Parse(idResponse).ShouldBeLessThan(int.Parse(id));
        }
    }
}
