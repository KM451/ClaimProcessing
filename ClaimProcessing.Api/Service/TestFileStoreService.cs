using ClaimProcessing.Application.Common.Interfaces;

namespace ClaimProcessing.Api.Service
{
    public class TestFileStoreService : IFileStore
    {
        public string SafeWriteFile(byte[] content, string sourceFileName, string path) => "c:/files/test.jpg";
    }
}
