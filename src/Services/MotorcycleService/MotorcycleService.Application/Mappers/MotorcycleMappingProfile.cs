using AutoMapper;
using EventBus.Messages.Events;
using MotorcycleService.Application.Commands;
using MotorcycleService.Application.Responses;
using MotorcycleService.Core.Entities;

namespace MotorcycleService.Application.Mappers
{
    public class MotorcycleMappingProfile : Profile
    {
        public MotorcycleMappingProfile()
        {
            CreateMap<Motorcycle, CreateMotorcycleCommand>().ReverseMap();
            CreateMap<CreateMotorcycleEvent, Motorcycle>().ReverseMap();
            CreateMap<MotorcycleResponse, Motorcycle>().ReverseMap();
        }
    }
}
