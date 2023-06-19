using Entities.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Repositories.Config
{
    public class CategoryConfig : IEntityTypeConfiguration<Category>
    {
        public void Configure(EntityTypeBuilder<Category> builder)
        {
            builder.HasKey(c => c.CategoryID);

            builder.Property(c => c.CategoryName).IsRequired();

            builder.HasData(
                new Category() { CategoryID = 1, CategoryName = "Book" },
                new Category() { CategoryID = 2, CategoryName = "Electronic" }
            );
        }
    }
}