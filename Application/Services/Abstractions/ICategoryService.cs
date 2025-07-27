using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Resturant_System.Models;

namespace Application.Services.Abstractions
{
    public interface ICategoryService
    {

        Task<List<Category>> getallassyunc();
        Task<Category?> GetByIdAsync(int id);
        Task AddAsync(Category category);
        Task UpdateAsync(Category category);
        Task DeleteAsync(int id);
        Task<bool> ExistsAsync(int id);
    }
}
