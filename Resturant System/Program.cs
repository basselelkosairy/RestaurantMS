using Application.Contracts;
using Application.Services.Abstractions;
using Application.Services.Implementations;
using Infrastructure.Repositeries;
using Microsoft.EntityFrameworkCore;
using Resturant_System.Data;
using Resturant_System.Middleware;

namespace Resturant_System
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddControllersWithViews();
            builder.Services.AddDbContext<ResturantDbcontext>(options =>
       options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));
            builder.Services.AddScoped<IOrderRepo, OrderRepo>();
            builder.Services.AddScoped<IOrderService, OrderService>();
            builder.Services.AddScoped<IMenueRepo, MenueRepo>();
            builder.Services.AddScoped<ImenueService,MenueService>();
            builder.Services.AddScoped<ICateogryRepo, CateogryRepo>();
            builder.Services.AddScoped<ICategoryService, CategoryService>();

            builder.Services.AddScoped<IDashboardRepository, DashboardRepository>();
            builder.Services.AddScoped<IDashboardService, DashboardService>();


            builder.Services.AddHostedService<MenuResetService>();

            var app = builder.Build();


           // app.UseMiddleware<BusinessHoursMiddleware>();

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();



            app.UseAuthorization();

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");

            app.Run();
        }
    }
}
