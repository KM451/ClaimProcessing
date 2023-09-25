using AutoMapper;
using ClaimProcessing.Application.Common.Interfaces;
using ClaimProcessing.Application.Common.Mappings;
using ClaimProcessing.Persistance;
using Microsoft.EntityFrameworkCore;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.UnitTest.Common
{
    public class QueryTestFixtures : IDisposable
    {
        public ClaimProcessingDbContext Context { get; private set; }
        public IMapper Mapper { get; private set; }
        public ICurrentUserService CurrentUser { get; private set; }


        public QueryTestFixtures()
        {
            Context = ClaimProcessingDbContextFactory.Create().Object;

            var configurationProvider = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<MappingProfile>();
            });

            Mapper = configurationProvider.CreateMapper();

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
