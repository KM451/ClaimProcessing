using AutoMapper;
using ClaimProcessing.Application.Common.Mappings;
using ClaimProcessing.Domain.Entities;
using ClaimProcessing.Domain.ValueObjects;
using MediatR;

namespace ClaimProcessing.Application.Packagings.Commands.CreatePackaging
{
    public class CreatePackagingCommand : IRequest<int>, IMapFrom<CreatePackagingCommand>
    {
        public string Type { get; set; }
        public double Height { get; set; }
        public double Width { get; set; }
        public double Depth { get; set; }
        public decimal Weight { get; set; }
        public string Notes { get; set; }
        public int ShipmentId { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<CreatePackagingCommand, Packaging>()
                .ForMember(c => c.Dimensions, map => map.MapFrom(src => new Dimensions
                {
                    Depth = src.Depth,
                    Width = src.Width,
                    Height = src.Height
                }));
        }
    }
}
