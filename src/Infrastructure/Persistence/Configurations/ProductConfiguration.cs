using Domin.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Persistence.Configurations;

public class ProductConfiguration : IEntityTypeConfiguration<Product>
{
    public void Configure(EntityTypeBuilder<Product> builder)
    {
        builder.ToTable("Product");

        // تنظیم کلید اصلی
        builder.HasKey(u => u.Id);

        builder.Property(u => u.Name)
            .IsRequired() 
            .HasMaxLength(100); 

        builder.Property(u => u.Model)
            .IsRequired()
            .HasMaxLength(100); 

        builder.Property(u => u.MadeIn)
            .IsRequired() 
            .HasMaxLength(100);

        builder.Property(u => u.Price)
            .IsRequired() 
            .HasMaxLength(100);
        builder.Property(u => u.Color)
               .IsRequired() 
               .HasMaxLength(100); 
       
    }
}

