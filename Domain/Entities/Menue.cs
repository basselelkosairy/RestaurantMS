using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Resturant_System.Models
{
    public class Menue
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Name is required")]
        [StringLength(100, ErrorMessage = "Name cannot exceed 100 characters")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Price is required")]
        [Range(0.01, double.MaxValue, ErrorMessage = "Price must be positive")]
        public decimal Price { get; set; }

        [StringLength(500, ErrorMessage = "Description cannot exceed 500 characters")]
        public string? Description { get; set; }

        [Range(0, 10000, ErrorMessage = "Orders per day must be between 0 and 10000")]
        public int orderspredday { get; set; }

        public bool isavailable { get; set; } = true;

        [Required(ErrorMessage = "Quantity is required")]
        [Range(0, 10000, ErrorMessage = "Quantity must be between 0 and 10000")]
        public int quantity { get; set; }


        [Display(Name = "Preparation Time (minutes)")]
        [Range(1, 60, ErrorMessage = "Please enter a value between 1 and 60.")]
        public int PreparationTimeInMinutes { get; set; }

        [ForeignKey("Category")]
        [Required(ErrorMessage = "Category is required")]
        public int cateogryId { get; set; }

        public Category Category { get; set; }

        public string? Image { get; set; }
    }
}
