using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Talabat.Core.Entities;
using Talabat.Core.Entities.Product_Module;

namespace Talabat.Repository.Data.Configration
{
    public class ProductConfigration : IEntityTypeConfiguration<Product>
    {
        public void Configure(Microsoft.EntityFrameworkCore.Metadata.Builders.EntityTypeBuilder<Product> builder)
        {
            builder.HasOne(p=>p.ProductBrand).WithMany().HasForeignKey(p=>p.ProductBrandId);
            builder.HasOne(p => p.ProductCategory).WithMany().HasForeignKey(p => p.ProductTypeId);


            builder.Property(p => p.PictureUrl).IsRequired();
            builder.Property(p=>p.Price).HasColumnType("decimal(18,2)"); // to specify the type of the price column in the database
            builder.Property(p=>p.Name).IsRequired().HasMaxLength(50);
            builder.Property(p => p.Description).IsRequired();

        }
    }
}
