using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Resturant_System.Models;

namespace Application.Services.Abstractions
{
    public interface ImenueService
    {
        Task<List<Menue>> getallasync();

        Task<bool> UpdateMenueItem(Menue menue);
        Task addItem(Menue menue);
        Task<bool> RemoveItem(Menue menue);
        Task<Menue> finditem(int Id);


    }
}
