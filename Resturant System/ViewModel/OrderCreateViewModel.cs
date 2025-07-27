using Microsoft.AspNetCore.Mvc.Rendering;
using Resturant_System.Models;

namespace Resturant_System.ViewModel
{
    public class OrderCreateViewModel
    {

        public orderkind Type { get; set; }
        public orderstaues Status { get; set; } = orderstaues.pending;
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public string DeliveryAddress { get; set; }

        public List<OrderItemViewModel> OrderItems { get; set; } = new List<OrderItemViewModel>();

        public IEnumerable<SelectListItem> menuesitems { get; set; }




    }


}
