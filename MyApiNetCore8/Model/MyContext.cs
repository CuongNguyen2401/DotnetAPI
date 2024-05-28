using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace MyApiNetCore8.Model
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
        public DbSet<RelatedProduct> RelatedProduct { get; set; }
        public DbSet<User> User { get; set; }
        public DbSet<UserRole> UserRole { get; set; }
        public DbSet<Role> Role { get; set; }

        #endregion
        //protected override void OnModelCreating(ModelBuilder modelBuilder)
        //{
        //    base.OnModelCreating(modelBuilder);



        //    // Configure the Category entity if needed
        //    // Add configurations for other entities as needed
        //}
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

            foreach (var entityType in modelBuilder.Model.GetEntityTypes())
            {
                if (typeof(BaseEntity).IsAssignableFrom(entityType.ClrType))
                {
                    modelBuilder.Entity(entityType.ClrType)
                        .Property("CreatedDate")
                        .HasDefaultValueSql("CURRENT_TIMESTAMP");

                    modelBuilder.Entity(entityType.ClrType)
                        .Property("ModifiedDate")
                        .HasDefaultValueSql("CURRENT_TIMESTAMP");
                }
            }

        }
    }
}
