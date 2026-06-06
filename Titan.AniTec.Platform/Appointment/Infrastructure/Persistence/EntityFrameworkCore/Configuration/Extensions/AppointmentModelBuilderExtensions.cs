using Microsoft.EntityFrameworkCore;
using Titan.AniTec.Platform.Appointment.Domain.Model.Aggregates;

namespace Titan.AniTec.Platform.Appointment.Infrastructure.Persistence.EntityFrameworkCore.Configuration.Extensions;

public static class AppointmentModelBuilderExtensions
{
    public static void ApplyAppointmentConfiguration(this ModelBuilder builder)
    {
        builder.Entity<VeterinaryAppointment>(entity =>
        {
            entity.HasKey(a => a.Id);
            entity.Property(a => a.Id).IsRequired().ValueGeneratedOnAdd();
            entity.Property(a => a.FarmId).IsRequired();
            entity.HasIndex(a => a.FarmId);
            entity.Property(a => a.VeterinarianId).IsRequired();
            entity.HasIndex(a => a.VeterinarianId);
            entity.Property(a => a.FarmerId).IsRequired();
            entity.HasIndex(a => a.FarmerId);
            entity.Property(a => a.AnimalId);
            entity.HasIndex(a => a.AnimalId);
            entity.Property(a => a.AppointmentType).IsRequired().HasMaxLength(100);
            entity.Property(a => a.Status).IsRequired().HasMaxLength(50);
            entity.HasIndex(a => a.Status);
            entity.Property(a => a.ScheduledAt).IsRequired();
            entity.Property(a => a.Location).HasMaxLength(500);
            entity.Property(a => a.Notes).HasMaxLength(2000);
        });

        builder.Entity<AppointmentReminder>(entity =>
        {
            entity.HasKey(r => r.Id);
            entity.Property(r => r.Id).IsRequired().ValueGeneratedOnAdd();
            entity.Property(r => r.AppointmentId).IsRequired();
            entity.HasIndex(r => r.AppointmentId);
        });

        builder.Entity<AppointmentFollowUp>(entity =>
        {
            entity.HasKey(f => f.Id);
            entity.Property(f => f.Id).IsRequired().ValueGeneratedOnAdd();
            entity.Property(f => f.AppointmentId).IsRequired();
            entity.HasIndex(f => f.AppointmentId);
            entity.Property(f => f.Status).IsRequired().HasMaxLength(50);
            entity.Property(f => f.Notes).HasMaxLength(2000);
        });

        builder.Entity<AppointmentNote>(entity =>
        {
            entity.HasKey(n => n.Id);
            entity.Property(n => n.Id).IsRequired().ValueGeneratedOnAdd();
            entity.Property(n => n.AppointmentId).IsRequired();
            entity.HasIndex(n => n.AppointmentId);
            entity.Property(n => n.Content).IsRequired().HasMaxLength(4000);
        });

        builder.Entity<VeterinarianAvailability>(entity =>
        {
            entity.HasKey(v => v.Id);
            entity.Property(v => v.Id).IsRequired().ValueGeneratedOnAdd();
            entity.Property(v => v.VeterinarianId).IsRequired();
            entity.HasIndex(v => v.VeterinarianId);
            entity.Property(v => v.DayOfWeek).IsRequired();
            entity.Property(v => v.StartTime).IsRequired();
            entity.Property(v => v.EndTime).IsRequired();
        });

        builder.Entity<AvailabilityBlock>(entity =>
        {
            entity.HasKey(b => b.Id);
            entity.Property(b => b.Id).IsRequired().ValueGeneratedOnAdd();
            entity.Property(b => b.VeterinarianId).IsRequired();
            entity.HasIndex(b => b.VeterinarianId);
            entity.Property(b => b.Reason).HasMaxLength(500);
        });
    }
}
