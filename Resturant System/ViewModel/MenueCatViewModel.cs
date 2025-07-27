namespace Resturant_System.Models
{
    public class MenueCatViewModel
    {
        public Menue Menue { get; set; }
        public IFormFile? ImageFile { get; set; } 

        public IEnumerable<Category> categories { get; set; }
    }
}
