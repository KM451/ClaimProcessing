using Application.UnitTest.Common;
using ClaimProcessing.Application.Suppliers.Queries.GetSupplierClaims;
using ClaimProcessing.Persistance;
using ClaimProcessing.Shared.Suppliers.Queries.GetSupplierClaims;
using Shouldly;

namespace Application.UnitTest.Supplier.Queries.GetSupplierClaims
{
    [Collection("QueryCollection")]
    public class GetSupplierClaimsQueryHandlerTests
    {
        private readonly ClaimProcessingDbContext _context;

        public GetSupplierClaimsQueryHandlerTests(QueryTestFixtures fixtures)
        {
            _context = fixtures.Context;
        }

        [Fact]
        public async Task GetClaimsBySupplierId()
        {
            var handler = new GetSupplierClaimsQueryHandler(_context);
            var result = await handler.Handle(new GetSupplierClaimsQuery { SupplierId = 1}, CancellationToken.None);

            result.ShouldBeOfType<SupplierClaimsVm>();
            result.SupplierClaims.Select(s => s.ClaimId).FirstOrDefault().ShouldBe(1);
        }

        [Fact]
        public async Task GetClaimsBySupplierIdWithFilter()
        {
            var handler = new GetSupplierClaimsQueryHandler(_context);
            var result = await handler.Handle(new GetSupplierClaimsQuery { SupplierId = 1, Filter = "in 1999.01.01-2000.01.02" }, CancellationToken.None);

            result.ShouldBeOfType<SupplierClaimsVm>();
            result.SupplierClaims.Count.ShouldBe(2);
        }

        [Fact]
        public async Task GetClaimsBySupplierIdWithFilter2()
        {
            var handler = new GetSupplierClaimsQueryHandler(_context);
            var result = await handler.Handle(new GetSupplierClaimsQuery { SupplierId = 1, Filter = "in 2000.01.02-2000.01.03" }, CancellationToken.None);

            result.ShouldBeOfType<SupplierClaimsVm>();
            result.SupplierClaims.ShouldBeEmpty();
        }
    }
}
