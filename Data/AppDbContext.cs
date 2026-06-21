using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using MyWebApp.Models; 

namespace MyWebApp.Data
{
    public class AppDbContext : IdentityDbContext<IdentityUser>
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<Shoe> Shoes { get; set; } = null!;
        public DbSet<Category> Categories { get; set; } = null!;
        public DbSet<Brand> Brands { get; set; } = null!;
        public DbSet<Order> Orders { get; set; } = null!;
        public DbSet<OrderItem> OrderItems { get; set; } = null!;

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.Entity<Shoe>().Property(s => s.Price).HasPrecision(18, 2);
            builder.Entity<OrderItem>().Property(oi => oi.PriceAtOrder).HasPrecision(18, 2);
            builder.Entity<OrderItem>().Property(oi => oi.UnitPrice).HasPrecision(18, 2);
        }
    }
}