using Application.UnitTest.Common;
using ClaimProcessing.Application.Suppliers.Queries.GetSupplierDetail;
using ClaimProcessing.Persistance;
using ClaimProcessing.Shared.Suppliers.Queries.GetSupplierDetail;
using Shouldly;

namespace Application.UnitTest.Supplier.Queries.GetSupplierDetail
{
    [Collection("QueryCollection")]
    public class GetSupplierDetailQueryHandlerTests
    {
        private readonly ClaimProcessingDbContext _context;

        public GetSupplierDetailQueryHandlerTests(QueryTestFixtures fixtures)
        {
            _context = fixtures.Context;
        }

        [Fact]

        public async Task GetSupplierDetailById()
        {
            var handler = new GetSupplierDetailQueryHandler(_context);
            var result = await handler.Handle(new GetSupplierDetailQuery { SupplierId = 1 }, CancellationToken.None);

            result.ShouldBeOfType<SupplierDetailVm>();
            result.ContactPerson.ShouldBe("Bruce Lee");
        }
    }
}
