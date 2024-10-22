using AutoMapper;
using RentService.Application.Commands;
using RentService.Application.Responses;
using RentService.Core.Entities;

namespace RentService.Application.Mappers
{
    public class RentMappingProfile : Profile
    {
        public RentMappingProfile()
        {
            CreateMap<Rent, CreateRentCommand>().ReverseMap();
            CreateMap<RentResponse, Rent>().ReverseMap();
        }
    }
}
