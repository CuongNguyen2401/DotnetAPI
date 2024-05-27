using Microsoft.EntityFrameworkCore;

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
    }
}
