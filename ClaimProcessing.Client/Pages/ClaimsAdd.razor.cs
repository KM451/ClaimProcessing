using ClaimProcessing.Client.HttpRepository.Interfaces;
using ClaimProcessing.Client.Services.Interfaces;
using ClaimProcessing.Shared.Claims.Commands.CreateClaim;
using ClaimProcessing.Shared.Suppliers.Queries.GetSuppliers;
using Microsoft.AspNetCore.Components;

namespace ClaimProcessing.Client.Pages
{
    public partial class ClaimsAdd()
    {
        private bool _isLoading = false;
        private SuppliersVm _suppliersVm;
        private Dictionary<int, string> _suppliers = new();
        private CreateClaimCommand _command = new CreateClaimCommand
        {
            PurchaseDate = DateTime.Now,
            SaleDate = DateTime.Now,
            ClaimStatus = 1
        };
        [Inject]
        public IClaimsHttpRepository ClaimsRepo { get; set; }

        [Inject]
        public ISupplierHttpRepository SuppliersRepo { get; set; }

        [Inject]
        public NavigationManager NavigationManager { get; set; }

        [Inject]
        public IToastrService ToastrService { get; set; }

        protected override async Task OnInitializedAsync()
        {
            _suppliersVm = await SuppliersRepo.GetAll();
            foreach (var item in _suppliersVm.Suppliers)
            {
                _suppliers.Add(item.SupplierId, $"{item.Name} {item.City}");
            }
        }
        private async Task Save()
        {
            try
            {
                _isLoading = true;
                await ClaimsRepo.Add(_command);
                NavigationManager.NavigateTo("/claims");
                await ToastrService.ShowSuccessMessage($"Zgłoszenie zostało dodane");
            }
            finally
            {
                _isLoading = false;
            }
        }

    }
}
