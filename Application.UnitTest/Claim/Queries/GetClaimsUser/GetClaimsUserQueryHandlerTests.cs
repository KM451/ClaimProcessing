using Application.UnitTest.Common;
using ClaimProcessing.Application.Claims.Queries.GetClaimsUser;
using ClaimProcessing.Application.Common.Interfaces;
using ClaimProcessing.Persistance;
using ClaimProcessing.Shared.Claims.Queries.GetClaimsUser;
using Shouldly;

namespace Application.UnitTest.Claim.Queries.GetClaimsUser
{
    [Collection("QueryCollection")]
    public class GetClaimsUserQueryHandlerTests
    {
        private readonly ClaimProcessingDbContext _context;
        private readonly ICurrentUserService _currentUser;

        public GetClaimsUserQueryHandlerTests(QueryTestFixtures fixtures)
        {
            _context = fixtures.Context;
            _currentUser = fixtures.CurrentUser;
        }

        [Fact]
        public async Task GetClaimsOfCurrentUser()
        {
            var handler = new GetClaimsUserQueryHandler(_context, _currentUser);
            var result = await handler.Handle(new GetClaimsUserQuery(), CancellationToken.None);

            result.ShouldBeOfType<ClaimsUserVm>();
            result.Claims.Select(x => x.ClaimNumber).FirstOrDefault().ShouldBe("C10/23");
        }

        [Fact]
        public async Task GetClaimsOfCurrentUserWithFilter()
        {
            var handler = new GetClaimsUserQueryHandler(_context, _currentUser);
            var result = await handler.Handle(new GetClaimsUserQuery { Filter = "ClaimNumber eq C10/23", Sort = "desc" }, CancellationToken.None);

            result.ShouldBeOfType<ClaimsUserVm>();
            result.Claims.Select(x => x.ClaimNumber).FirstOrDefault().ShouldBe("C10/23");
        }

        [Fact]
        public async Task GetClaimsOfCurrentUserWithFilter2()
        {
            var handler = new GetClaimsUserQueryHandler(_context, _currentUser);
            var result = await handler.Handle(new GetClaimsUserQuery { Filter = "ClaimNumber neq C10/23", Sort = "desc" }, CancellationToken.None);

            result.ShouldBeOfType<ClaimsUserVm>();
            result.Claims.Select(x => x.ClaimNumber).FirstOrDefault().ShouldBeNull();
        }
    }
}
