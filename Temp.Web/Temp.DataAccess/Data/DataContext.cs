using Microsoft.EntityFrameworkCore;

namespace Temp.DataAccess.Data
{    
    /// <summary>
    /// DbContext
    /// </summary>
    public class DataContext : DbContext
    {
        public DbSet<Category> Categories { get; set; }
        
        public DbSet<Product> Products { get; set; }

        public DbSet<Role> Roles { get; set; }

        public DbSet<User> Users { get; set; }

        public DbSet<CartDetail> CartDetails { get; set; }

        public DbSet<Comment> Comments { get; set; }

        public DbSet<Image> Images { get; set; }

        public DbSet<Nsx> Nsxs { get; set; }

        public DbSet<Sale> Sales { get; set; }

        public DbSet<Customer> Customers { get; set; }

        public DataContext(DbContextOptions<DataContext> options) : base(options) {}
    }
}