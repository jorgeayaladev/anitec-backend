using Microsoft.EntityFrameworkCore;
using Titan.AniTec.Platform.Health.Domain.Model.Aggregates;
using Titan.AniTec.Platform.Health.Domain.Repositories;
using Titan.AniTec.Platform.Shared.Infrastructure.Persistence.EntityFrameworkCore.Configuration;
using Titan.AniTec.Platform.Shared.Infrastructure.Persistence.EntityFrameworkCore.Repositories;

namespace Titan.AniTec.Platform.Health.Infrastructure.Persistence.EntityFrameworkCore.Repositories;

public class VaccinationRepository(AppDbContext context) : BaseRepository<Vaccination>(context), IVaccinationRepository
{
    public async Task<IReadOnlyCollection<Vaccination>> FindByFarmIdAsync(int farmId)
        => await Context.Set<Vaccination>()
            .Where(v => v.FarmId == farmId)
            .OrderByDescending(v => v.ApplicationDate)
            .ToListAsync();

    public async Task<IReadOnlyCollection<Vaccination>> FindByAnimalIdAsync(int animalId)
        => await Context.Set<Vaccination>()
            .Where(v => v.AnimalId == animalId)
            .OrderByDescending(v => v.ApplicationDate)
            .ToListAsync();

    public async Task<IReadOnlyCollection<Vaccination>> FindByDateRangeAsync(int farmId, DateTime start, DateTime end)
        => await Context.Set<Vaccination>()
            .Where(v => v.FarmId == farmId && v.ApplicationDate >= start && v.ApplicationDate <= end)
            .OrderByDescending(v => v.ApplicationDate)
            .ToListAsync();

    public async Task<IReadOnlyCollection<Vaccination>> FindUpcomingAsync(int farmId)
        => await Context.Set<Vaccination>()
            .Where(v => v.FarmId == farmId && v.NextDoseDate >= DateTime.UtcNow)
            .OrderBy(v => v.NextDoseDate)
            .ToListAsync();

    public async Task<IReadOnlyCollection<Vaccination>> FindOverdueAsync(int farmId)
        => await Context.Set<Vaccination>()
            .Where(v => v.FarmId == farmId && v.NextDoseDate < DateTime.UtcNow)
            .OrderBy(v => v.NextDoseDate)
            .ToListAsync();
}

public class TreatmentRepository(AppDbContext context) : BaseRepository<Treatment>(context), ITreatmentRepository
{
    public async Task<IReadOnlyCollection<Treatment>> FindByFarmIdAsync(int farmId)
        => await Context.Set<Treatment>()
            .Where(t => t.FarmId == farmId)
            .OrderByDescending(t => t.TreatmentDate)
            .ToListAsync();

    public async Task<IReadOnlyCollection<Treatment>> FindByAnimalIdAsync(int animalId)
        => await Context.Set<Treatment>()
            .Where(t => t.AnimalId == animalId)
            .OrderByDescending(t => t.TreatmentDate)
            .ToListAsync();

    public async Task<IReadOnlyCollection<Treatment>> FindByDateRangeAsync(int farmId, DateTime start, DateTime end)
        => await Context.Set<Treatment>()
            .Where(t => t.FarmId == farmId && t.TreatmentDate >= start && t.TreatmentDate <= end)
            .OrderByDescending(t => t.TreatmentDate)
            .ToListAsync();

    public async Task<IReadOnlyCollection<Treatment>> FindActiveByFarmIdAsync(int farmId)
        => await Context.Set<Treatment>()
            .Where(t => t.FarmId == farmId && t.Status == "active")
            .OrderByDescending(t => t.TreatmentDate)
            .ToListAsync();

    public async Task<IReadOnlyCollection<Treatment>> FindCompletedByFarmIdAsync(int farmId)
        => await Context.Set<Treatment>()
            .Where(t => t.FarmId == farmId && t.Status == "completed")
            .OrderByDescending(t => t.TreatmentDate)
            .ToListAsync();

    public async Task<IReadOnlyCollection<TreatmentDose>> FindDosesByTreatmentIdAsync(int treatmentId)
        => await Context.Set<TreatmentDose>()
            .Where(d => d.TreatmentId == treatmentId)
            .OrderByDescending(d => d.AdministeredDate)
            .ToListAsync();

    public async Task AddDoseAsync(TreatmentDose dose)
        => await Context.Set<TreatmentDose>().AddAsync(dose);
}

