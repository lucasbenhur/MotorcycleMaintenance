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
            CreateMap<CreateMotorcycleEvent, PublishEventCreateMotorcycleCommand>().ReverseMap();
            CreateMap<CreateMotorcycleCommand, CreateMotorcycleEvent>().ReverseMap();
            CreateMap<CreateMotorcycleResponse, Motorcycle>().ReverseMap();
        }
    }
}
