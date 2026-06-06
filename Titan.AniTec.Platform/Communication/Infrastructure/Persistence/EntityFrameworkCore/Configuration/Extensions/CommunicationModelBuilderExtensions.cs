using Titan.AniTec.Platform.Communication.Domain.Model.Aggregates;
using Microsoft.EntityFrameworkCore;

namespace Titan.AniTec.Platform.Communication.Infrastructure.Persistence.EntityFrameworkCore.Configuration.Extensions;

public static class CommunicationModelBuilderExtensions
{
    public static void ApplyCommunicationConfiguration(this ModelBuilder builder)
    {
        builder.Entity<Conversation>(entity =>
        {
            entity.HasKey(c => c.Id);
            entity.Property(c => c.Id).IsRequired().ValueGeneratedOnAdd();
            entity.Property(c => c.FarmId).IsRequired();
            entity.HasIndex(c => c.FarmId);
            entity.Property(c => c.Title).IsRequired().HasMaxLength(200);
            entity.Property(c => c.Participants).IsRequired().HasMaxLength(1000);
        });

        builder.Entity<Message>(entity =>
        {
            entity.HasKey(m => m.Id);
            entity.Property(m => m.Id).IsRequired().ValueGeneratedOnAdd();
            entity.Property(m => m.ConversationId).IsRequired();
            entity.HasIndex(m => m.ConversationId);
            entity.Property(m => m.SenderId).IsRequired();
            entity.Property(m => m.Content).IsRequired().HasMaxLength(5000);
        });

        builder.Entity<Contact>(entity =>
        {
            entity.HasKey(c => c.Id);
            entity.Property(c => c.Id).IsRequired().ValueGeneratedOnAdd();
            entity.Property(c => c.FarmId).IsRequired();
            entity.HasIndex(c => c.FarmId);
            entity.Property(c => c.ContactUserId).IsRequired();
            entity.Property(c => c.Name).IsRequired().HasMaxLength(200);
            entity.Property(c => c.Email).HasMaxLength(200);
            entity.Property(c => c.Phone).HasMaxLength(50);
            entity.Property(c => c.Role).IsRequired().HasMaxLength(50);
            entity.Property(c => c.Notes).HasMaxLength(2000);
        });

        builder.Entity<ContactRequest>(entity =>
        {
            entity.HasKey(r => r.Id);
            entity.Property(r => r.Id).IsRequired().ValueGeneratedOnAdd();
            entity.Property(r => r.FarmId).IsRequired();
            entity.Property(r => r.FromUserId).IsRequired();
            entity.Property(r => r.ToUserId).IsRequired();
            entity.Property(r => r.Status).IsRequired().HasMaxLength(20);
        });

        builder.Entity<SharedReport>(entity =>
        {
            entity.HasKey(r => r.Id);
            entity.Property(r => r.Id).IsRequired().ValueGeneratedOnAdd();
            entity.Property(r => r.ReportId).IsRequired();
            entity.Property(r => r.SharedById).IsRequired();
            entity.Property(r => r.SharedWithId).IsRequired();
        });

        builder.Entity<PushDevice>(entity =>
        {
            entity.HasKey(d => d.Id);
            entity.Property(d => d.Id).IsRequired().ValueGeneratedOnAdd();
            entity.Property(d => d.FarmId).IsRequired();
            entity.HasIndex(d => d.FarmId);
            entity.Property(d => d.DeviceToken).IsRequired().HasMaxLength(500);
            entity.Property(d => d.Platform).IsRequired().HasMaxLength(50);
        });

        builder.Entity<NotificationSetting>(entity =>
        {
            entity.HasKey(n => n.Id);
            entity.Property(n => n.Id).IsRequired().ValueGeneratedOnAdd();
            entity.Property(n => n.FarmId).IsRequired();
            entity.HasIndex(n => n.FarmId);
            entity.Property(n => n.Settings).IsRequired().HasMaxLength(4000);
        });
    }
}
