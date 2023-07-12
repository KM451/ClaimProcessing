using AutoMapper;
using ClaimProcessing.Application.Common.Exceptions;
using ClaimProcessing.Application.Common.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace ClaimProcessing.Application.Claims.Queries.GetAllClaimsShort
{
    public class GetAllClaimsShortQueryHandler : IRequestHandler<GetAllClaimsShortQuery, AllClaimsShortVm>
    {
        private readonly IClaimProcessingDbContext _context;
        private IMapper _mapper;
        public GetAllClaimsShortQueryHandler(IClaimProcessingDbContext claimProcessingDbContext, IMapper mapper)
        {
            _context = claimProcessingDbContext;
            _mapper = mapper;
        }

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
                    var field = phrases[i].Trim().Split(" ")[0].ToString();
                    var key = phrases[i].Trim().Split(" ")[1].ToString();
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
                Claims = claims.Select(src => _mapper.Map<AllClaimsShortDto>(src)).ToList()
            };

            return claimsVm;

        }
    }
}
