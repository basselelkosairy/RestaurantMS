using System.Reflection;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Resturant_System.Models;

namespace Resturant_System.Data
{
    public class ResturantDbcontext : DbContext
    {

        public DbSet<User> Users { get; set; }

        public ResturantDbcontext(DbContextOptions<ResturantDbcontext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            // Optional: Seed tables
            modelBuilder.Entity<Table>().HasData(
                new Table { Id = 1, TableNumber = 1, Capacity = 4 },
                new Table { Id = 2, TableNumber = 2, Capacity = 2 },
                new Table { Id = 3, TableNumber = 3, Capacity = 6 }
            );
        }
        public DbSet<Table> Tables { get; set; }
        public DbSet<Reservation> Reservations { get; set; }

        public DbSet<Menue> MenueItems { get; set; } = default!;
        public DbSet<Category> Categories { get; set; }

        public DbSet<Order> orders { get; set; }

        public DbSet<OrderItems> OrderItems { get; set; }
    }
}
