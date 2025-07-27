using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class Reservation
    {
        public int Id { get; set; }

        public string CustomerName { get; set; }

        public DateTime ReservationTime { get; set; }

        public int TableId { get; set; }
        
        public Table Table { get; set; }

        public string Notes { get; set; }
    }

}
