﻿using AutoMapper;
using ClaimProcessing.Application.Common.Mappings;
using ClaimProcessing.Domain.Entities;
using MediatR;

namespace ClaimProcessing.Application.Claims.Commands.CreateClaim
{
    public class CreateClaimCommand : IRequest<int>, IMapFrom<CreateClaimCommand>                
    {
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

        public void Mapping(Profile profile)
        {
            profile.CreateMap<CreateClaimCommand, Claim>()
                .ForMember(c => c.SaleDetail, map => map.MapFrom(src => new SaleDetail
                {
                    SaleInvoiceNo = src.SaleInvoiceNo,
                    SaleDate = src.SaleDate ?? DateTime.MinValue,
                }))
                .ForMember(c => c.PurchaseDetail, map => map.MapFrom(src => new PurchaseDetail
                {
                    PurchaseInvoiceNo = src.PurchaseInvoiceNo,
                    PurchaseDate = src.PurchaseDate ?? DateTime.MinValue,
                    InternalDocNo = src.InternalDocNo
                }));

        }

    }
}
