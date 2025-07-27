using System.ComponentModel.DataAnnotations;

namespace Resturant_System.Models
{
    public class User
    {

        [Key]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        public string Email { get; set; }

        [Required]
        public string Password { get; set; }    

        public roles roles { get; set; } = roles.Waiter;

    }

    public enum roles
    {
    Chef,
Waiter,
Manager
    }
}
