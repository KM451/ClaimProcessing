using ClaimProcessing.Application.AttachmentUrls.Commands.CreateAttachmentUrl;
using ClaimProcessing.Application.Packagings.Commands.CreatePackaging;
using Newtonsoft.Json;
using Shouldly;
using System.Text;
using WebApi.IntegrationTests.Common;
using Xunit;

namespace WebApi.IntegrationTests.Controllers.Packagings
{
    public class PostPackaging_Tests(CustomWebApplicationFactory<Program> _factory)
        : IClassFixture<CustomWebApplicationFactory<Program>>
    {
        [Fact]
        public async Task PostGivenPackaging_ReturnsIdValue()
        {
            var client = await _factory.GetAuthenticatedClientAsync();

            CreatePackagingCommand packaging = new()
            {
                Depth = 1,
                Width = 1,
                Height = 1,
                Weight = 1,
                Type = "box",
                Notes = "",
                ShipmentId = 1,
            };

            var jsonValue = JsonConvert.SerializeObject(packaging);
            var content = new StringContent(jsonValue, Encoding.UTF8, "application/json");

            var response = await client.PostAsync($"/api/v1/packagings", content);

            response.EnsureSuccessStatusCode();
            var id = await response.Content.ReadAsStringAsync();
            id.ShouldBeGreaterThan("2");
        }
    }
}
