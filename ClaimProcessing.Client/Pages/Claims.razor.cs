using ClaimProcessing.Client.HttpRepository.Interfaces;
using ClaimProcessing.Shared.Claims.Queries.GetAllClaimsShort;
using Microsoft.AspNetCore.Components;

namespace ClaimProcessing.Client.Pages
{
    public partial class Claims
    {
        private AllClaimsShortVm _claims;

        [Inject]
        public IClaimsHttpRepository ClaimsRepo { get; set; }

        protected override async Task OnInitializedAsync()
        {
            _claims = await ClaimsRepo.GetAll();
        }
    }
}
