using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Resturant_System.Models;

namespace Application.DTO
{
    public class OrderCreateDto
    {
        public DateTime CreatedAt { get; set; }
        public string? DeliveryAddress { get; set; }
        public orderkind Type { get; set; }
        public List<OrderItemDto> Items { get; set; } = new();
        public bool IsStudent { get; set; }
        public bool IsSenior { get; set; }
    }

}
