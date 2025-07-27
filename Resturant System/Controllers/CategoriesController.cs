using Application.Services.Abstractions;
using Microsoft.AspNetCore.Mvc;
using Resturant_System.Models;

public class CategoriesController : Controller
{
    private readonly ICategoryService _categoryService;

    public CategoriesController(ICategoryService categoryService)
    {
        _categoryService = categoryService;
    }

    public async Task<IActionResult> Index()
    {
        var categories = await _categoryService.getallassyunc();
        return View(categories);
    }

    public async Task<IActionResult> Details(int? id)
    {
        if (id == null) return NotFound();

        var category = await _categoryService.GetByIdAsync(id.Value);
        if (category == null) return NotFound();

        return View(category);
    }

    public IActionResult Create() => View();

    [HttpPost]
    public async Task<IActionResult> Create(Category category)
    {
        await _categoryService.AddAsync(category);
        return RedirectToAction(nameof(Index));
    }

    public async Task<IActionResult> Edit(int? id)
    {
        if (id == null) return NotFound();

        var category = await _categoryService.GetByIdAsync(id.Value);
        if (category == null) return NotFound();

        return View(category);
    }

    [HttpPost]
    public async Task<IActionResult> Save(Category category)
    {
        await _categoryService.UpdateAsync(category);
        return RedirectToAction(nameof(Index));
    }

    public async Task<IActionResult> Delete(int? id)
    {
        if (id == null) return NotFound();

        var category = await _categoryService.GetByIdAsync(id.Value);
        if (category == null) return NotFound();

        return View(category);
    }

    [HttpPost, ActionName("Delete")]
    public async Task<IActionResult> DeleteConfirmed(int id)
    {
        await _categoryService.DeleteAsync(id);
        return RedirectToAction(nameof(Index));
    }
}
