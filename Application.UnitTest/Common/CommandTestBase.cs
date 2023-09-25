using AutoMapper;
using ClaimProcessing.Application.Common.Mappings;
using ClaimProcessing.Persistance;
using Moq;

namespace Application.UnitTest.Common
{
    public class CommandTestBase : IDisposable
    {
        protected readonly ClaimProcessingDbContext _context;
        protected readonly Mock<ClaimProcessingDbContext> _contextMock;
        public readonly IMapper _mapper;
        public CommandTestBase()
        {
            _contextMock = ClaimProcessingDbContextFactory.Create();
            _context = _contextMock.Object;

            var configurationProvider = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<MappingProfile>();
            });

            _mapper = configurationProvider.CreateMapper();
        }
        public void Dispose()
        {
            ClaimProcessingDbContextFactory.Destroy(_context);
        }
    }
}
