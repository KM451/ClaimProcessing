using AutoMapper;
using ClaimProcessing.Application.Common.Mappings;
using ClaimProcessing.Domain.Entities;
using MediatR;

namespace ClaimProcessing.Application.Claims.Commands.UpdateShipmentId
{
    public class UpdateShipmentIdCommand : IRequest, IMapFrom<UpdateShipmentIdCommand>
    {
        public int ClaimId { get; set; }
        public int ShipmentId { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<UpdateShipmentIdCommand, Claim>();
        }
    }
}
