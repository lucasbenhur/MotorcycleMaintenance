using AutoMapper;
using RentService.Application.Commands;
using RentService.Application.Responses;
using RentService.Core.Entities;

namespace RentService.Application.Mappers
{
    public class RentalMappingProfile : Profile
    {
        public RentalMappingProfile()
        {
            CreateMap<Rental, RentMotorcycleCommand>().ReverseMap();
            CreateMap<RentalResponse, Rental>().ReverseMap();
        }
    }
}
