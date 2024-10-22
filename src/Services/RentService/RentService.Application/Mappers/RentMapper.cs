using AutoMapper;

namespace RentService.Application.Mappers
{
    public class RentMapper
    {
        private static readonly Lazy<IMapper> Lazy = new Lazy<IMapper>(() =>
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.ShouldMapProperty = p => p.GetMethod!.IsPublic || p.GetMethod.IsAssembly;
                cfg.AddProfile<RentMappingProfile>();
            });
            var mapper = config.CreateMapper();
            return mapper;
        });

        public static IMapper Mapper => Lazy.Value;
    }
}
