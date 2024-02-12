using ClaimProcessing.Application.Common.Exceptions;
using ClaimProcessing.Application.Common.Interfaces;
using ClaimProcessing.Domain.Entities;
using ClaimProcessing.Shared.Suppliers.Queries.GetSupplierShipments;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace ClaimProcessing.Application.Suppliers.Queries.GetSupplierShipments
{
    public class GetSupplierShipmentsQueryHandler(IClaimProcessingDbContext _context) : IRequestHandler<GetSupplierShipmentsQuery, SupplierShipmentsVm>
    {
        public async Task<SupplierShipmentsVm> Handle(GetSupplierShipmentsQuery request, CancellationToken cancellationToken)
        {
            var supplierShipments = await _context.Shipments
                .Where(s => s.StatusId != 0 && s.SupplierId == request.SupplierId)
                .ToListAsync(cancellationToken);

            if (supplierShipments == null)
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
                    if (splitDate.Count() != 2 || !DateTime.TryParse(splitDate[0].ToString(), out date1) || !DateTime.TryParse(splitDate[1].ToString(), out date2))
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
                        supplierShipments = supplierShipments.Where(i => i.ShipmentDate.Date == date1).ToList();
                        break;
                    case "in":
                        supplierShipments = supplierShipments.Where(i => i.ShipmentDate.Date >= date1 && i.ShipmentDate.Date <= date2).ToList();
                        break;
                    case "nin":
                        supplierShipments = supplierShipments.Where(i => i.ShipmentDate.Date < date1 || i.ShipmentDate.Date > date2).ToList();
                        break;
                    case "neq":
                        supplierShipments = supplierShipments.Where(i => i.ShipmentDate.Date != date1).ToList();
                        break;
                    case "gt":
                        supplierShipments = supplierShipments.Where(i => i.ShipmentDate.Date > date1).ToList();
                        break;
                    case "gte":
                        supplierShipments = supplierShipments.Where(i => i.ShipmentDate.Date >= date1).ToList();
                        break;
                    case "lt":
                        supplierShipments = supplierShipments.Where(i => i.ShipmentDate.Date < date1).ToList();
                        break;
                    case "lte":
                        supplierShipments = supplierShipments.Where(i => i.ShipmentDate.Date <= date1).ToList();
                        break;
                    default: throw new InvalidFilterKeyException(key);
                }

            }

            var supplierShipmentsVm = new SupplierShipmentsVm
            {
                SupplierShipments = supplierShipments.Select(src => Map(src)).ToList()
            };

            return supplierShipmentsVm;
        }

        private static SupplierShipmentsDto Map(Shipment shipment)
        {
            return new SupplierShipmentsDto
            {
                ShipmentDate = shipment.ShipmentDate,
                Speditor = shipment.Speditor,
                ShippingDocumentNo = shipment.ShippingDocumentNo,
                TotalWeight = shipment.TotalWeight ?? throw new NullReferenceException("The value of 'shipment.TotalWeight' should not be null")
            };
        }
    }
}
