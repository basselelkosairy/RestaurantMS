using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services.Abstractions
{
    public interface IDashboardService
    {
        Task<decimal> GetTodayRevenueAsync();
        Task<int> GetItemsToReorderCountAsync();
        Task<int> GetPendingKOTCountAsync();
        Task<List<(string Hour, decimal Total)>> GetSalesTrendAsync();
        Task<List<(string Hour, int Count)>> GetPeakHoursAsync();
    }

}
