using ClaimProcessing.Application.Common.Interfaces;

namespace ClaimProcessing.Infrastructure.Services
{
    public class DateTimeService : IDateTime
    {
        public DateTime Now => DateTime.Now;
    }
}
