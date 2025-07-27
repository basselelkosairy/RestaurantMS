using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.DTO;
using Resturant_System.Models;

namespace Application.Services.Abstractions
{
    public interface IOrderService
    {

        Task<List<Order>> GetallitemsAsync();
        Task<List<Menue>> GetAvailableMenuItemsAsync();
        Task<bool> CreateOrderAsync(OrderCreateDto dto);
        Task<Order> GetOrderForEditAsync(int id);
        Task<bool> DeleteOrderAsync(int id);

        Task<bool> UpdateOrder(Order order);

    }
}
