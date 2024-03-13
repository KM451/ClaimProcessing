using ClaimProcessing.Client.HttpRepository.Interfaces;
using ClaimProcessing.Shared.FotoUrls.Commands.CreateFotoUrl;
using System.Net.Http.Json;

namespace ClaimProcessing.Client.HttpRepository
{
    public class FotoHttpRepository(HttpClient _client) : IFotoHttpRepository
    {
        public async Task Delete(int id) => await _client.DeleteAsync($"v1/foto-urls/{id}");


        public async Task UploadFoto(CreateFotoUrlCommand command) 
            => await _client.PostAsJsonAsync("v1/foto-urls", command);

    }
}
