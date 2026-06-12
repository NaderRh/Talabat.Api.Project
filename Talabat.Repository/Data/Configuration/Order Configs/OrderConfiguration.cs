using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Talabat.Core.Entities.Order_Module;

namespace Talabat.Repository.Data.Configuration.Order_Configs
{
    public class OrderConfiguration : IEntityTypeConfiguration<Order>
    {
        public void Configure(EntityTypeBuilder<Order> builder)
        {
            builder.OwnsOne(P => P.ShippingAddress, shippingAddress => shippingAddress.WithOwner());

            builder.Property(P => P.Status).
                HasConversion(
                OStatus => OStatus.ToString(),
                OStatus => (OrderStatus)Enum.Parse(typeof(OrderStatus), OStatus));

            builder.Property(P => P.SupTotal).HasColumnType("decimal(18,2)");
        }
    }
}
