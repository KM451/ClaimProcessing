using ClaimProcessing.Application.Common.Interfaces;

namespace WebApi.IntegrationTests.Common.DummyServices
{
    public class DummyCurrentUserService : ICurrentUserService
    {
        public string Email { get; set; } = "user@user.com";
        public string Name { get; set; } = "Jan Nowak";
        public string UserId { get; set; } = "00000000-aaaa-1111-0000-000000000000";
        public bool IsAuthenticated { get; set; } = true;
    }
}