public class VeterinaryVisitRepository(AppDbContext context) : BaseRepository<VeterinaryVisit>(context), IVeterinaryVisitRepository
{
    public async Task<IReadOnlyCollection<VeterinaryVisit>> FindByFarmIdAsync(int farmId)
        => await Context.Set<VeterinaryVisit>()
            .Where(v => v.FarmId == farmId)
            .OrderByDescending(v => v.VisitDate)
            .ToListAsync();

    public async Task<IReadOnlyCollection<VeterinaryVisit>> FindByAnimalIdAsync(int animalId)
        => await Context.Set<VeterinaryVisit>()
            .Where(v => v.AnimalId == animalId)
            .OrderByDescending(v => v.VisitDate)
            .ToListAsync();

    public async Task<IReadOnlyCollection<VeterinaryVisit>> FindByDateRangeAsync(int farmId, DateTime start, DateTime end)
        => await Context.Set<VeterinaryVisit>()
            .Where(v => v.FarmId == farmId && v.VisitDate >= start && v.VisitDate <= end)
            .OrderByDescending(v => v.VisitDate)
            .ToListAsync();
}

public class VaccineRepository(AppDbContext context) : BaseRepository<Vaccine>(context), IVaccineRepository
{
    public async Task<IReadOnlyCollection<Vaccine>> GetAllAsync()
        => await Context.Set<Vaccine>().OrderBy(v => v.Name).ToListAsync();

    public async Task<IReadOnlyCollection<Vaccine>> SearchAsync(string searchTerm)
        => await Context.Set<Vaccine>()
            .Where(v => v.Name.Contains(searchTerm) || (v.Description != null && v.Description.Contains(searchTerm)))
            .OrderBy(v => v.Name)
            .ToListAsync();

    public async Task<IReadOnlyCollection<Vaccine>> FindByDiseaseAsync(int diseaseId)
        => await Context.Set<Vaccine>().OrderBy(v => v.Name).ToListAsync();

    public async Task<IReadOnlyCollection<Vaccine>> FindByRaceAsync(int raceId)
        => await Context.Set<Vaccine>().OrderBy(v => v.Name).ToListAsync();
}

public class MedicationRepository(AppDbContext context) : BaseRepository<Medication>(context), IMedicationRepository
{
    public async Task<IReadOnlyCollection<Medication>> GetAllAsync()
        => await Context.Set<Medication>().OrderBy(m => m.Name).ToListAsync();

    public async Task<IReadOnlyCollection<Medication>> SearchAsync(string searchTerm)
        => await Context.Set<Medication>()
            .Where(m => m.Name.Contains(searchTerm) || (m.Description != null && m.Description.Contains(searchTerm)))
            .OrderBy(m => m.Name)
            .ToListAsync();

    public async Task<IReadOnlyCollection<Medication>> FindByCategoryAsync(string category)
        => await Context.Set<Medication>()
            .Where(m => m.Category != null && m.Category == category)
            .OrderBy(m => m.Name)
            .ToListAsync();
}

public class DiseaseRepository(AppDbContext context) : BaseRepository<Disease>(context), IDiseaseRepository
{
    public async Task<IReadOnlyCollection<Disease>> GetAllAsync()
        => await Context.Set<Disease>().OrderBy(d => d.Name).ToListAsync();

    public async Task<IReadOnlyCollection<Disease>> FindByRaceAsync(int raceId)
        => await Context.Set<Disease>().OrderBy(d => d.Name).ToListAsync();

    public async Task<IReadOnlyCollection<Disease>> FindBySymptomsAsync(string symptoms)
        => await Context.Set<Disease>()
            .Where(d => d.Symptoms != null && d.Symptoms.Contains(symptoms))
            .OrderBy(d => d.Name)
            .ToListAsync();
}

public class HealthEventRepository(AppDbContext context) : BaseRepository<HealthEvent>(context), IHealthEventRepository
{
    public async Task<IReadOnlyCollection<HealthEvent>> FindByFarmIdAsync(int farmId)
        => await Context.Set<HealthEvent>()
            .Where(e => e.FarmId == farmId)
            .OrderBy(e => e.ScheduledDate)
            .ToListAsync();

    public async Task<IReadOnlyCollection<HealthEvent>> FindByAnimalIdAsync(int animalId)
        => await Context.Set<HealthEvent>()
            .Where(e => e.AnimalId == animalId)
            .OrderBy(e => e.ScheduledDate)
            .ToListAsync();

