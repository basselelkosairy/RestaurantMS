using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Resturant_System.Models;

namespace Application.Contracts
{
    public interface  IOrderRepo
    {
        Task<List<Order>> GetAllAsync();
        Task<Order?> GetByIdAsync(int id);
        Task AddAsync(Order order);
        Task SaveChangesAsync();
        void UpdateItem(Order order);
        void Deleteitem(Order order);


        Task<List<Menue>> GetAvailableMenuItemsAsync();
    }
}
