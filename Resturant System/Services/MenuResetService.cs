using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Resturant_System.Data;

public class MenuResetService : BackgroundService
{
    private readonly IServiceProvider _serviceProvider;

    public MenuResetService(IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        while (!stoppingToken.IsCancellationRequested)
        {
            var now = DateTime.Now;
            var midnight = DateTime.Today.AddDays(1);
            var delay = midnight - now;

            await Task.Delay(delay, stoppingToken);

            using (var scope = _serviceProvider.CreateScope())
            {
                var db = scope.ServiceProvider.GetRequiredService<ResturantDbcontext>();

                var items = db.MenueItems.ToList();

                foreach (var item in items)
                {
                    item.orderspredday = 0;
                    item.isavailable = true;
                }

                await db.SaveChangesAsync();
            }
        }
    }
}
