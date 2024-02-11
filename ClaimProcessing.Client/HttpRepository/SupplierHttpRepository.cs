using ClaimProcessing.Client.HttpRepository.Interfaces;
using ClaimProcessing.Shared.Suppliers.Queries.GetSuppliers;
using System.Net.Http.Json;

namespace ClaimProcessing.Client.HttpRepository
{
    public class SupplierHttpRepository(HttpClient _client) : ISupplierHttpRepository
    {
        public async Task<SuppliersVm> GetAll() => await _client.GetFromJsonAsync<SuppliersVm>("v1/suppliers");
    }
}
