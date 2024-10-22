using RentService.Core.Dtos;

namespace RentService.Core.Integrations
{
    public interface IDeliveryManApi
    {
        Task<DeliveryManDto?> Get(string id);
    }
}
