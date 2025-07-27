using System.Diagnostics;
using Application.Services.Abstractions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Resturant_System.Data;
using Resturant_System.Models;

namespace Resturant_System.Controllers
{
    public class HomeController : Controller
    {
        private readonly IDashboardService _dashboard;


        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger , IDashboardService dashboard)
        {
            _logger = logger;

       
  
            _dashboard = dashboard;
        }

        public async Task<IActionResult> Index()
        {
            ViewBag.TodayRevenue = await _dashboard.GetTodayRevenueAsync();
            ViewBag.ItemsToReorder = await _dashboard.GetItemsToReorderCountAsync();
            ViewBag.PendingKOT = await _dashboard.GetPendingKOTCountAsync();
            return View();
        }

        [HttpGet]
        public async Task<JsonResult> GetSalesTrend()
        {
            var data = await _dashboard.GetSalesTrendAsync();
            var json = data.Select(d => new { hour = d.Hour, total = d.Total });
            return Json(json);
        }

        [HttpGet]
        public async Task<JsonResult> GetPeakHours()
        {
            var data = await _dashboard.GetPeakHoursAsync();
            var json = data.Select(d => new { hour = d.Hour, count = d.Count });
            return Json(json);
        }


        //[HttpPost]
        //public IActionResult Check(User user)
        //{
          
                
        //    if (dbcontext.Users.Any(u => u.Name == user.Name && u.Password == user.Password))
        //    {
        //        return RedirectToAction("Privacy");
        //    }

        //    ModelState.AddModelError(string.Empty, "Invalid username or password");
        //    return View("login", user); 
        //}

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
