using Domain.Entities;
using System;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Resturant_System.Data;

namespace Resturant_System.Controllers
{
    public class ReservationController : Controller
    {

        private readonly ResturantDbcontext _context;

        public ReservationController(ResturantDbcontext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult Create()
        {
            ViewBag.Tables = new SelectList(_context.Tables.Where(t => t.IsAvailable), "Id", "TableNumber");
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(Reservation reservation)
        {
            if (ModelState.IsValid)
            {
                var table = await _context.Tables.FindAsync(reservation.TableId);
                if (table != null)
                {
                    reservation.Table = table;
                    table.IsAvailable = false; 
                    _context.Add(reservation);
                    await _context.SaveChangesAsync();
                    TempData["Success"] = "Reservation successful!";
                    return RedirectToAction("Index", "Home");
                }
            }
            ViewBag.Tables = new SelectList(_context.Tables.Where(t => t.IsAvailable), "Id", "TableNumber");
            TempData["Error"] = "Reservation failed.";
            return View(reservation);
        }
    }
}
