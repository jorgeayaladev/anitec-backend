using Titan.AniTec.Platform.Livestock.Domain.Model.Aggregates;
using Microsoft.EntityFrameworkCore;

namespace Titan.AniTec.Platform.Livestock.Infrastructure.Persistence.EntityFrameworkCore.Configuration.Extensions;

public static class LivestockModelBuilderExtensions
{
    public static void ApplyLivestockConfiguration(this ModelBuilder builder)
    {
        builder.Entity<Breed>(entity =>
        {
            entity.HasKey(b => b.Id);
            entity.Property(b => b.Id).IsRequired().ValueGeneratedOnAdd();
            entity.Property(b => b.Name).IsRequired().HasMaxLength(200);
            entity.Property(b => b.Species).IsRequired().HasMaxLength(100);
            entity.Property(b => b.Description).HasMaxLength(2000);
            entity.HasIndex(b => new { b.Name, b.Species }).IsUnique();
        });

        builder.Entity<Animal>(entity =>
        {
            entity.HasKey(a => a.Id);
            entity.Property(a => a.Id).IsRequired().ValueGeneratedOnAdd();
            entity.Property(a => a.FarmId).IsRequired();
            entity.HasIndex(a => a.FarmId);
            entity.Property(a => a.Code).IsRequired().HasMaxLength(100);
            entity.HasIndex(a => a.Code);
            entity.Property(a => a.Name).HasMaxLength(200);
            entity.Property(a => a.Species).IsRequired().HasMaxLength(100);
            entity.Property(a => a.Sex).IsRequired().HasMaxLength(20);
            entity.Property(a => a.Weight);
            entity.Property(a => a.Color).HasMaxLength(100);
            entity.Property(a => a.Notes).HasMaxLength(2000);
            entity.Property(a => a.Status).IsRequired().HasMaxLength(50);
            entity.Property(a => a.PurchasePrice).HasColumnType("decimal(18,2)");

            entity.HasOne<Breed>()
                .WithMany()
                .HasForeignKey(a => a.BreedId)
                .OnDelete(DeleteBehavior.SetNull);

            entity.HasOne<Animal>()
                .WithMany()
                .HasForeignKey(a => a.MotherId)
                .OnDelete(DeleteBehavior.SetNull);

            entity.HasOne<Animal>()
                .WithMany()
                .HasForeignKey(a => a.FatherId)
                .OnDelete(DeleteBehavior.SetNull);
        });

        builder.Entity<Birth>(entity =>
        {
            entity.HasKey(b => b.Id);
            entity.Property(b => b.Id).IsRequired().ValueGeneratedOnAdd();
            entity.Property(b => b.FarmId).IsRequired();
            entity.HasIndex(b => b.FarmId);
            entity.Property(b => b.BirthDate).IsRequired();
            entity.Property(b => b.OffspringCount).IsRequired();
            entity.Property(b => b.Notes).HasMaxLength(2000);

            entity.HasOne<Animal>()
                .WithMany()
                .HasForeignKey(b => b.MotherId)
                .OnDelete(DeleteBehavior.Restrict);

            entity.HasOne<Animal>()
                .WithMany()
                .HasForeignKey(b => b.FatherId)
                .OnDelete(DeleteBehavior.SetNull);
        });

        builder.Entity<Mating>(entity =>
        {
            entity.HasKey(m => m.Id);
            entity.Property(m => m.Id).IsRequired().ValueGeneratedOnAdd();
            entity.Property(m => m.FarmId).IsRequired();
            entity.HasIndex(m => m.FarmId);
            entity.Property(m => m.MatingDate).IsRequired();
            entity.Property(m => m.Result).HasMaxLength(50);
            entity.Property(m => m.Notes).HasMaxLength(2000);

            entity.HasOne<Animal>()
                .WithMany()
                .HasForeignKey(m => m.FemaleId)
                .OnDelete(DeleteBehavior.Restrict);

            entity.HasOne<Animal>()
                .WithMany()
                .HasForeignKey(m => m.MaleId)
                .OnDelete(DeleteBehavior.Restrict);
        });

        builder.Entity<Weaning>(entity =>
        {
            entity.HasKey(w => w.Id);
            entity.Property(w => w.Id).IsRequired().ValueGeneratedOnAdd();
            entity.Property(w => w.FarmId).IsRequired();
            entity.HasIndex(w => w.FarmId);
            entity.Property(w => w.WeaningDate).IsRequired();
            entity.Property(w => w.Weight);
            entity.Property(w => w.Notes).HasMaxLength(2000);

            entity.HasOne<Animal>()
                .WithMany()
                .HasForeignKey(w => w.CalfId)
                .OnDelete(DeleteBehavior.Restrict);

            entity.HasOne<Animal>()
                .WithMany()
                .HasForeignKey(w => w.MotherId)
                .OnDelete(DeleteBehavior.Restrict);
        });
    }
}
