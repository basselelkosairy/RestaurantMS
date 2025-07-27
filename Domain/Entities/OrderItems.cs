using System.ComponentModel.DataAnnotations.Schema;

namespace Resturant_System.Models
{
    public class OrderItems
    {
        public int Id { get; set; }

        [ForeignKey("Menue")]
        public int MenuItemId { get; set; }
        public Menue MenuItem { get; set; }
        public int Quantity { get; set; }
        public decimal Subtotal { get; set; }


    }


}





