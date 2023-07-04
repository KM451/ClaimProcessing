using AutoMapper;
using ClaimProcessing.Application.Common.Mappings;
using ClaimProcessing.Domain.Entities;
using MediatR;

namespace ClaimProcessing.Application.Claims.Commands.UpdateClaim
{
    public class UpdateClaimCommand : IRequest<int>, IMapFrom<UpdateClaimCommand>
    {
        public int ClaimId { get; set; }
        public string ClaimNumber { get; set; }
        public string OwnerType { get; set; }
        public string ClaimType { get; set; }
        public string ItemCode { get; set; }
        public decimal Qty { get; set; }
        public string? CustomerName { get; set; }
        public string? ItemName { get; set; }
        public string? ClaimDescription { get; set; }
        public string? Remarks { get; set; }
        public int ClaimStatus { get; set; }
        public int SupplierId { get; set; }
        public string? SaleInvoiceNo { get; set; }
        public DateTime? SaleDate { get; set; }
        public string? PurchaseInvoiceNo { get; set; }
        public DateTime? PurchaseDate { get; set; }
        public string? InternalDocNo { get; set; }
        public bool RmaAvailable { get; set; }
        public int? ShipmentId { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<UpdateClaimCommand, Claim>()
                .ForMember(c => c.Id, map => map.MapFrom(src => src.ClaimId));
            profile.CreateMap<UpdateClaimCommand, SaleDetail>();
            profile.CreateMap<UpdateClaimCommand, PurchaseDetail>();
        }

    }
}
