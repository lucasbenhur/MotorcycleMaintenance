using AutoMapper;
using MotorcycleService.Application.Commands;
using MotorcycleService.Core.Entities;

namespace MotorcycleService.Application.Mappers
{
    public class MotorcycleMapper
    {
        private static readonly Lazy<IMapper> Lazy = new Lazy<IMapper>(() =>
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.ShouldMapProperty = p => p.GetMethod.IsPublic || p.GetMethod.IsAssembly;
                cfg.AddProfile<MotorcycleMappingProfile>();
            });
            var mapper = config.CreateMapper();
            return mapper;
        });

        public static IMapper Mapper => Lazy.Value;

        public static Motorcycle Map(CreateMotorcycleCommand createMotorcycleCommand)
        {
            return new Motorcycle(
                createMotorcycleCommand.Id,
                createMotorcycleCommand.Year,
                createMotorcycleCommand.Model,
                createMotorcycleCommand.Plate);
        }
    }
}
