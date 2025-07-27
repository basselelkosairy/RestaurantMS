using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.EntityFrameworkCore;
using Resturant_System.Data;

namespace Infrastructure.Data
{
    public class ResturantDbContextFactory : IDesignTimeDbContextFactory<ResturantDbcontext>
    {
        public ResturantDbcontext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<ResturantDbcontext>();

            optionsBuilder.UseSqlServer("Data Source=DESKTOP-2FODDU6\\SQLEXPRESS;Initial Catalog=RestaurantSystem;Integrated Security=True;TrustServerCertificate=True;");

            return new ResturantDbcontext(optionsBuilder.Options);
        }
    }
}
