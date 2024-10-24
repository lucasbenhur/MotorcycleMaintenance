﻿using DeliveryManService.Core.Entities;
using DeliveryManService.Core.Specs;

namespace DeliveryManService.Core.Repositories
{
    public interface IDeliveryManRepository
    {
        Task<DeliveryMan> CreateAsync(DeliveryMan deliveryMan);
        Task<ICollection<DeliveryMan>> GetAllAsync(GetAllDeliveryMenSpecParams specParams);
        Task<bool> UpdateAsync(DeliveryMan deliveryMan);
        Task<DeliveryMan?> GetAsync(string id);
    }
}
