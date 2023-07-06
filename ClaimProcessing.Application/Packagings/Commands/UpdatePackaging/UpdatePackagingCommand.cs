using AutoMapper;
using ClaimProcessing.Application.Common.Mappings;
using ClaimProcessing.Domain.Entities;
using ClaimProcessing.Domain.ValueObjects;
using MediatR;

namespace ClaimProcessing.Application.Packagings.Commands.UpdatePackaging
{
    public class UpdatePackagingCommand : IRequest<int>, IMapFrom<UpdatePackagingCommand>
    {
        
        public int PackagingId { get; private set; }
        public string Type { get; set; }
        public double Height { get; set; }
        public double Width { get; set; }
        public double Depth { get; set; }
        public decimal Weight { get; set; }
        public string Notes { get; set; }
        public int ShipmentId { get; set; }

        public void SetId(int id)
        {
            PackagingId = id;
        }
        public void Mapping(Profile profile)
        {
            profile.CreateMap<UpdatePackagingCommand, Packaging>()
                .ForMember(p => p.Id, map => map.MapFrom(src => src.PackagingId))
                .ForMember(p => p.Dimensions, map => map.MapFrom(src => new Dimensions
                {
                    Height = src.Height,
                    Width = src.Width,
                    Depth = src.Depth
                }));
        }


    }
}
