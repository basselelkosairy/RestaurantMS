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
    public class MenueService : ImenueService
    {

        IMenueRepo  _repo;
        public MenueService(IMenueRepo menueRepo)
        {
            _repo = menueRepo;
        }
        public async Task<List<Menue>> getallasync()
        {
            return await _repo.getallitemsasync();
        }
        public async Task addItem(Menue menue)
        {
            await _repo.AddItem(menue);
            await _repo.Savechanges();
        }

        public async Task<Menue> finditem(int Id)
        {
            var findeditem = await _repo.finditem(Id);

                return findeditem;
           
        }

        public async Task<bool> UpdateMenueItem(Menue menue)
        {
         
                _repo.UpdateItem(menue);
                await _repo.Savechanges();
            return true;
      
           

        }

        public async Task <bool> RemoveItem (Menue menue)
        {

         var deleteditem= await _repo.finditem(menue.Id);

            if (deleteditem != null)
            {
                 _repo.Deleteitem(deleteditem);
                await _repo.Savechanges();
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
