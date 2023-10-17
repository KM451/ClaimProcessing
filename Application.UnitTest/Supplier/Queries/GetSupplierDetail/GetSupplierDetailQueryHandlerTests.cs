using Application.UnitTest.Common;
using AutoMapper;
using ClaimProcessing.Application.Suppliers.Queries.GetSupplierDetail;
using ClaimProcessing.Persistance;
using Shouldly;

namespace Application.UnitTest.Supplier.Queries.GetSupplierDetail
{
    [Collection("QueryCollection")]
    public class GetSupplierDetailQueryHandlerTests
    {
        private readonly ClaimProcessingDbContext _context;
        private readonly IMapper _mapper;

        public GetSupplierDetailQueryHandlerTests(QueryTestFixtures fixtures)
        {
            _context = fixtures.Context;
            _mapper = fixtures.Mapper;
        }

        [Fact]

        public async Task GetSupplierDetailById()
        {
            var handler = new GetSupplierDetailQueryHandler(_context, _mapper);
            var result = await handler.Handle(new GetSupplierDetailQuery { SupplierId = 1 }, CancellationToken.None);

            result.ShouldBeOfType<SupplierDetailVm>();
            result.ContactPerson.ShouldBe("Bruce Lee");
        }
    }
}
