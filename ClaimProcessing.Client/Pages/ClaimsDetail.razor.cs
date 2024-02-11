using ClaimProcessing.Client.HttpRepository.Interfaces;
using ClaimProcessing.Client.Services.Interfaces;
using ClaimProcessing.Shared.Claims.Commands.UpdateClaim;
using ClaimProcessing.Shared.Claims.Queries.GetClaimDetail;
using ClaimProcessing.Shared.Suppliers.Queries.GetSuppliers;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;

namespace ClaimProcessing.Client.Pages
{
    public partial class ClaimsDetail
    {
        private ClaimDetailVm _detailVm;
        private SuppliersVm _suppliersVm;
        private UpdateClaimCommand _command;
        private bool isDisabled = true;
        private string title = "Dane szczegółowe zgłoszenia serwisowego";
        private Dictionary<int, string> _suppliers = new();
        private bool _isLoading = false;
        private bool _showDialog = false;
        private string _deleteDialogBody;

        [Parameter]
        public int Id { get; set; }

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
            foreach (var item in  _suppliersVm.Suppliers)
            {
                _suppliers.Add(item.SupplierId,$"{item.Name} {item.City}");
            }
            _detailVm = await ClaimsRepo.GetDetails(Id);
        }
        private void EditMode() 
        { 
            title = "Dane szczegółowe zgłoszenia serwisowego - edycja";
            isDisabled = false;
        }

        private void GoBack()
        {
            if (isDisabled)
            {
                NavigationManager.NavigateTo("/claims");
            } 
            else
            {
                NavigationManager.NavigateTo("/");
                NavigationManager.NavigateTo($"/claim/detail/{Id}");
            }
        }
        private async Task Save()
        {
            try
            {
                _isLoading = true;
                _command = Map(_detailVm);
                await ClaimsRepo.Edit(_command);
                NavigationManager.NavigateTo("/");
                NavigationManager.NavigateTo($"/claim/detail/{Id}");
                await ToastrService.ShowSuccessMessage($"Zgłoszenie zostało zaktualizowane");
            }
            finally
            {
                _isLoading = false;
            }
        }
        private async Task DeleteConfirmed(MouseEventArgs e)
        {
            _showDialog = false;
            await ClaimsRepo.Delete(_detailVm.ClaimId);
            await ToastrService.ShowSuccessMessage("Zgłoszenie zostało usunięte");
            NavigationManager.NavigateTo("/claims");
        }
        private void DeleteCanceled(MouseEventArgs e)
        {
            _showDialog = false;
        }

        private void DeleteClaim()
        {
            _deleteDialogBody = $"Czy napewno chcesz usunąć zlecenie serwisowe nr {_detailVm.ClaimNumber}?";
            _showDialog = true;
        }

        private static UpdateClaimCommand Map(ClaimDetailVm vm)
        {
            return new UpdateClaimCommand
            {
                ClaimId = vm.ClaimId,
                ClaimNumber = vm.ClaimNumber,
                OwnerType = vm.OwnerType,
                ClaimType = vm.ClaimType,
                ItemCode = vm.ItemCode,
                Qty = vm.Qty,
                CustomerName = vm.CustomerName,
                ItemName = vm.ItemName,
                ClaimDescription = vm.ClaimDescription,
                Remarks = vm.Remarks,
                ClaimStatus = vm.ClaimStatus,
                SupplierId = vm.SupplierId,
                SaleInvoiceNo = vm.SaleInvoiceNo,
                SaleDate = vm.SaleDate,
                PurchaseInvoiceNo = vm.PurchaseInvoiceNo,
                PurchaseDate = vm.PurchaseDate,
                InternalDocNo = vm.InternalDocNo,
                RmaAvailable = vm.RmaAvailable,
                ShipmentId = vm.ShipmentId,
            };
        }
    }
}



