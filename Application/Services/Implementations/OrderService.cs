using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Contracts;
using Application.DTO;
using Application.Services.Abstractions;
using Resturant_System.Models;

namespace Application.Services.Implementations
{
    public class OrderService : IOrderService
    {

        private readonly IOrderRepo orderRepo;

        public OrderService(IOrderRepo _orderRepo)
        {
            orderRepo = _orderRepo;   
        }

       public async Task<List<Menue>> GetAvailableMenuItemsAsync()
        {
            var items = await orderRepo.GetAvailableMenuItemsAsync();
            return items.Select(m => new Menue
            {
                Id = m.Id,
                Name = m.Name
            }).ToList();

        }
        public async Task<bool> CreateOrderAsync(OrderCreateDto dto)
        {
            var availableItems = await orderRepo.GetAvailableMenuItemsAsync();

            var order = new Order
            {
                CreatedAt = dto.CreatedAt,
                DeliveryAddress = dto.DeliveryAddress,
                Status = orderstaues.pending,
                Type = dto.Type,
                Items = new List<OrderItems>()
            };
            int totalPrepTime = 0;

            foreach (var item in dto.Items)
            {
                var menuItem = availableItems.FirstOrDefault(m => m.Id == item.MenuItemId);
                if (menuItem != null)
                {
                    var orderItem = new OrderItems
                    {
                        MenuItemId = item.MenuItemId,
                        Quantity = item.Quantity,
                        Subtotal = item.Quantity * menuItem.Price
                    };

                    menuItem.quantity -= item.Quantity;
                    menuItem.orderspredday += item.Quantity;
                    totalPrepTime += menuItem.PreparationTimeInMinutes * item.Quantity;

                    order.Items.Add(orderItem);
                }
            }

            order.PreparingAt = totalPrepTime;


            decimal subtotal = order.Items.Sum(i => i.Subtotal);
            decimal tax = subtotal * 0.085m;
            decimal discount = 0;

            if (subtotal > 100)
            {
                discount += subtotal * 0.10m;
            }

            if (dto.IsStudent || dto.IsSenior)
            {
                discount += subtotal * 0.15m;
            }

            var now = DateTime.Now;
            if (now.Hour >= 15 && now.Hour < 17)
            {
                discount += subtotal * 0.20m;
            }

            order.Total = subtotal + tax - discount;

            await orderRepo.AddAsync(order);
            await orderRepo.SaveChangesAsync();

            return true;
        }

        public async Task<List<Order>> GetallitemsAsync()
        {
         var order =   await orderRepo.GetAllAsync();

            foreach (var x in order)
            {
                UpdateOrderStatus(x);
            }
            return order;
        }

        public async Task<Order> GetOrderForEditAsync(int id)
        {
        return  await orderRepo.GetByIdAsync(id);

        }

        public async Task<bool> UpdateOrder(Order order)
        {

            orderRepo.UpdateItem(order);
            await orderRepo.SaveChangesAsync();
            return true;

        }

        public async Task<bool> DeleteOrderAsync(int id)
        {
            var deleteditem = await orderRepo.GetByIdAsync(id);

            if (deleteditem != null)
            {
                orderRepo.Deleteitem(deleteditem);
                await orderRepo.SaveChangesAsync();
                return true;
            }
            else
            {
                return false;
            }
        }

        public void UpdateOrderStatus(Order order)
        {
            var elapsed = (DateTime.Now - order.CreatedAt).TotalMinutes;

            if (order.Status == orderstaues.pending && elapsed >= 5)
            {
                order.Status = orderstaues.preparing;
            }
            else if (order.Status == orderstaues.preparing &&
                     elapsed >= 5 + order.PreparingAt)
            {
                order.Status = orderstaues.ready;
            }
        }

    }

}
