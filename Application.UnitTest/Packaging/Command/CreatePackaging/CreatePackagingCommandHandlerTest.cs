using Application.UnitTest.Common;
using ClaimProcessing.Application.Packagings.Commands.CreatePackaging;
using ClaimProcessing.Domain.ValueObjects;
using ClaimProcessing.Shared.Packagings.Commands.CreatePackaging;
using Microsoft.EntityFrameworkCore;
using Shouldly;

namespace Application.UnitTest.Packaging.Command.CreatePackaging
{
    public class CreatePackagingCommandHandlerTest : CommandTestBase
    {
        private readonly CreatePackagingCommandHandler _handler;

        public CreatePackagingCommandHandlerTest()
            :base()
        {
            _handler = new CreatePackagingCommandHandler(_context);
        }

        [Fact]
        public async Task Handle_GivenValidRequest_ShouldInsertPackaging()
        {
            var command = new CreatePackagingCommand()
            {
                Type = "test",
                Height = 123,
                Width = 234,
                Depth = 345,
                Weight = 456,
                Notes = "test",
                ShipmentId = 3
            };

            var result = await _handler.Handle(command, CancellationToken.None);
            var dir = await _context.Packagings.FirstAsync(x => x.Id == result, CancellationToken.None);
            dir.ShouldNotBeNull();
            dir.Dimensions.ShouldBe(new Dimensions(command.Height,command.Width,command.Depth));
        }
    }
}
