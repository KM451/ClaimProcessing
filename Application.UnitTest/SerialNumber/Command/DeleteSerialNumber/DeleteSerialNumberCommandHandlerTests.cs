using Application.UnitTest.Common;
using ClaimProcessing.Application.SerialNumbers.Commands.DeleteSerialNumber;
using Microsoft.EntityFrameworkCore;
using Shouldly;

namespace Application.UnitTest.SerialNumber.Command.DeleteSerialNumber
{
    public class DeleteSerialNumberCommandHandlerTests : CommandTestBase
    {
        private readonly DeleteSerialNumberCommandHandler _handler;

        public DeleteSerialNumberCommandHandlerTests()
            : base()
        {
            _handler = new DeleteSerialNumberCommandHandler(_context);
        }

        [Fact]
        public async Task Handle_GivenValidRequest_ShouldDeleteSerialNumber()
        {
            var command = new DeleteSerialNumberCommand()
            {
                SerialNumberId = 1
            };

            await _handler.Handle(command, CancellationToken.None);
            var dir = await _context.SerialNumbers.FirstAsync(x => x.Id == 1 && x.StatusId == 0, CancellationToken.None);
            dir.ShouldNotBeNull();
        }

    }

}