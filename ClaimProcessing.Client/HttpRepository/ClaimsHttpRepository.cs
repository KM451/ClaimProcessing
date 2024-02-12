using ClaimProcessing.Client.HttpRepository.Interfaces;
using ClaimProcessing.Shared.Claims.Commands.CreateClaim;
using ClaimProcessing.Shared.Claims.Commands.UpdateClaim;
using ClaimProcessing.Shared.Claims.Queries.GetAllClaimsShort;
using ClaimProcessing.Shared.Claims.Queries.GetClaimDetail;
using System.Net.Http.Json;

namespace ClaimProcessing.Client.HttpRepository
{
    public class ClaimsHttpRepository(HttpClient _client) : IClaimsHttpRepository
    {
        public async Task Add(CreateClaimCommand command) => await _client.PostAsJsonAsync("v1/claims", command);

        public async Task Delete(int id) => await _client.DeleteAsync($"v1/claims/{id}");
 
        public async Task Edit(UpdateClaimCommand command) => await _client.PutAsJsonAsync("v1/claims", command);

        public async Task<AllClaimsShortVm> GetAll() => await _client.GetFromJsonAsync<AllClaimsShortVm>("v1/claims");

        public async Task<ClaimDetailVm> GetDetails(int id) => await _client.GetFromJsonAsync<ClaimDetailVm>($"v1/claims/{id}");
        
    }
}
