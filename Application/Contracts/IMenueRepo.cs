using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Resturant_System.Models;

namespace Application.Contracts
{
    public interface IMenueRepo
    {

        Task<List<Menue>> getallitemsasync();

        Task  AddItem(Menue menue);
        Task Savechanges();

        void Deleteitem(Menue menue);

        Task<Menue> finditem(int Id);

       void UpdateItem(Menue menue);
    }
}