    public async Task<IReadOnlyCollection<HealthEvent>> FindByDateRangeAsync(int farmId, DateTime start, DateTime end)
        => await Context.Set<HealthEvent>()
            .Where(e => e.FarmId == farmId && e.ScheduledDate >= start && e.ScheduledDate <= end)
            .OrderBy(e => e.ScheduledDate)
            .ToListAsync();

    public async Task<IReadOnlyCollection<HealthEvent>> FindByTypeAsync(int farmId, string eventType)
        => await Context.Set<HealthEvent>()
            .Where(e => e.FarmId == farmId && e.EventType == eventType)
            .OrderBy(e => e.ScheduledDate)
            .ToListAsync();

    public async Task<IReadOnlyCollection<HealthEvent>> FindTodayAsync(int farmId)
        => await Context.Set<HealthEvent>()
            .Where(e => e.FarmId == farmId && e.ScheduledDate.Date == DateTime.UtcNow.Date)
            .OrderBy(e => e.ScheduledDate)
            .ToListAsync();

    public async Task<IReadOnlyCollection<HealthEvent>> FindUpcomingAsync(int farmId, int days)
    {
        var end = DateTime.UtcNow.Date.AddDays(days);
        return await Context.Set<HealthEvent>()
            .Where(e => e.FarmId == farmId && !e.IsCompleted && e.ScheduledDate <= end && e.ScheduledDate >= DateTime.UtcNow)
            .OrderBy(e => e.ScheduledDate)
            .ToListAsync();
    }

    public async Task<IReadOnlyCollection<HealthEvent>> FindOverdueAsync(int farmId)
        => await Context.Set<HealthEvent>()
            .Where(e => e.FarmId == farmId && !e.IsCompleted && e.ScheduledDate < DateTime.UtcNow)
            .OrderBy(e => e.ScheduledDate)
            .ToListAsync();
}

public class DiagnosisRepository(AppDbContext context) : BaseRepository<Diagnosis>(context), IDiagnosisRepository
{
    public async Task<IReadOnlyCollection<Diagnosis>> FindByFarmIdAsync(int farmId)
        => await Context.Set<Diagnosis>()
            .Where(d => d.FarmId == farmId)
            .OrderByDescending(d => d.DiagnosedDate)
            .ToListAsync();

    public async Task<IReadOnlyCollection<Diagnosis>> FindByAnimalIdAsync(int animalId)
        => await Context.Set<Diagnosis>()
            .Where(d => d.AnimalId == animalId)
            .OrderByDescending(d => d.DiagnosedDate)
            .ToListAsync();

    public async Task<IReadOnlyCollection<Diagnosis>> FindByDiseaseIdAsync(int diseaseId)
        => await Context.Set<Diagnosis>()
            .Where(d => d.DiseaseId == diseaseId)
            .OrderByDescending(d => d.DiagnosedDate)
            .ToListAsync();

    public async Task<IReadOnlyCollection<Diagnosis>> FindRecentAsync(int farmId)
        => await Context.Set<Diagnosis>()
            .Where(d => d.FarmId == farmId)
            .OrderByDescending(d => d.DiagnosedDate)
            .Take(20)
            .ToListAsync();
}

public class HealthAlertRepository(AppDbContext context) : BaseRepository<HealthAlert>(context), IHealthAlertRepository
{
    public async Task<IReadOnlyCollection<HealthAlert>> FindByFarmIdAsync(int farmId)
        => await Context.Set<HealthAlert>()
            .Where(a => a.FarmId == farmId)
            .OrderByDescending(a => a.CreatedAt)
            .ToListAsync();

    public async Task<IReadOnlyCollection<HealthAlert>> FindByAnimalIdAsync(int animalId)
        => await Context.Set<HealthAlert>()
            .Where(a => a.AnimalId == animalId)
            .OrderByDescending(a => a.CreatedAt)
            .ToListAsync();

    public async Task<IReadOnlyCollection<HealthAlert>> FindOpenByFarmIdAsync(int farmId)
        => await Context.Set<HealthAlert>()
            .Where(a => a.FarmId == farmId && !a.IsResolved)
            .OrderByDescending(a => a.CreatedAt)
            .ToListAsync();

    public async Task<IReadOnlyCollection<HealthAlert>> FindByTypeAsync(int farmId, string alertType)
        => await Context.Set<HealthAlert>()
            .Where(a => a.FarmId == farmId && a.AlertType == alertType)
            .OrderByDescending(a => a.CreatedAt)
            .ToListAsync();
}
