//using Microsoft.EntityFrameworkCore;
//using Resturant_System.Data;
//using Resturant_System.Models;
//using System;

//public class OrderStatusUpdaterService : BackgroundService
//{
//    private readonly IServiceScopeFactory _scopeFactory;
//    private readonly TimeSpan _checkInterval = TimeSpan.FromMinutes(1);

//    public OrderStatusUpdaterService(IServiceScopeFactory scopeFactory)
//    {
//        _scopeFactory = scopeFactory;
//    }

//    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
//    {
//        while (!stoppingToken.IsCancellationRequested)
//        {
//            using var scope = _scopeFactory.CreateScope();
//            var context = scope.ServiceProvider.GetRequiredService<ResturantDbcontext>();

//            var now = DateTime.Now;

//            // PENDING → PREPARING after 5 minutes
//            var pendingOrders = await context.orders
//                .Where(o => o.Status == orderstaues.pending && EF.Functions.DateDiffMinute(o.CreatedAt, now) >= 5)
//                .ToListAsync(stoppingToken);

//            foreach (var order in pendingOrders)
//            {
//                order.Status = orderstaues.preparing;
//                order.PreparingAt = now;
//            }

//            var preparingOrders = await context.orders
//                .Include(o => o.Items).ThenInclude(i => i.MenuItem)
//                .Where(o => o.Status == orderstaues.preparing && o.PreparingAt != null)
//                .ToListAsync(stoppingToken);

//            foreach (var order in preparingOrders)
//            {
//                var totalPrepTime = order.Items.Sum(i => i.MenuItem.PreparationTimeInMinutes);
//                if ((now - order.PreparingAt.Value).TotalMinutes >= totalPrepTime)
//                {
//                    order.Status = orderstaues.ready;
//                }
//            }

//            await context.SaveChangesAsync(stoppingToken);
//            await Task.Delay(_checkInterval, stoppingToken);
//        }
//    }
//}
