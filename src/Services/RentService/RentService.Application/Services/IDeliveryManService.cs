using RentService.Application.Entities;

namespace RentService.Application.Services
{
    public interface IDeliveryManService
    {
        Task<DeliveryMan?> Get(string id);
    }
}
