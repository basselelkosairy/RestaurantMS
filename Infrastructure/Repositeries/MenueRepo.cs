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
    public class MenueRepo : IMenueRepo
    {
        private readonly ResturantDbcontext _context;

        public MenueRepo(ResturantDbcontext resturantDbcontext)
        {
            _context = resturantDbcontext;
        }

        public async Task Savechanges()
        {

            await _context.SaveChangesAsync();
        }

        public async Task AddItem(Menue menue)
        {
            await _context.MenueItems.AddAsync(menue);
        
        }

        public async Task<List<Menue>> getallitemsasync()
        {
            return await _context.MenueItems.Include(c=>c.Category).ToListAsync();
        }

    

        public async Task<Menue> finditem(int Id)
        {
  
            var item= await _context.MenueItems.FirstOrDefaultAsync(I=>I.Id == Id);
            return item;
        }

        public void Deleteitem(Menue menue)
        {

            _context.MenueItems.Remove(menue);

        }

        public  void UpdateItem(Menue menue)
        {
            _context.MenueItems.Update(menue);
        }
    }
}
