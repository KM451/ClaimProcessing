using Application.UnitTest.Common;
using ClaimProcessing.Application.Suppliers.Queries.GetSupplierShipments;
using ClaimProcessing.Persistance;
using ClaimProcessing.Shared.Suppliers.Queries.GetSupplierShipments;
using Shouldly;

namespace Application.UnitTest.Supplier.Queries.GetSupplierShipments
{
    [Collection("QueryCollection")]
    public class GetSupplierShipmentsQueryHandlerTests
    {
        private readonly ClaimProcessingDbContext _context;

        public GetSupplierShipmentsQueryHandlerTests(QueryTestFixtures fixtures)
        {
            _context = fixtures.Context;
        }

        [Fact]
        public async Task GetShipmentsBySupplierId()
        {
            var handler = new GetSupplierShipmentsQueryHandler(_context);
            var result = await handler.Handle(new GetSupplierShipmentsQuery { SupplierId = 1}, CancellationToken.None);

            result.ShouldBeOfType<SupplierShipmentsVm>();
            result.SupplierShipments.Select(s => s.ShippingDocumentNo).FirstOrDefault().ShouldBe("A1234XYZ");
        }

        [Fact]
        public async Task GetShipmentsBySupplierIdWithFilter()
        {
            var handler = new GetSupplierShipmentsQueryHandler(_context);
            var result = await handler.Handle(new GetSupplierShipmentsQuery { SupplierId = 1, Filter = "eq 2000.01.01" }, CancellationToken.None);

            result.ShouldBeOfType<SupplierShipmentsVm>();
            result.SupplierShipments.Select(s => s.ShippingDocumentNo).FirstOrDefault().ShouldBe("A1234XYZ");
        }

        [Fact]
        public async Task GetShipmentsBySupplierIdWithFilter2()
        {
            var handler = new GetSupplierShipmentsQueryHandler(_context);
            var result = await handler.Handle(new GetSupplierShipmentsQuery { SupplierId = 1, Filter = "eq 2000.01.02" }, CancellationToken.None);

            result.ShouldBeOfType<SupplierShipmentsVm>();
            result.SupplierShipments.ShouldBeEmpty();
        }
    }
}
