using RentService.Core.Dtos;

namespace RentService.Core.Integrations
{
    public interface IDeliveryManApi
    {
        Task<DeliveryManDto?> GetAsync(string id);
    }
}
