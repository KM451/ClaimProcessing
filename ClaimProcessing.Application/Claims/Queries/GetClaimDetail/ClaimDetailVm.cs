using AutoMapper;
using ClaimProcessing.Application.Common.Mappings;
using ClaimProcessing.Domain.Entities;

namespace ClaimProcessing.Application.Claims.Queries.GetClaimDetail
{
    public class ClaimDetailVm : IMapFrom<Claim>
    {
        public int ClaimId { get; set; }
        public string OwnerType { get; set; }
        public string ClaimType { get; set; }
        public string ItemCode { get; set; }
        public decimal Qty { get; set; }
        public string SupplierName { get; set; } 
        public string? CustomerName { get; set; }
        public string? SaleInvoiceNo { get; set; }
        public DateTime? SaleDate { get; set; }  
        public string? PurchaseInvoiceNo { get; set; }
        public DateTime? PurchaseDate { get; set; }
        public string? InternalDocNo { get; set; }
        public string? ItemName { get; set; }
        public string? ClaimDescription { get; set; }
        public string? Remarks { get; set; }
        public int ClaimStatus { get; set; }
        public bool RmaAvailable { get; set; } = false;
        public DateTime? ShipmentDate { get; set; }
        
        public void Mapping(Profile profile)
        {
            profile.CreateMap<Claim, ClaimDetailVm>()
                .ForMember(s => s.ClaimId, map => map.MapFrom(src => src.Id))
                .ForMember(s => s.SupplierName, map => map.MapFrom(src => src.Supplier.Name))
                .ForMember(s => s.SaleInvoiceNo, map => map.MapFrom(src => src.SaleDetail.SaleInvoiceNo))
                .ForMember(s => s.SaleDate, map => map.MapFrom(src => src.SaleDetail.SaleDate))
                .ForMember(p => p.PurchaseInvoiceNo, map => map.MapFrom(src => src.PurchaseDetail.PurchaseInvoiceNo))
                .ForMember(p => p.PurchaseDate, map => map.MapFrom(src => src.PurchaseDetail.PurchaseDate))
                .ForMember(p => p.InternalDocNo, map => map.MapFrom(src => src.PurchaseDetail.InternalDocNo))
                .ForMember(p => p.ShipmentDate, map => map.MapFrom(src => src.Shipment.ShipmentDate));

        }
    }
}
