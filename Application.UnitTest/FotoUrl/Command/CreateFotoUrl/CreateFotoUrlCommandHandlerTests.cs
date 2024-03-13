using Application.UnitTest.Common;
using ClaimProcessing.Application.FotoUrls.Commands.CreateFotoUrl;
using ClaimProcessing.Shared.FotoUrls.Commands.CreateFotoUrl;
using Microsoft.EntityFrameworkCore;
using Shouldly;

namespace Application.UnitTest.FotoUrl.Command.CreateFotoUrl
{
    public class CreateFotoUrlCommandHandlerTests : CommandTestBase
    {
        private readonly CreateFotoUrlCommandHandler _handler;

        public CreateFotoUrlCommandHandlerTests()
            : base()
        {
            _handler = new CreateFotoUrlCommandHandler(_context, _fileStore, _hosting);
        }

        [Fact]
        public async Task Handle_GivenValidRequest_ShouldInsertFotoUrl()
        {
            var command = new CreateFotoUrlCommand()
            {
                FileName = "test.jpg",
                ClaimId = 1,
                Content = new byte[0]
            };

            await _handler.Handle(command, CancellationToken.None);
            //var dir = await _context.FotoUrls.FirstAsync(x => x.Id == result, CancellationToken.None);
            //dir.ShouldNotBeNull();
        }
    }
}