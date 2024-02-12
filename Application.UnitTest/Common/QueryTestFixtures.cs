using ClaimProcessing.Application.Common.Interfaces;
using ClaimProcessing.Persistance;
using Moq;

namespace Application.UnitTest.Common
{
    public class QueryTestFixtures : IDisposable
    {
        public ClaimProcessingDbContext Context { get; private set; }
        public ICurrentUserService CurrentUser { get; private set; }


        public QueryTestFixtures()
        {
            Context = ClaimProcessingDbContextFactory.Create().Object;

            var _currentUser = new Mock<ICurrentUserService>();
            _currentUser.Setup(m => m.UserId).Returns("00000000-aaaa-1111-0000-000000000000");
            CurrentUser = _currentUser.Object;
        }

        public void Dispose()
        {
            ClaimProcessingDbContextFactory.Destroy(Context);
        }
    }
    [CollectionDefinition("QueryCollection")]
    public class QueryCollection : ICollectionFixture<QueryTestFixtures> { }
}
