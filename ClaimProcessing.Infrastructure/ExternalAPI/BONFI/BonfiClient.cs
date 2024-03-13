using ClaimProcessing.Application.Claims.Commands.UpdateClaimStatus;
using ClaimProcessing.Application.Common.Interfaces;
using ClaimProcessing.Shared.Claims.Commands.UpdateClaimStatus;
using IdentityModel.Client;
using Newtonsoft.Json;
using System.Text;

namespace ClaimProcessing.Infrastructure.ExternalAPI.BONFI
{
    public partial class BonfiClient : IBonfiClient
    {
        private string _baseUrl = "https://api.bonfiglioli.com";
        private readonly HttpClient _httpClient;
        private Lazy<JsonSerializerSettings> _settings;

        public BonfiClient(IHttpClientFactory factory)
        {
            _httpClient = factory.CreateClient("BonfiClient");
            _baseUrl = _httpClient.BaseAddress.ToString();

            _settings = new Lazy<JsonSerializerSettings>(() =>
            {
                var settings = new JsonSerializerSettings();
                UpdateJsonSerializerSettings(settings);
                return settings;
            });
        }

        protected JsonSerializerSettings JsonSerializerSettings => _settings.Value;

        partial void UpdateJsonSerializerSettings(JsonSerializerSettings settings);

        public string BaseUrl
        {
            get { return _baseUrl; }
            set { _baseUrl = value; }
        }


        public async Task<UpdateClaimStatusVm> GetClaim(string searchFilter, CancellationToken cancellationToken)
        {
            var urlBuilder = new StringBuilder();
            urlBuilder.Append(BaseUrl != null ? BaseUrl.TrimEnd('/') : "")
                      .Append("/gateway/crm-claims/v1/claims/")
                      .Append(searchFilter);
            
            var client = _httpClient;
            var token = await GetAccessTokenAsync(client);
            client.SetBearerToken(token);
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
                        var responseData = await GetResponseContent<UpdateClaimStatusVm>(response);
                        return responseData;
                    }
                    else
                    {
                        throw new Exception();
                    }
                }
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        
        private async Task<string> GetAccessTokenAsync(HttpClient client)
        {
            var response = await client.RequestClientCredentialsTokenAsync(new ClientCredentialsTokenRequest
            {
                
            });

            if (response.IsError)
            {
                throw new Exception(response.Error);
            }

            return response.AccessToken;
        }
        public static async Task<T> GetResponseContent<T>(HttpResponseMessage response) => 
            JsonConvert.DeserializeObject<T>(await response.Content.ReadAsStringAsync());

    }
}
