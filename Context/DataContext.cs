using ASP.NET.Models;
using Microsoft.EntityFrameworkCore;

namespace ASP.NET.Context
{
    public class DataContext : DbContext
    {
        public DataContext (DbContextOptions<DataContext> options ) : base(options)
        {

        }
        public DbSet<User> Users {get; set;}
        public DbSet<Product> Products { get; set; }
        public DbSet<Cart> Carts { get; set; }
    }
}
