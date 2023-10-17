using Application.UnitTest.Common;
using ClaimProcessing.Application.Packagings.Commands.UpdatePackaging;
using ClaimProcessing.Domain.ValueObjects;
using Microsoft.EntityFrameworkCore;
using Shouldly;

namespace Application.UnitTest.Packaging.Command.UpdatePackaging
{
    public class UpdatePackagingCommandHandlerTest : CommandTestBase
    {
        private readonly UpdatePackagingCommandHandler _handler;

        public UpdatePackagingCommandHandlerTest()
            : base()
        {
            _handler = new UpdatePackagingCommandHandler(_context, _mapper);
        }

        [Fact]
        public async Task Handle_GivenValidRequest_ShouldUpdatePackaging()
        {
            var command = new UpdatePackagingCommand()
            {
                Type = "test1",
                Height = 111,
                Width = 222,
                Depth = 333,
                Weight = 444,
                Notes = "test1",
                ShipmentId = 9
            };

            var result = await _handler.Handle(command, CancellationToken.None);
            var p = await _context.Packagings.FirstAsync(x => x.Id == result, CancellationToken.None);
            p.Type.ShouldBe(command.Type);
            p.Dimensions.ShouldBe(new Dimensions(command.Height, command.Width, command.Depth));
            p.Weight.ShouldBe(command.Weight);
            p.Notes.ShouldBe(command.Notes);
            p.ShipmentId.ShouldBe(command.ShipmentId);  

        }
    }
}