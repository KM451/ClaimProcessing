using ClaimProcessing.Application.Common.Interfaces;

namespace WebApi.IntegrationTests.Common.DummyServices
{
    public class DummyDateTimeService : IDateTime
    {
        public DateTime Now { get; } = new DateTime(2023, 1, 1);
    }
}
