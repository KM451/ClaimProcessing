using ClaimProcessing.Application.Common.Interfaces;
using Newtonsoft.Json;
using System.Text;

namespace ClaimProcessing.Infrastructure.ExternalAPI.INTAMI
{
    public partial class IntamiClient : IIntamiClient
    {
        private string _baseUrl = "http://kodpocztowy.intami.pl";
        private readonly HttpClient _httpClient;
        private System.Lazy<JsonSerializerSettings> _settings;

        public IntamiClient(IHttpClientFactory factory) 
        {
            _httpClient = factory.CreateClient("IntamiClient");
            _baseUrl = _httpClient.BaseAddress.ToString();
            _settings = new Lazy<JsonSerializerSettings>(() =>
            {
                var settings = new JsonSerializerSettings();
                UpdateJsonSerializerSettings(settings);
                return settings;
            });
        }

        protected JsonSerializerSettings JsonSerializerSettings { get { return _settings.Value; } }

        partial void UpdateJsonSerializerSettings(JsonSerializerSettings settings);

        public string BaseUrl
        {
            get { return _baseUrl; }
            set { _baseUrl = value; }
        }

      
        public async Task<string> GetCity(string searchFilter, CancellationToken cancellationToken)
        {
            var urlBuilder = new StringBuilder();
            urlBuilder.Append(BaseUrl != null ? BaseUrl.TrimEnd('/') : "").Append("/kod/");
            urlBuilder.Append(searchFilter);
            var client = _httpClient;
            try
            {
                using (var request = new HttpRequestMessage())
                {
                    request.Method = new HttpMethod("GET");
                    var url = urlBuilder.ToString();
                    request.RequestUri = new Uri(url, UriKind.RelativeOrAbsolute);
                    var response = await client.SendAsync(request, HttpCompletionOption.ResponseHeadersRead, cancellationToken);

                    if (response.StatusCode == System.Net.HttpStatusCode.OK)
                    {
                        var responseData = response.Content == null ? null : await response.Content.ReadAsStringAsync().ConfigureAwait(false);
                        return responseData;
                    }
                    else
                    {
                        return "Someting bad happened";
                    }
                }
            }
            catch (Exception ex)
            {
                throw;
            }
        }
    }
}
