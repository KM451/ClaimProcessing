using ClaimProcessing.Application.AttachmentUrls.Commands.CreateAttachmentUrl;
using Newtonsoft.Json;
using Shouldly;
using System.Text;
using WebApi.IntegrationTests.Common;
using Xunit;

namespace WebApi.IntegrationTests.Controllers.AttachmentUrls
{
    public class PostAttachmentUrl_Tests(CustomWebApplicationFactory<Program> _factory)
        : IClassFixture<CustomWebApplicationFactory<Program>>
    {
        [Fact]
        public async Task PostGivenAttachmentUrl_ReturnsIdValueEq2() 
        {
            var client = await _factory.GetAuthenticatedClientAsync();

            CreateAttachmentUrlCommand attachment = new CreateAttachmentUrlCommand
            {
                ClaimId = 1,
                Path = "test",
            };

            var jsonValue = JsonConvert.SerializeObject(attachment);
            var content = new StringContent(jsonValue, Encoding.UTF8, "application/json");

            var response = await client.PostAsync($"/api/v1/attachment-urls", content);

            response.EnsureSuccessStatusCode();
            var id = await response.Content.ReadAsStringAsync();
            id.ShouldBe("3");
        }
    }
}
