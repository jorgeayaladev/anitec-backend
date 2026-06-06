using Titan.AniTec.Platform.Health.Domain.Model.Aggregates;
using Microsoft.EntityFrameworkCore;

namespace Titan.AniTec.Platform.Health.Infrastructure.Persistence.EntityFrameworkCore.Configuration.Extensions;

public static class HealthModelBuilderExtensions
{
    public static void ApplyHealthConfiguration(this ModelBuilder builder)
    {
        builder.Entity<Vaccination>(entity =>
        {
            entity.HasKey(v => v.Id);
            entity.Property(v => v.Id).IsRequired().ValueGeneratedOnAdd();
            entity.Property(v => v.FarmId).IsRequired();
            entity.HasIndex(v => v.FarmId);
            entity.Property(v => v.AnimalId).IsRequired();
            entity.HasIndex(v => v.AnimalId);
            entity.Property(v => v.VaccineName).IsRequired().HasMaxLength(200);
            entity.Property(v => v.BatchNumber).HasMaxLength(100);
            entity.Property(v => v.ApplicationRoute).HasMaxLength(100);
            entity.Property(v => v.Dosage).HasMaxLength(100);
            entity.Property(v => v.AppliedBy).HasMaxLength(200);
            entity.Property(v => v.Notes).HasMaxLength(2000);
        });

        builder.Entity<Treatment>(entity =>
        {
            entity.HasKey(t => t.Id);
            entity.Property(t => t.Id).IsRequired().ValueGeneratedOnAdd();
            entity.Property(t => t.FarmId).IsRequired();
            entity.HasIndex(t => t.FarmId);
            entity.Property(t => t.AnimalId).IsRequired();
            entity.HasIndex(t => t.AnimalId);
            entity.Property(t => t.Diagnosis).IsRequired().HasMaxLength(500);
            entity.Property(t => t.MedicationName).HasMaxLength(200);
            entity.Property(t => t.Dosage).HasMaxLength(100);
            entity.Property(t => t.AdministrationRoute).HasMaxLength(100);
            entity.Property(t => t.TreatedBy).HasMaxLength(200);
            entity.Property(t => t.Notes).HasMaxLength(2000);
            entity.Property(t => t.Status).HasMaxLength(50).HasDefaultValue("pending");
        });

        builder.Entity<VeterinaryVisit>(entity =>
        {
            entity.HasKey(v => v.Id);
            entity.Property(v => v.Id).IsRequired().ValueGeneratedOnAdd();
            entity.Property(v => v.FarmId).IsRequired();
            entity.HasIndex(v => v.FarmId);
            entity.Property(v => v.AnimalId).IsRequired();
            entity.HasIndex(v => v.AnimalId);
            entity.Property(v => v.VetName).IsRequired().HasMaxLength(200);
            entity.Property(v => v.Reason).IsRequired().HasMaxLength(500);
            entity.Property(v => v.Diagnosis).HasMaxLength(500);
            entity.Property(v => v.Recommendations).HasMaxLength(2000);
            entity.Property(v => v.Cost).HasColumnType("decimal(18,2)");
            entity.Property(v => v.Notes).HasMaxLength(2000);
        });

        builder.Entity<HealthAlert>(entity =>
        {
            entity.HasKey(a => a.Id);
            entity.Property(a => a.Id).IsRequired().ValueGeneratedOnAdd();
            entity.Property(a => a.FarmId).IsRequired();
            entity.HasIndex(a => a.FarmId);
            entity.Property(a => a.AnimalId).IsRequired();
            entity.HasIndex(a => a.AnimalId);
            entity.Property(a => a.AlertType).IsRequired().HasMaxLength(100);
            entity.Property(a => a.Description).IsRequired().HasMaxLength(500);
            entity.Property(a => a.ResolvedBy).HasMaxLength(200);
            entity.Property(a => a.Notes).HasMaxLength(2000);
        });

        builder.Entity<Vaccine>(entity =>
        {
            entity.HasKey(v => v.Id);
            entity.Property(v => v.Id).IsRequired().ValueGeneratedOnAdd();
            entity.Property(v => v.Name).IsRequired().HasMaxLength(200);
            entity.Property(v => v.Description).HasMaxLength(500);
            entity.Property(v => v.Manufacturer).HasMaxLength(200);
        });

        builder.Entity<Medication>(entity =>
        {
            entity.HasKey(m => m.Id);
            entity.Property(m => m.Id).IsRequired().ValueGeneratedOnAdd();
            entity.Property(m => m.Name).IsRequired().HasMaxLength(200);
            entity.Property(m => m.Description).HasMaxLength(500);
            entity.Property(m => m.Category).HasMaxLength(100);
            entity.Property(m => m.Manufacturer).HasMaxLength(200);
            entity.Property(m => m.ActiveIngredient).HasMaxLength(200);
            entity.Property(m => m.Presentation).HasMaxLength(100);
            entity.Property(m => m.DosageForm).HasMaxLength(100);
        });

        builder.Entity<Disease>(entity =>
        {
            entity.HasKey(d => d.Id);
            entity.Property(d => d.Id).IsRequired().ValueGeneratedOnAdd();
            entity.Property(d => d.Name).IsRequired().HasMaxLength(200);
            entity.Property(d => d.Description).HasMaxLength(500);
            entity.Property(d => d.Category).HasMaxLength(100);
            entity.Property(d => d.Symptoms).HasMaxLength(2000);
        });

        builder.Entity<Diagnosis>(entity =>
        {
            entity.HasKey(d => d.Id);
            entity.Property(d => d.Id).IsRequired().ValueGeneratedOnAdd();
            entity.Property(d => d.FarmId).IsRequired();
            entity.HasIndex(d => d.FarmId);
            entity.Property(d => d.AnimalId).IsRequired();
            entity.HasIndex(d => d.AnimalId);
            entity.Property(d => d.Condition).IsRequired().HasMaxLength(500);
            entity.Property(d => d.Description).HasMaxLength(2000);
            entity.Property(d => d.Severity).HasMaxLength(50);
            entity.Property(d => d.DiagnosedBy).HasMaxLength(200);
        });

        builder.Entity<HealthEvent>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.Property(e => e.Id).IsRequired().ValueGeneratedOnAdd();
            entity.Property(e => e.FarmId).IsRequired();
            entity.HasIndex(e => e.FarmId);
            entity.Property(e => e.AnimalId);
            entity.HasIndex(e => e.AnimalId);
            entity.Property(e => e.Title).IsRequired().HasMaxLength(200);
            entity.Property(e => e.Description).HasMaxLength(500);
            entity.Property(e => e.EventType).IsRequired().HasMaxLength(100);
            entity.Property(e => e.CompletedBy).HasMaxLength(200);
            entity.Property(e => e.Notes).HasMaxLength(2000);
        });

        builder.Entity<TreatmentDose>(entity =>
        {
            entity.HasKey(d => d.Id);
            entity.Property(d => d.Id).IsRequired().ValueGeneratedOnAdd();
            entity.Property(d => d.TreatmentId).IsRequired();
            entity.HasIndex(d => d.TreatmentId);
            entity.Property(d => d.AdministeredBy).HasMaxLength(200);
            entity.Property(d => d.Dosage).HasMaxLength(100);
            entity.Property(d => d.Notes).HasMaxLength(2000);
        });
    }
}
