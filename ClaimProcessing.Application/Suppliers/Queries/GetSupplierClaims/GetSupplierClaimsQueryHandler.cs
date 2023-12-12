using AutoMapper;
using ClaimProcessing.Application.Common.Exceptions;
using ClaimProcessing.Application.Common.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace ClaimProcessing.Application.Suppliers.Queries.GetSupplierClaims
{
    public class GetSupplierClaimsQueryHandler : IRequestHandler<GetSupplierClaimsQuery, SupplierClaimsVm>
    {
        private readonly IClaimProcessingDbContext _context;
        private IMapper _mapper;
        public GetSupplierClaimsQueryHandler(IClaimProcessingDbContext claimProcessingDbContext, IMapper mapper)
        {
            _context = claimProcessingDbContext;
            _mapper = mapper;
        }
        public async Task<SupplierClaimsVm> Handle(GetSupplierClaimsQuery request, CancellationToken cancellationToken)
        {
            var supplierClaims = await _context.Claims
                .Include(p => p.Shipment)
                .Include(p => p.Supplier)
                .Where(p => p.StatusId != 0 && p.SupplierId == request.SupplierId)
                .ToListAsync(cancellationToken);

            if (supplierClaims == null)
            {
                throw new ArgumentNullException();
            }

            DateTime date1 = new DateTime();
            DateTime date2 = new DateTime();

            if (request.Filter != null)
            {
                string key, datePart;
                try
                {
                    key = request.Filter.Split(" ")[0].ToString();
                    datePart = request.Filter.Split(" ")[1].ToString();
                }
                catch
                {
                    throw new InvalidFilterException(request.Filter);
                }
                
                if (key == "in" || key == "nin")
                {
                    var splitDate = datePart.Split("-");
                    if (splitDate.Count()!=2 || !DateTime.TryParse(splitDate[0].ToString(), out date1) || !DateTime.TryParse(splitDate[1].ToString(), out date2))
                    {
                        throw new InvalidFilterException(request.Filter);
                    }
                }
                else
                {
                    if (!DateTime.TryParse(datePart, out date1))
                    {
                        throw new InvalidFilterDateException(datePart);
                    }
                }

                switch (key)
                {
                    case "eq":
                        supplierClaims = supplierClaims.Where(i => i.Created.Date == date1).ToList();
                        break;
                    case "in":
                        supplierClaims = supplierClaims.Where(i => i.Created.Date >= date1 && i.Created.Date <= date2).ToList();
                        break;
                    case "nin":
                        supplierClaims = supplierClaims.Where(i => i.Created.Date < date1 || i.Created.Date > date2).ToList();
                        break;
                    case "neq":
                        supplierClaims = supplierClaims.Where(i => i.Created.Date != date1).ToList();
                        break;
                    case "gt":
                        supplierClaims = supplierClaims.Where(i => i.Created.Date > date1).ToList();
                        break;
                    case "gte":
                        supplierClaims = supplierClaims.Where(i => i.Created.Date >= date1).ToList();
                        break;
                    case "lt":
                        supplierClaims = supplierClaims.Where(i => i.Created.Date < date1).ToList();
                        break;
                    case "lte":
                        supplierClaims = supplierClaims.Where(i => i.Created.Date <= date1).ToList();
                        break;
                    default: throw new InvalidFilterKeyException(key);
                }

            }

            var supplierClaimsVm = new SupplierClaimsVm
            {
                SupplierClaims = supplierClaims.Select(src => _mapper.Map<SupplierClaimsDto>(src)).ToList()
            };

            return supplierClaimsVm;
        }
    }
}
