using ClaimProcessing.Shared.Suppliers.Queries.GetSupplierShipments;
using Shouldly;
using WebApi.IntegrationTests.Common;
using Xunit;

namespace WebApi.IntegrationTests.Controllers.Suppliers
{
    public class GetSupplierShipments_Tests(CustomWebApplicationFactory<Program> _factory)
        : IClassFixture<CustomWebApplicationFactory<Program>>
    {
        [Fact]
        public async Task GivenSupplierId_ReturnsCollectionOfShipmentsToSupplier()
        {
            var client = await _factory.GetAuthenticatedClientAsync();
            var id = 1;
            var response = await client.GetAsync($"/api/v1/suppliers/{id}/Shipments");
            var vm = await Utilities.GetResponseContent<SupplierShipmentsVm>(response);
            response.EnsureSuccessStatusCode();
            vm.SupplierShipments.Count.ShouldBeGreaterThan(0);
        }

        [Fact]
        public async Task GivenSupplierIdAndDateInFilter_ReturnsCollectionOfShipmentsToSupplier()
        {
            var client = await _factory.GetAuthenticatedClientAsync();
            var id = 1;
            var shipmentDate = new DateTime(2023, 10, 10);
            var response = await client.GetAsync($"/api/v1/suppliers/{id}/Shipments?filter=eq%20{shipmentDate}");
            var vm = await Utilities.GetResponseContent<SupplierShipmentsVm>(response);
            response.EnsureSuccessStatusCode();
            vm.SupplierShipments.Count.ShouldBe(1);
        }

        [Fact]
        public async Task GivenSupplierIdAndDateRangeInFilter_ReturnsCollectionOfShipmentsToSupplier()
        {
            var client = await _factory.GetAuthenticatedClientAsync();
            var id = 1;
            var dateTime = new DateTime(2023, 11, 11);
            var date1 = dateTime.AddDays(-40).ToShortDateString();
            var date2 = dateTime.AddDays(40).ToShortDateString();

            var response = await client.GetAsync($"/api/v1/suppliers/{id}/Shipments?filter=in%20{date1}-{date2}");
            var vm = await Utilities.GetResponseContent<SupplierShipmentsVm>(response);
            response.EnsureSuccessStatusCode();
            vm.SupplierShipments.Count.ShouldBeGreaterThan(0);
        }

        [Fact]
        public async Task GivenSupplierIdAndDateRangeInFilter_ReturnsEmptyCollection()
        {
            var client = await _factory.GetAuthenticatedClientAsync();
            var id = 1;
            var dateTime = new DateTime(2023, 11, 11);
            var date1 = dateTime.AddYears(-1).ToShortDateString();
            var date2 = dateTime.AddDays(40).ToShortDateString();

            var response = await client.GetAsync($"/api/v1/suppliers/{id}/Shipments?filter=nin%20{date1}-{date2}");
            var vm = await Utilities.GetResponseContent<SupplierShipmentsVm>(response);
            response.EnsureSuccessStatusCode();
            vm.SupplierShipments.Count.ShouldBe(0);
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
                var response = await client.GetAsync($"/api/v1/suppliers/{id}/Shipments{filterAndSort}");
            }
            catch (Exception ex)
            {
                ex.Message.ShouldBe("Wartość podana jako filtr: 'in 2023.07.01 00:00:00-2023.12.99 00:00:00' jest nieprawidłowa.");
            }
        }
    }
}
