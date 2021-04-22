using Marcelena_web.Domain.Entitites;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Marcelena_web.Data
{
    public class ShopContext : IdentityDbContext<User>
    {
        public ShopContext(DbContextOptions<ShopContext> options) : base(options)
        {

        }

        public DbSet<Shop> Shops { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Address> Addresses { get; set; }
        public DbSet<Coordinate> Coordinates { get; set; }
        public DbSet<Photo> Photos { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Shop> Favorites { get; set; }
        public DbSet<Review> Reviews { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Shop>().HasMany(c => c.Products).WithOne();
            modelBuilder.Entity<Shop>().HasMany(c => c.PhotoPaths).WithOne();
            modelBuilder.Entity<Shop>().HasMany(c => c.Reviews).WithOne();
            modelBuilder.Entity<Shop>().ToTable("Shop");
            modelBuilder.Entity<Product>().ToTable("Product");
            modelBuilder.Entity<Photo>().ToTable("PhotoPath");
            modelBuilder.Entity<Address>().ToTable("Address");
            modelBuilder.Entity<Coordinate>().ToTable("Coordinate");
            modelBuilder.Entity<User>().ToTable("AspNetUsers");
            modelBuilder.Entity<User>().HasMany(c => c.Favorites).WithMany(s => s.Users);
            modelBuilder.Entity<Review>().ToTable("Review");
        }
    }
}
