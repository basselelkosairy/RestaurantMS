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
    public class DashboardRepository : IDashboardRepository
    {
        private readonly ResturantDbcontext _context;

        public DashboardRepository(ResturantDbcontext context)
        {
            _context = context;
        }

        public async Task<decimal> GetTodayRevenueAsync()
        {
            var today = DateTime.Today;
            var tomorrow = today.AddDays(1);

            return await _context.orders
                .Where(o => o.CreatedAt >= today && o.CreatedAt < tomorrow)
                .SumAsync(o => (decimal?)o.Total) ?? 0m;
        }

        public async Task<int> GetItemsToReorderCountAsync()
        {
            return await _context.MenueItems.CountAsync(m => m.quantity < 5);
        }

        public async Task<int> GetPendingKOTCountAsync()
        {
            return await _context.orders.CountAsync(o => o.Status == orderstaues.pending);
        }

        public async Task<List<(string Hour, decimal Total)>> GetSalesTrendAsync()
        {
            var today = DateTime.Today;

            return await _context.orders
                .Where(o => o.CreatedAt.Date == today)
                .GroupBy(o => o.CreatedAt.Hour)
                .Select(g => new ValueTuple<string, decimal>($"{g.Key}:00", g.Sum(o => o.Total)))
                .OrderBy(g => g.Item1)
                .ToListAsync();
        }

        public async Task<List<(string Hour, int Count)>> GetPeakHoursAsync()
        {
            var today = DateTime.Today;

            return await _context.orders
                .Where(o => o.CreatedAt.Date == today)
                .GroupBy(o => o.CreatedAt.Hour)
                .Select(g => new ValueTuple<string, int>($"{g.Key}:00", g.Count()))
                .OrderBy(g => g.Item1)
                .ToListAsync();
        }
    }

}
