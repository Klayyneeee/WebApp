using Microsoft.EntityFrameworkCore;
using WebApp.Domain.Models;

namespace WebApp.Infrastructure.Data
{
        public class DataContext : DbContext
        {
            public DataContext(DbContextOptions<DataContext> options) : base(options)
            {

            }   
            public DbSet<Category> Categories { get; set; }
            public DbSet<Products> Products { get; set; }
            public DbSet<ProductsCategory> ProductsCategories { get; set; }



            protected override void OnModelCreating(ModelBuilder modelBuilder)
            {
                modelBuilder.Entity<ProductsCategory>()
                    .HasKey(pc => new { pc.ProductId, pc.CategoryId });
                modelBuilder.Entity<ProductsCategory>()
                    .HasOne(p => p.Products)
                    .WithMany(pc => pc.ProductsCategories)
                    .HasForeignKey(p => p.ProductId);
                modelBuilder.Entity<ProductsCategory>()
                    .HasOne(p => p.Category)
                    .WithMany(pc => pc.ProductsCategories)
                    .HasForeignKey(c => c.CategoryId);


            }
        }
    }
