using ClaimProcessing.Application.Common.Interfaces;

namespace ClaimProcessing.Infrastructure.FileStore
{
    public class FileStore : IFileStore
    {
        private readonly IFileWrapper _fileWrapper;
        private readonly IDirectoryWrapper _directoryWrapper;
        public FileStore(IFileWrapper fileWrapper, IDirectoryWrapper directoryWrapper)
        {
            _directoryWrapper = directoryWrapper;
            _fileWrapper = fileWrapper;
        }

        public string SafeWriteFile(byte[] content, string sourceFileName, string path)
        {
            _directoryWrapper.CreateDirectory(path);
            var outputFile = Path.Combine(path, sourceFileName);
            _fileWrapper.WriteAllBytes(outputFile, content);
            return outputFile;
        }
    }
}
