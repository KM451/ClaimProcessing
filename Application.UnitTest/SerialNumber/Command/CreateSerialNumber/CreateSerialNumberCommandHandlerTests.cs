using Application.UnitTest.Common;
using ClaimProcessing.Application.SerialNumbers.Commands.CreateSerialNumber;
using Microsoft.EntityFrameworkCore;
using Shouldly;

namespace Application.UnitTest.SerialNumber.Command.CreateSerialNumber
{
    public class CreateSerialNumberCommandHandlerTests : CommandTestBase
    {
        private readonly CreateSerialNumberCommandHandler _handler;

        public CreateSerialNumberCommandHandlerTests()
            : base()
        {
            _handler = new CreateSerialNumberCommandHandler(_context, Mapper);
        }

        [Fact]
        public async Task Handle_GivenValidRequest_ShouldInsertSerialNumber()
        {
            var command = new CreateSerialNumberCommand()
            {
                Value = "1234321",
                ClaimId = 1
            };

            var result = await _handler.Handle(command, CancellationToken.None);
            var dir = await _context.SerialNumbers.FirstAsync(x => x.Id == result, CancellationToken.None);
            dir.ShouldNotBeNull();
        }
    }
}
