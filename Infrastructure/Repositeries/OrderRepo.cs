using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Contracts;
using Microsoft.EntityFrameworkCore;
using Resturant_System.Data;
using Resturant_System.Models;

namespace Infrastructure.Repositeries
{
    public class OrderRepo : IOrderRepo
    {
        private readonly ResturantDbcontext _context;

        public OrderRepo( ResturantDbcontext resturantDbcontext)
        {
            _context = resturantDbcontext;
        }
        public async Task<List<Order>> GetAllAsync()
        {
            return await _context.orders.Include(o => o.Items).ToListAsync();
        }

        public async Task<Order?> GetByIdAsync(int id)
        {
            return await _context.orders.Include(o => o.Items).FirstOrDefaultAsync(o => o.Id == id);
        }

        public async Task AddAsync(Order order)
        {
            await _context.orders.AddAsync(order);
        }

        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }
        public void UpdateItem(Order order)
        {
            _context.orders.Update(order);
        }

        public async Task<List<Menue>> GetAvailableMenuItemsAsync()
        {
            return await _context.MenueItems
                .Where(x => x.quantity > 0 && x.isavailable)
                .ToListAsync();
        }

        public void Deleteitem(Order order)
        {
            _context.orders.Remove(order);
        }
    }
}
