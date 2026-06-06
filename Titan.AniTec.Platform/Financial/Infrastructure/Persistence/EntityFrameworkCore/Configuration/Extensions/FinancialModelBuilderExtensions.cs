using Titan.AniTec.Platform.Financial.Domain.Model.Aggregates;
using Microsoft.EntityFrameworkCore;

namespace Titan.AniTec.Platform.Financial.Infrastructure.Persistence.EntityFrameworkCore.Configuration.Extensions;

public static class FinancialModelBuilderExtensions
{
    public static void ApplyFinancialConfiguration(this ModelBuilder builder)
    {
        builder.Entity<FinancialCategory>(entity =>
        {
            entity.HasKey(c => c.Id);
            entity.Property(c => c.Id).IsRequired().ValueGeneratedOnAdd();
            entity.Property(c => c.FarmId).IsRequired();
            entity.HasIndex(c => c.FarmId);
            entity.Property(c => c.Name).IsRequired().HasMaxLength(100);
            entity.Property(c => c.Type).IsRequired().HasMaxLength(20);
            entity.Property(c => c.Description).HasMaxLength(500);
            entity.HasIndex(c => new { c.FarmId, c.Name }).IsUnique();
            entity.HasIndex(c => new { c.FarmId, c.Type });
        });

        builder.Entity<Transaction>(entity =>
        {
            entity.HasKey(t => t.Id);
            entity.Property(t => t.Id).IsRequired().ValueGeneratedOnAdd();
            entity.Property(t => t.FarmId).IsRequired();
            entity.HasIndex(t => t.FarmId);
            entity.Property(t => t.TransactionDate).IsRequired();
            entity.Property(t => t.Type).IsRequired().HasMaxLength(20);
            entity.Property(t => t.Category).IsRequired().HasMaxLength(50);
            entity.Property(t => t.Description).IsRequired().HasMaxLength(500);
            entity.Property(t => t.Amount).IsRequired().HasColumnType("decimal(18,2)");
            entity.Property(t => t.PaymentMethod).HasMaxLength(50);
            entity.Property(t => t.Reference).HasMaxLength(200);
            entity.Property(t => t.Notes).HasMaxLength(2000);
            entity.HasIndex(t => new { t.FarmId, t.Type });
            entity.HasIndex(t => new { t.FarmId, t.Category });
        });

        builder.Entity<Budget>(entity =>
        {
            entity.HasKey(b => b.Id);
            entity.Property(b => b.Id).IsRequired().ValueGeneratedOnAdd();
            entity.Property(b => b.FarmId).IsRequired();
            entity.HasIndex(b => b.FarmId);
            entity.Property(b => b.Category).IsRequired().HasMaxLength(50);
            entity.Property(b => b.BudgetType).IsRequired().HasMaxLength(20);
            entity.Property(b => b.PlannedAmount).IsRequired().HasColumnType("decimal(18,2)");
            entity.Property(b => b.Notes).HasMaxLength(2000);
            entity.HasIndex(b => new { b.FarmId, b.Year, b.Month, b.Category, b.BudgetType }).IsUnique();
        });
    }
}
