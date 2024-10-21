using AutoMapper;
using DeliveryManService.Application.Commands;
using DeliveryManService.Core.Entities;

namespace DeliveryManService.Application.Mappers
{
    public class DeliveryManMappingProfile : Profile
    {
        public DeliveryManMappingProfile()
        {
            CreateMap<DeliveryMan, CreateDeliveryManCommand>().ReverseMap();
        }
    }
}
