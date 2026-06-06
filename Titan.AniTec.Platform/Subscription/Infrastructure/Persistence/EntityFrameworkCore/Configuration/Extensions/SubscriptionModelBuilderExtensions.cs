using Microsoft.EntityFrameworkCore;
using Titan.AniTec.Platform.Subscription.Domain.Model.Aggregates;

namespace Titan.AniTec.Platform.Subscription.Infrastructure.Persistence.EntityFrameworkCore.Configuration.Extensions;

public static class SubscriptionModelBuilderExtensions
{
    public static void ApplySubscriptionConfiguration(this ModelBuilder builder)
    {
        builder.Entity<SubscriptionPlan>(entity =>
        {
            entity.HasKey(p => p.Id);
            entity.Property(p => p.Id).IsRequired().ValueGeneratedOnAdd();
            entity.Property(p => p.Name).IsRequired().HasMaxLength(200);
            entity.HasIndex(p => p.Name).IsUnique();
            entity.Property(p => p.Description).HasMaxLength(2000);
            entity.Property(p => p.Price).HasColumnType("decimal(18,2)");
            entity.Property(p => p.Currency).IsRequired().HasMaxLength(3);
            entity.Property(p => p.BillingCycle).IsRequired().HasMaxLength(50);
            entity.Property(p => p.Features).HasMaxLength(4000);
        });

        builder.Entity<UserSubscription>(entity =>
        {
            entity.HasKey(s => s.Id);
            entity.Property(s => s.Id).IsRequired().ValueGeneratedOnAdd();
            entity.Property(s => s.FarmId).IsRequired();
            entity.HasIndex(s => s.FarmId);
            entity.Property(s => s.PlanId).IsRequired();
            entity.Property(s => s.Status).IsRequired().HasMaxLength(50);
            entity.HasIndex(s => s.Status);
            entity.Property(s => s.StripeSubscriptionId).HasMaxLength(500);
        });

        builder.Entity<Invoice>(entity =>
        {
            entity.HasKey(i => i.Id);
            entity.Property(i => i.Id).IsRequired().ValueGeneratedOnAdd();
            entity.Property(i => i.UserSubscriptionId).IsRequired();
            entity.Property(i => i.FarmId).IsRequired();
            entity.HasIndex(i => i.FarmId);
            entity.Property(i => i.Amount).HasColumnType("decimal(18,2)");
            entity.Property(i => i.Currency).IsRequired().HasMaxLength(3);
            entity.Property(i => i.Status).IsRequired().HasMaxLength(50);
            entity.Property(i => i.StripeInvoiceId).HasMaxLength(500);
        });

        builder.Entity<PaymentMethod>(entity =>
        {
            entity.HasKey(m => m.Id);
            entity.Property(m => m.Id).IsRequired().ValueGeneratedOnAdd();
            entity.Property(m => m.FarmId).IsRequired();
            entity.HasIndex(m => m.FarmId);
            entity.Property(m => m.StripePaymentMethodId).IsRequired().HasMaxLength(500);
            entity.Property(m => m.CardBrand).IsRequired().HasMaxLength(100);
            entity.Property(m => m.Last4).IsRequired().HasMaxLength(4);
        });

        builder.Entity<Payment>(entity =>
        {
            entity.HasKey(p => p.Id);
            entity.Property(p => p.Id).IsRequired().ValueGeneratedOnAdd();
            entity.Property(p => p.InvoiceId).IsRequired();
            entity.Property(p => p.FarmId).IsRequired();
            entity.HasIndex(p => p.FarmId);
            entity.Property(p => p.Amount).HasColumnType("decimal(18,2)");
            entity.Property(p => p.Currency).IsRequired().HasMaxLength(3);
            entity.Property(p => p.Status).IsRequired().HasMaxLength(50);
            entity.Property(p => p.StripePaymentIntentId).HasMaxLength(500);
        });
    }
}
