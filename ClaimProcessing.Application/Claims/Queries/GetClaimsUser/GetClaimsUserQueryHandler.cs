using ClaimProcessing.Application.Common.Exceptions;
using ClaimProcessing.Application.Common.Interfaces;
using ClaimProcessing.Domain.Entities;
using ClaimProcessing.Shared.Claims.Queries.GetClaimsUser;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace ClaimProcessing.Application.Claims.Queries.GetClaimsUser
{
    public class GetClaimsUserQueryHandler(IClaimProcessingDbContext _context, ICurrentUserService _userService) : IRequestHandler<GetClaimsUserQuery, ClaimsUserVm>
    {
        public async Task<ClaimsUserVm> Handle(GetClaimsUserQuery request, CancellationToken cancellationToken)
        {
            var claims = await _context.Claims
                .Where(p => p.StatusId != 0)
                .Where(p => p.CustomerId == _userService.UserId)
                .Include(c => c.Supplier)
                .ToListAsync(cancellationToken);

            if (claims == null)
            {
                throw new ArgumentNullException();
            }

            if (request.Filter == null)
            {
                claims = request.Sort == "desc" ? claims.OrderByDescending(i => i.Created).ToList() : claims;
            }
            else
            {
                var phrases = request.Filter.Split(",");

                for (int i = 0; i < phrases.Length; i++)
                {
                    if (phrases[i].Trim().Split(" ").Length != 3)
                    {
                        throw new InvalidFilterException(phrases[i]);
                    }
                    var field = phrases[i].Trim().Split(" ")[0].ToString();
                    var key = phrases[i].Trim().Split(" ")[1].ToString();
                    if (!((key == "eq") || (key == "neq")))
                    {
                        throw new InvalidFilterKeyException(key);
                    }
                    var value = phrases[i].Trim().Split(" ")[2].ToString();


                    switch (field)
                    {
                        case "ClaimNumber":
                            claims = key == "eq" ? claims.Where(i => i.ClaimNumber == value).ToList() : claims.Where(i => i.ClaimNumber != value).ToList();
                            break;
                        case "ItemCode":
                            claims = key == "eq" ? claims.Where(i => i.ItemCode == value).ToList() : claims.Where(i => i.ItemCode != value).ToList();
                            break;
                        case "ClaimStatus":
                            claims = key == "eq" ? claims.Where(i => i.ClaimStatus.ToString() == value).ToList() : claims.Where(i => i.ClaimStatus.ToString() != value).ToList();
                            break;
                        default: throw new InvalidFilterFieldException(field);
                    }
                }
            }

            var claimsVm = new ClaimsUserVm
            {
                Claims = claims.Select(src => Map(src)).ToList()
            };

            return claimsVm;
        }
        private static ClaimsUserDto Map(Claim claim)
        {
            return new ClaimsUserDto
            {
                ClaimNumber = claim.ClaimNumber,
                ClaimCreationDate = claim.Created,
                ItemCode = claim.ItemCode,
                ItemName = claim.ItemName,
                ClaimStatus = claim.ClaimStatus
            };
        }
    }
}
