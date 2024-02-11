using ClaimProcessing.Shared.Claims.Queries.GetAllClaimsShort;
using ClaimProcessing.Shared.Suppliers.Queries.GetSuppliers;

namespace ClaimProcessing.Client.HttpRepository.Interfaces
{
    public interface ISupplierHttpRepository
    {
        Task<SuppliersVm> GetAll();
    }
}
