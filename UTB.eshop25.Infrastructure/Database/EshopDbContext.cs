using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UTB.eshop25.Domain.Entities;
using UTB.eshop25.Infrastructure.Database.Seeding;

namespace UTB.eshop25.Infrastructure.Database
{
    public class EshopDbContext : DbContext
    {
        public DbSet<Product> Products { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderItem> OrderItems { get; set; }
        public DbSet<User> Users { get; set; }



        public EshopDbContext(DbContextOptions<EshopDbContext> dbContextOptions)
            : base(dbContextOptions)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            var prodInit = new ProductInit();
            modelBuilder.Entity<Product>()
                        .HasData(prodInit.GenerateProducts3());

            modelBuilder.Entity<Order>()
                        .HasMany(o => o.OrderItems)
                        .WithOne(oi => oi.Order)
                        .HasForeignKey(oi => oi.OrderID)
                        .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<OrderItem>()
                        .HasOne(oi => oi.Product)
                        .WithMany()
                        .HasForeignKey(oi => oi.ProductID);

            modelBuilder.Entity<Order>()
                        .HasOne(o => o.User)
                        .WithMany()
                        .HasForeignKey(o => o.UserId);


        }
    }
}
