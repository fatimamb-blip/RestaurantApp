using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Restaurant.Core.Models;

namespace Restaurant.DAL.Configurations;

public class OrderItemConfiguration : IEntityTypeConfiguration<OrderItem>
{
    public void Configure(EntityTypeBuilder<OrderItem> builder)
    {
        builder.HasKey(x => x.Id);
        builder.Property(x => x.Count).IsRequired();

     
        builder.HasOne(oi => oi.MenuItem)
               .WithMany(mi => mi.OrderItems)  
               .HasForeignKey(oi => oi.MenuItemId)
               .OnDelete(DeleteBehavior.Restrict);

       
        builder.HasOne(oi => oi.Order)
               .WithMany(o => o.OrderItems)   
               .HasForeignKey(oi => oi.OrderId)
               .OnDelete(DeleteBehavior.Cascade);
    }
}
