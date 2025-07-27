using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Contracts;
using Application.Services.Abstractions;
using Resturant_System.Models;

namespace Application.Services.Implementations
{
    public class CategoryService : ICategoryService
    {
        private readonly ICateogryRepo _repository;

        public CategoryService(ICateogryRepo repository)
        {
            _repository = repository;
        }

        public async Task<List<Category>> getallassyunc() 
        {
            return await _repository.GetAllAsync();
                
          }

        public async Task<Category?> GetByIdAsync(int id)
        {
            return await _repository.GetByIdAsync(id);
        }

        public async Task AddAsync(Category category)
        {
            await _repository.AddAsync(category);
        }

        public async Task UpdateAsync(Category category) {
            await _repository.UpdateAsync(category); }

        public async Task DeleteAsync(int id) { 
            await _repository.DeleteAsync(id); }

        public async Task<bool> ExistsAsync(int id) {
           return await _repository.ExistsAsync(id); }
    }
}

