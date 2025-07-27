using Application.Services.Abstractions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Resturant_System.Data;
using Resturant_System.Models;

namespace Resturant_System.Controllers
{

    public class MenueController : Controller
    {
        private readonly ResturantDbcontext dbcontext;


        private readonly ICategoryService _categoryService;

        private readonly ImenueService _service;
        public MenueController(ImenueService imenueService , ICategoryService categoryService) { 
         
            _service = imenueService;
            _categoryService = categoryService;
        }

        public IActionResult Index()
        {
            return View();
        }
 
        public async Task<IActionResult> ViewItems()
        {
            var menueitems = await _service.getallasync();
            var categories = await _categoryService.getallassyunc();
            ViewBag.Categories = categories.Select(c => new {
                Value = c.Id.ToString(),
                Text = c.Name
            });

            return View(menueitems);

        }

        [HttpPost]
        public async Task<IActionResult> Add(Menue menue, IFormFile ImageFile)
        {
            if (ImageFile != null && ImageFile.Length > 0)
            {
                var extension = Path.GetExtension(ImageFile.FileName).ToLower();
                var allowedExtensions = new[] { ".jpg", ".jpeg", ".png", ".gif" };

                if (!allowedExtensions.Contains(extension))
                {
                    TempData["Error"] = "Only image files (.jpg, .jpeg, .png, .gif) are allowed.";
                    return RedirectToAction("ViewItems");
                }

                var fileName = $"{Guid.NewGuid()}{extension}";

                var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/images", fileName);

                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    await ImageFile.CopyToAsync(stream);
                }

                menue.Image = $"/images/{fileName}";
            }

            await _service.addItem(menue);
            return RedirectToAction("ViewItems");
        }

        public async Task<IActionResult> AddItem()
        {
            MenueCatViewModel menueCatViewModel =  new()
            {
                Menue = new Menue(),
                categories = await _categoryService.getallassyunc()
            };

          return View("MenueAdd", menueCatViewModel);
        }

        [HttpPost]
        public async Task<IActionResult> Update(Menue item, IFormFile? ImageFile)
        {
            var updateItem = await _service.finditem(item.Id);

            if (updateItem == null)
            {
                TempData["Error"] = "Item not found.";
                return RedirectToAction("ViewItems");
            }

            updateItem.Name = item.Name;
            updateItem.Price = item.Price;
            updateItem.Description = item.Description;
            updateItem.cateogryId = item.cateogryId;
            updateItem.quantity = item.quantity;

            if (ImageFile != null && ImageFile.Length > 0)
            {
                var fileName = Guid.NewGuid().ToString() + Path.GetExtension(ImageFile.FileName);
                var imagePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/images", fileName);

                using (var stream = new FileStream(imagePath, FileMode.Create))
                {
                    await ImageFile.CopyToAsync(stream);
                }

                updateItem.Image = "/images/" + fileName;
            }

            await _service.UpdateMenueItem(updateItem);
            TempData["Success"] = "Item updated successfully.";
            return RedirectToAction("ViewItems");
        }


        public async Task< IActionResult> Delete(int id)
        { 
             var DeletedItem = await _service.RemoveItem(new Menue{Id =id});
            if (DeletedItem == false)
            {
                TempData["Error"] = "item not found.";
                return RedirectToAction("ViewItems");
            }

           

            TempData["Success"] = "Item deleted successfully.";
            return RedirectToAction("ViewItems");
        }

    }
}
