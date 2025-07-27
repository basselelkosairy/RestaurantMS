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
    public class CateogryRepo : ICateogryRepo
    {
        private readonly ResturantDbcontext _context;

        public CateogryRepo( ResturantDbcontext resturantDbcontext)
        {
            _context = resturantDbcontext;
        }
        public async Task<List<Category>> GetCategoriesAsync()
        {
            return await _context.Categories.ToListAsync();
        
        }
        public async Task<List<Category>> GetAllAsync() =>
            await _context.Categories.ToListAsync();

        public async Task<Category?> GetByIdAsync(int id) =>
            await _context.Categories.FirstOrDefaultAsync(c => c.Id == id);

        public async Task AddAsync(Category category)
        {
            _context.Categories.Add(category);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Category category)
        {
            _context.Categories.Update(category);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var category = await GetByIdAsync(id);
            if (category != null)
            {
                _context.Categories.Remove(category);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<bool> ExistsAsync(int id) =>
            await _context.Categories.AnyAsync(c => c.Id == id);
    }
}

