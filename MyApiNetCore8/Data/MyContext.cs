using Microsoft.EntityFrameworkCore;
using MyApiNetCore8.Model;
using System.Reflection.Metadata;

namespace MyApiNetCore8.Data
{
    public class MyContext : DbContext
    {
        public MyContext(DbContextOptions<MyContext> options) : base(options)
        {
        }

        #region DbSet
        public DbSet<Product> Product { get; set; }
        public DbSet<Category> Category { get; set; }
        public DbSet<Rating> Rating { get; set; }

        public DbSet<User> User { get; set; }
        public DbSet<UserRole> UserRole { get; set; }
        public DbSet<Role> Role { get; set; }
        public DbSet<Order> Order { get; set; }
        public DbSet<OrderItem> OrderItem { get; set; }
        public DbSet<Coupon> Coupons { get; set; }
        #endregion
     
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {

            if (!optionsBuilder.IsConfigured)
            {
                var connectionString = "Server=localhost; database=dotnetStore;user=root;password=;AllowZeroDateTime=True;";
                optionsBuilder.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString));
            }
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            foreach (var entityType in modelBuilder.Model.GetEntityTypes())
            {
                if (typeof(BaseEntity).IsAssignableFrom(entityType.ClrType))
                {
                    modelBuilder.Entity(entityType.ClrType)
                        .Property("CreatedDate")
                        .HasDefaultValueSql("SYSDATE()");

                    modelBuilder.Entity(entityType.ClrType)
                        .Property("ModifiedDate")
                        .HasDefaultValueSql("SYSDATE()");
                }
            }
        }

    }
}
