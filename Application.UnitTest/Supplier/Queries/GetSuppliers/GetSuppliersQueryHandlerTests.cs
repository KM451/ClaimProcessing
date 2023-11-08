using Application.UnitTest.Common;
using AutoMapper;
using ClaimProcessing.Application.Suppliers.Queries.GetSuppliers;
using ClaimProcessing.Persistance;
using Shouldly;

namespace Application.UnitTest.Supplier.Queries.GetSuppliers
{
    [Collection("QueryCollection")]
    public class GetSuppliersQueryHandlerTests
    {
        private readonly ClaimProcessingDbContext _context;
        private readonly IMapper _mapper;

        public GetSuppliersQueryHandlerTests(QueryTestFixtures fixtures)
        {
            _context = fixtures.Context;
            _mapper = fixtures.Mapper;
        }

        [Fact]
        public async Task GetAllSuppliers()
        {
            var handler = new GetSuppliersQueryHandler(_context, _mapper);
            var result = await handler.Handle(new GetSuppliersQuery(), CancellationToken.None);

            result.ShouldBeOfType<SuppliersVm>();
            result.Suppliers.Select(s => s.Name).FirstOrDefault().ShouldBe("Supplier");
        }
    }
}
