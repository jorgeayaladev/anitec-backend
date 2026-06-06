using Titan.AniTec.Platform.Inventory.Domain.Model.Aggregates;
using Microsoft.EntityFrameworkCore;

namespace Titan.AniTec.Platform.Inventory.Infrastructure.Persistence.EntityFrameworkCore.Configuration.Extensions;

public static class InventoryModelBuilderExtensions
{
    public static void ApplyInventoryConfiguration(this ModelBuilder builder)
    {
        builder.Entity<Product>(entity =>
        {
            entity.HasKey(p => p.Id);
            entity.Property(p => p.Id).IsRequired().ValueGeneratedOnAdd();
            entity.Property(p => p.FarmId).IsRequired();
            entity.HasIndex(p => p.FarmId);
            entity.Property(p => p.Name).IsRequired().HasMaxLength(200);
            entity.Property(p => p.Category).IsRequired().HasMaxLength(100);
            entity.Property(p => p.Unit).IsRequired().HasMaxLength(50);
            entity.Property(p => p.Sku).HasMaxLength(100);
            entity.Property(p => p.Description).HasMaxLength(2000);
        });

        builder.Entity<InventoryItem>(entity =>
        {
            entity.HasKey(i => i.Id);
            entity.Property(i => i.Id).IsRequired().ValueGeneratedOnAdd();
            entity.Property(i => i.FarmId).IsRequired();
            entity.HasIndex(i => i.FarmId);
            entity.Property(i => i.ProductId).IsRequired();
            entity.HasIndex(i => i.ProductId).IsUnique();
            entity.Property(i => i.CurrentStock);
            entity.Property(i => i.MinimumStock);
            entity.Property(i => i.Location).HasMaxLength(200);
        });

        builder.Entity<StockMovement>(entity =>
        {
            entity.HasKey(m => m.Id);
            entity.Property(m => m.Id).IsRequired().ValueGeneratedOnAdd();
            entity.Property(m => m.FarmId).IsRequired();
            entity.HasIndex(m => m.FarmId);
            entity.Property(m => m.InventoryItemId).IsRequired();
            entity.HasIndex(m => m.InventoryItemId);
            entity.Property(m => m.MovementType).IsRequired().HasMaxLength(50);
            entity.Property(m => m.UnitPrice).HasColumnType("decimal(18,2)");
            entity.Property(m => m.Reference).HasMaxLength(200);
            entity.Property(m => m.Notes).HasMaxLength(2000);
        });

        builder.Entity<Supplier>(entity =>
        {
            entity.HasKey(s => s.Id);
            entity.Property(s => s.Id).IsRequired().ValueGeneratedOnAdd();
            entity.Property(s => s.FarmId).IsRequired();
            entity.HasIndex(s => s.FarmId);
            entity.Property(s => s.Name).IsRequired().HasMaxLength(200);
            entity.Property(s => s.ContactPerson).HasMaxLength(200);
            entity.Property(s => s.Phone).HasMaxLength(50);
            entity.Property(s => s.Email).HasMaxLength(200);
            entity.Property(s => s.Address).HasMaxLength(500);
            entity.Property(s => s.Notes).HasMaxLength(2000);
        });
    }
}
