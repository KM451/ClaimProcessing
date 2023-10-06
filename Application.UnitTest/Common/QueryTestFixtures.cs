using AutoMapper;
using ClaimProcessing.Application.Common.Mappings;
using ClaimProcessing.Persistance;
using Microsoft.EntityFrameworkCore;
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

        public QueryTestFixtures()
        {
            Context = ClaimProcessingDbContextFactory.Create().Object;

            var configurationProvider = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<MappingProfile>();
            });

            Mapper = configurationProvider.CreateMapper();
        }

        public void Dispose()
        {
            ClaimProcessingDbContextFactory.Destroy(Context);
        }
    }
    [CollectionDefinition("QueryCollection")]
    public class QueryCollection : ICollectionFixture<QueryTestFixtures> { }
}
