using ClaimProcessing.Application.Common.Exceptions;
using ClaimProcessing.Application.Common.Interfaces;
using ClaimProcessing.Domain.Entities;
using ClaimProcessing.Shared.Claims.Queries.GetAllClaimsShort;
using MediatR;
using Microsoft.EntityFrameworkCore;


namespace ClaimProcessing.Application.Claims.Queries.GetAllClaimsShort
{
    public class GetAllClaimsShortQueryHandler(IClaimProcessingDbContext _context) : IRequestHandler<GetAllClaimsShortQuery, AllClaimsShortVm>
    {

        public async Task<AllClaimsShortVm> Handle(GetAllClaimsShortQuery request, CancellationToken cancellationToken)
        {
            var claims = await _context.Claims
                .Where(p => p.StatusId != 0)
                .Include(c => c.Supplier)
                .ToListAsync(cancellationToken);

            if (claims == null)
            {
                throw new ArgumentNullException();
            }

            if (request.Filter == null)
            {
                claims = request.Sort == "desc" ? claims.OrderByDescending(i => i.Id).ToList() : claims;
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
                        case "OwnerType":
                            claims = key == "eq" ? claims.Where(i => i.OwnerType == value).ToList() : claims.Where(i => i.OwnerType != value).ToList();
                            break;
                        case "ClaimType":
                            claims = key == "eq" ? claims.Where(i => i.ClaimType == value).ToList() : claims.Where(i => i.ClaimType != value).ToList();
                            break;
                        case "ItemCode":
                            claims = key == "eq" ? claims.Where(i => i.ItemCode == value).ToList() : claims.Where(i => i.ItemCode != value).ToList();
                            break;
                        case "Customer":
                            claims = key == "eq" ? claims.Where(i => i.CustomerName == value).ToList() : claims.Where(i => i.CustomerName != value).ToList();
                            break;
                        case "ClaimStatus":
                            claims = key == "eq" ? claims.Where(i => i.ClaimStatus.ToString() == value).ToList() : claims.Where(i => i.ClaimStatus.ToString() != value).ToList();
                            break;
                        case "RmaAvailable":
                            claims = key == "eq" ? claims.Where(i => i.RmaAvailable == (value == "true" ? true : false)).ToList() : claims.Where(i => i.RmaAvailable != (value == "true" ? true : false)).ToList();
                            break;
                        case "SupplierId":
                            claims = key == "eq" ? claims.Where(i => i.SupplierId.ToString() == value).ToList() : claims.Where(i => i.SupplierId.ToString() != value).ToList();
                            break;
                        default: throw new InvalidFilterFieldException(field);
                    }
                }
            }

            var claimsVm = new AllClaimsShortVm
            {
                Claims = claims.Select(src => Map(src)).ToList()
            };

            return claimsVm;

        }

        private AllClaimsShortDto Map(Claim claim)
        {
            return new AllClaimsShortDto
            {
                ClaimId = claim.Id,
                ClaimNumber = claim.ClaimNumber,
                ClaimCreationDate = claim.Created,
                SupplierName = claim.Supplier.Name,
                ItemCode = claim.ItemCode,
                ClaimStatus = claim.ClaimStatus
            };
        }
    }
}