using AutoMapper;
using ClaimProcessing.Application.Common.Mappings;
using ClaimProcessing.Domain.Entities;

namespace ClaimProcessing.Application.Shipments.Queries.GetShipments
{
    public class ShipmentsVm
    {
        public ICollection<ShipmentsDto> Shipments { get; set; }
    }
}
