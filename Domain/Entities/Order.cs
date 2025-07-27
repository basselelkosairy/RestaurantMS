namespace Resturant_System.Models
{
    public enum orderkind
    {
        Dinein,

        takeout,

        delivery
    }


    public enum orderstaues
    {
        pending,

        preparing,

        ready,

        declined

    }
    public class Order
    {
        public int Id { get; set; }
        public orderkind Type { get; set; }
        public orderstaues Status { get; set; } = orderstaues.pending;
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public string DeliveryAddress { get; set; }
        public ICollection<OrderItems> Items { get; set; }
        public int PreparingAt { get; set; }


        public decimal Total{ get; set; }

        public decimal CalculateDiscount(decimal subtotal)
        {
            decimal discount = 0;
            if (subtotal > 100)
                discount += subtotal * 0.1m;

            if (CreatedAt.Hour >= 15 && CreatedAt.Hour < 17)
                discount += subtotal * 0.2m;

            return discount;
        }



    }
}
