using Application.UnitTest.Common;
using ClaimProcessing.Application.Suppliers.Queries.GetSuppliers;
using ClaimProcessing.Persistance;
using ClaimProcessing.Shared.Suppliers.Queries.GetSuppliers;
using Shouldly;

namespace Application.UnitTest.Supplier.Queries.GetSuppliers
{
    [Collection("QueryCollection")]
    public class GetSuppliersQueryHandlerTests
    {
        private readonly ClaimProcessingDbContext _context;

        public GetSuppliersQueryHandlerTests(QueryTestFixtures fixtures)
        {
            _context = fixtures.Context;
        }

        [Fact]
        public async Task GetAllSuppliers()
        {
            var handler = new GetSuppliersQueryHandler(_context);
            var result = await handler.Handle(new GetSuppliersQuery(), CancellationToken.None);

            result.ShouldBeOfType<SuppliersVm>();
            result.Suppliers.Select(s => s.Name).FirstOrDefault().ShouldBe("Supplier");
        }
    }
}
