using Entities.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Repositories.Config
{
    public class ProductConfig : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            builder.HasKey(p => p.ProductID);
            builder.Property(p => p.ProductName).IsRequired();
            builder.Property(p => p.Price).IsRequired();

            builder.HasData(
                new Product() { ProductID = 1, ImageUrl = "/images/1.jpg", CategoryID = 2, ProductName = "Computer", Price = 17_000,ShowCase=false },
                new Product() { ProductID = 2, ImageUrl = "/images/2.jpg", CategoryID = 2, ProductName = "Keyboard", Price = 1_000,ShowCase=false },
                new Product() { ProductID = 3, ImageUrl = "/images/3.jpg", CategoryID = 2, ProductName = "Mouse", Price = 500,ShowCase=false },
                new Product() { ProductID = 4, ImageUrl = "/images/4.jpg", CategoryID = 2, ProductName = "Monitor", Price = 7_000,ShowCase=false },
                new Product() { ProductID = 5, ImageUrl = "/images/5.jpg", CategoryID = 2, ProductName = "Deck", Price = 1_500,ShowCase=false },
                new Product() { ProductID = 6, ImageUrl = "/images/6.jpg", CategoryID = 1, ProductName = "History", Price = 25,ShowCase=false },
                new Product() { ProductID = 7, ImageUrl = "/images/7.jpg", CategoryID = 1, ProductName = "Hamlet", Price = 45,ShowCase=false },
                new Product() { ProductID = 8, ImageUrl = "/images/8.jpg", CategoryID = 1, ProductName = "Xp-Pen", Price = 1145,ShowCase=true },
                new Product() { ProductID = 9, ImageUrl = "/images/9.jpg", CategoryID = 2, ProductName = "Galaxy FE", Price = 4345,ShowCase=true },
                new Product() { ProductID = 10, ImageUrl = "/images/11.jpg", CategoryID = 1, ProductName = "Airpod", Price = 545,ShowCase=true }
            );
        }
    }
}