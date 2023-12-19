using ClaimProcessing.Application.Common.Interfaces;

namespace WebApi.IntegrationTests.Common.DummyServices
{
    public class DummyIntamiClientService : IIntamiClient
    {
        public async Task<string> GetCity(string searchFilter, CancellationToken cancellationToken)
        {
            if (searchFilter == "87-100")
            {
                return "[\r\n  {\r\n    \"kod\": \"87-100\",\r\n    \"miejscowosc\": \"Toruń\",\r\n    \"gmina\": \"Toruń\",\r\n    \"powiat\": \"Toruń\",\r\n    \"wojewodztwo\": \"kujawsko-pomorskie\",\r\n    \"numeracja\": []\r\n  }\r\n]";
            }
            else return string.Empty;
        }
    }
}
