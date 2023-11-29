using ClaimProcessing.Application.Common.Interfaces;

namespace WebApi.IntegrationTests.Common.DummyServices
{
    public class DummyCurrentUserService : ICurrentUserService
    {
        public string Email { get; set; } = "AliceSmith@email.com";
        public string Name { get; set; } = "Alice Smith";
        public string UserId { get; set; } = "4B434A88-212D-4A4D-A17C-F35102D73CBB";
        public bool IsAuthenticated { get; set; } = true;
    }
}
