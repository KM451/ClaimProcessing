using ClaimProcessing.Application.Common.Interfaces;
using ClaimProcessing.Persistance;
using Microsoft.AspNetCore.Hosting;
using Moq;
using static Application.UnitTest.Common.FileStoreFactory;

namespace Application.UnitTest.Common
{
    public class CommandTestBase : IDisposable
    {
        protected readonly ClaimProcessingDbContext _context;
        protected readonly IBonfiClient _bonfi;
        protected readonly Mock<ClaimProcessingDbContext> _contextMock;
        protected readonly IFileStore _fileStore;
        protected readonly Mock<FileStoreMock> _fileStoreMock;
        protected readonly IHostingEnvironment _hosting;
        protected readonly Mock<IHostingEnvironment> _hostingMock;

        public CommandTestBase()
        {
            _contextMock = ClaimProcessingDbContextFactory.Create();
            _context = _contextMock.Object;
            _hostingMock = HostingEnvironmentFactory.Create();
            _hosting = _hostingMock.Object;
            _fileStoreMock = FileStoreFactory.Create();
            _fileStore = _fileStoreMock.Object;

        }
        public void Dispose()
        {
            ClaimProcessingDbContextFactory.Destroy(_context);
        }
    }
}
