using erp_sql.Model;
using Microsoft.EntityFrameworkCore;


namespace erp_sql.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options)
        {
        }

        public DbSet<User> Users { get; set; }

        public DbSet<Model.Customer> Customers { get; set; }
       // public DbSet<Model.Customer.Customer> Customers { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Example: configure User table
            modelBuilder.Entity<User>(entity =>
            {
                entity.HasKey(u => u.Id);
                entity.Property(u => u.Name).IsRequired().HasMaxLength(100);
                // Add more configurations as needed
            });

            // Example: configure Customer table
            modelBuilder.Entity<Model.Customer>(entity =>
            {
                entity.HasKey(c => c.Custid);
                entity.Property(c => c.CustName).IsRequired().HasMaxLength(150);
            });
        }

    }
}
