using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Contracts;
using Application.Services.Abstractions;

namespace Application.Services.Implementations
{
    public class DashboardService : IDashboardService
    {
        private readonly IDashboardRepository _repo;

        public DashboardService(IDashboardRepository repo)
        {
            _repo = repo;
        }

        public async Task<decimal> GetTodayRevenueAsync()

        {return await _repo.GetTodayRevenueAsync(); }

        public async Task<int> GetItemsToReorderCountAsync() 
        
        { return await _repo.GetItemsToReorderCountAsync(); }

        public async Task<int> GetPendingKOTCountAsync() {
           return await _repo.GetPendingKOTCountAsync(); }

        public Task<List<(string Hour, decimal Total)>> GetSalesTrendAsync() => _repo.GetSalesTrendAsync();

        public Task<List<(string Hour, int Count)>> GetPeakHoursAsync() => _repo.GetPeakHoursAsync();
    }

}
