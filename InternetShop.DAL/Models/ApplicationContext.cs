using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DAL.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace InternetShop.DAL.Models
{
    public class ApplicationContext : IdentityDbContext<User>
    {
        public DbSet<Category> Categories { get; set; }
        public DbSet<Product> Products { get; set; }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<Product>().HasOne(c => c.Category).WithMany(c => c.Products)
                .OnDelete(DeleteBehavior.ClientSetNull);
            builder.Entity<Category>().HasData(new Category[]
            {
                new Category {Name = "Home", Description = "products for your home", Id=1},
                new Category {Name = "Car", Description = "Products for your Car", Id=2},
                new Category {Name = "Garden", Description = "Products for your garden", Id=3}
            });
            builder.Entity<Product>().HasData(new Product[]
            {
                new Product{ Name = "wheel", CategoryId = 2, Price = 150, ProductId=1},
                new Product{ Name = "door", CategoryId = 1, Price = 450, ProductId=2},
                new Product{ Name = "window", CategoryId = 1, Price = 860, ProductId=3},
                new Product{ Name = "kitchen table", CategoryId = 1, Price = 1250, ProductId=4},
                new Product{ Name = "chair", CategoryId = 1, Price = 100, ProductId=5},
                new Product{ Name = "apple tree", CategoryId = 3, Price = 200, ProductId=6},
                new Product{ Name = "pruner", CategoryId = 3, Price = 70, ProductId=7},
                new Product{ Name = "bucket", CategoryId = 3, Price = 60, ProductId=8}
            });
            base.OnModelCreating(builder);
        }

        public ApplicationContext(DbContextOptions<ApplicationContext> options) : base(options)
        {
            Database.EnsureCreated();
        }

    }
}
