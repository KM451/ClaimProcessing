using Application.UnitTest.Common;
using AutoMapper;
using ClaimProcessing.Application.Claims.Queries.GetAllClaimsShort;
using ClaimProcessing.Application.Claims.Queries.GetClaimAttachmentsUrls;
using ClaimProcessing.Persistance;
using Shouldly;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.UnitTest.Claim.Queries.GetClaimAttachmentUrls
{
    [Collection("QueryCollection")]
    public class GetClaimAttachmentUrlQueryHandlerTests
    {
        private readonly ClaimProcessingDbContext _context;
        private readonly IMapper _mapper;

        public GetClaimAttachmentUrlQueryHandlerTests(QueryTestFixtures fixtures)
        {
            _context = fixtures.Context;
            _mapper = fixtures.Mapper;
        }

        [Fact]

        public async Task GetAttachmentUrlsByClaimId()
        {
            var handler = new GetClaimAttachmentUrlsQueryHandler(_context, _mapper);
            var result = await handler.Handle(new GetClaimAttachmentUrlsQuery { ClaimId = 1}, CancellationToken.None);
            var result2 = await handler.Handle(new GetClaimAttachmentUrlsQuery { ClaimId = 2}, CancellationToken.None);

            result.ShouldBeOfType<ClaimAttachmentUrlsVm>();
            result2.ShouldBeOfType<ClaimAttachmentUrlsVm>();
            result.AttachmentUrls.Count.ShouldBe(0);
            result2.AttachmentUrls.Count.ShouldBe(1);
            result2.AttachmentUrls.FirstOrDefault().Path.ShouldBe("C:\\Windows\\System32");

        }
    }
}
