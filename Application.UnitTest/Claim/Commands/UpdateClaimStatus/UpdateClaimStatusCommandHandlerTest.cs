using Application.UnitTest.Common;
using ClaimProcessing.Application.Claims.Commands.UpdateClaim;
using ClaimProcessing.Application.Claims.Commands.UpdateClaimStatus;
using Microsoft.EntityFrameworkCore;
using Shouldly;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.UnitTest.Claim.Command.UpdateClaimStatus
{
    public class UpdateClaimStatusCommandHandlerTest : CommandTestBase
    {
        private readonly UpdateClaimStatusCommandHandler _handler;

        public UpdateClaimStatusCommandHandlerTest()
            :base()
        {
            _handler = new UpdateClaimStatusCommandHandler(_context, Mapper);
        }

        [Fact]
        public async Task Handle_GivenValidRequest_ShouldUpdateClaimStatus()
        {
            var command = new UpdateClaimStatusCommand()
            {
                ClaimId = 1,
                ClaimStatus = 11
            };

            await _handler.Handle(command, CancellationToken.None);
            var cs = await _context.Claims.FirstAsync(x => x.Id == command.ClaimId, CancellationToken.None);

            cs.ClaimStatus.ShouldBe(11);
        }
    }
}
