using E_Commerce.Entities.Models;
using Microsoft.EntityFrameworkCore;


namespace E_Commerce.DataAccess.Configurations
{
    class ProductConfiguration : IEntityTypeConfiguration<Product>
    {
        public void Configure(Microsoft.EntityFrameworkCore.Metadata.Builders.EntityTypeBuilder<Product> builder)
        {
            builder.Property(e => e.Price).HasColumnType("decimal(18,2)");
            builder.HasOne(e => e.Category).WithMany(e => e.Products).HasForeignKey(e => e.CategoryId);
        }
    }
}
