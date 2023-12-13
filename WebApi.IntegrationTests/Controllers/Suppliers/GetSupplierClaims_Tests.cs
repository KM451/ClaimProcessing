using ClaimProcessing.Application.Common.Interfaces;
using ClaimProcessing.Application.Suppliers.Queries.GetSupplierClaims;
using Shouldly;
using WebApi.IntegrationTests.Common;
using Xunit;

namespace WebApi.IntegrationTests.Controllers.Suppliers
{
    public class GetSupplierClaims_Tests(CustomWebApplicationFactory<Program> _factory)
        : IClassFixture<CustomWebApplicationFactory<Program>>
    {
        [Fact]
        public async Task GivenSupplierId_ReturnsCollectionOfClaimsToSupplier()
        {
            var client = await _factory.GetAuthenticatedClientAsync();
            var id = 1;
            var response = await client.GetAsync($"/api/v1/suppliers/{id}/Claims");
            var vm = await Utilities.GetResponseContent<SupplierClaimsVm>(response);
            response.EnsureSuccessStatusCode();
            vm.SupplierClaims.Count.ShouldBeGreaterThan(1);
        }

        [Fact]
        public async Task GivenSupplierIdAndDateInFilter_ReturnsCollectionOfClaimsToSupplier()
        {
            var client = await _factory.GetAuthenticatedClientAsync();
            var id = 1;
            var claimDate = _factory.Services.GetService<IDateTime>().Now;
            
            var response = await client.GetAsync($"/api/v1/suppliers/{id}/Claims?filter=eq%20{claimDate}");
            var vm = await Utilities.GetResponseContent<SupplierClaimsVm>(response);
            response.EnsureSuccessStatusCode();
            vm.SupplierClaims.Count.ShouldBeGreaterThan(0);
        }

        [Fact]
        public async Task GivenSupplierIdAndDateRangeInFilter_ReturnsCollectionOfClaimsToSupplier()
        {
            var client = await _factory.GetAuthenticatedClientAsync();
            var id = 1;
            var dateTime = _factory.Services.GetService<IDateTime>();
            var date1 = dateTime.Now.AddDays(-10).ToShortDateString();
            var date2 = dateTime.Now.AddDays(10).ToShortDateString();

            var response = await client.GetAsync($"/api/v1/suppliers/{id}/Claims?filter=in%20{date1}-{date2}");
            var vm = await Utilities.GetResponseContent<SupplierClaimsVm>(response);
            response.EnsureSuccessStatusCode();
            vm.SupplierClaims.Count.ShouldBeGreaterThan(1);
        }

        [Fact]
        public async Task GivenSupplierIdAndDateRangeInFilter_ReturnsEmptyCollection()
        {
            var client = await _factory.GetAuthenticatedClientAsync();
            var id = 1;
            var dateTime = _factory.Services.GetService<IDateTime>();
            var date1 = dateTime.Now.AddDays(-10).ToShortDateString();
            var date2 = dateTime.Now.AddDays(10).ToShortDateString();

            var response = await client.GetAsync($"/api/v1/suppliers/{id}/Claims?filter=nin%20{date1}-{date2}");
            var vm = await Utilities.GetResponseContent<SupplierClaimsVm>(response);
            response.EnsureSuccessStatusCode();
            vm.SupplierClaims.Count.ShouldBe(0);
        }

        [Fact]
        public async Task GivenWrongFilter_GeneratesExMessage()
        {
            var client = await _factory.GetAuthenticatedClientAsync();
            var id = 1;
            var d = DateTime.Now;
            var filterAndSort = "?filter=in%202023.07.01 00:00:00-2023.12.99 00:00:00";
            try
            {
                var response = await client.GetAsync($"/api/v1/suppliers/{id}/Claims{filterAndSort}");
            }
            catch (Exception ex)
            {
                ex.Message.ShouldBe("Wartość podana jako filtr: 'in 2023.07.01 00:00:00-2023.12.99 00:00:00' jest nieprawidłowa.");
            }
        }
    }
}
