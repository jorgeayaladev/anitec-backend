using Titan.AniTec.Platform.Health.Domain.Model.Aggregates;
using Titan.AniTec.Platform.Shared.Domain.Repositories;

namespace Titan.AniTec.Platform.Health.Domain.Repositories;

public interface IVaccineRepository : IBaseRepository<Vaccine>
{
    Task<IReadOnlyCollection<Vaccine>> GetAllAsync();
    Task<IReadOnlyCollection<Vaccine>> SearchAsync(string searchTerm);
    Task<IReadOnlyCollection<Vaccine>> FindByDiseaseAsync(int diseaseId);
    Task<IReadOnlyCollection<Vaccine>> FindByRaceAsync(int raceId);
}

public interface IMedicationRepository : IBaseRepository<Medication>
{
    Task<IReadOnlyCollection<Medication>> GetAllAsync();
    Task<IReadOnlyCollection<Medication>> SearchAsync(string searchTerm);
    Task<IReadOnlyCollection<Medication>> FindByCategoryAsync(string category);
}

public interface IDiseaseRepository : IBaseRepository<Disease>
{
    Task<IReadOnlyCollection<Disease>> GetAllAsync();
    Task<IReadOnlyCollection<Disease>> FindByRaceAsync(int raceId);
    Task<IReadOnlyCollection<Disease>> FindBySymptomsAsync(string symptoms);
}

public interface IHealthEventRepository : IBaseRepository<HealthEvent>
{
    Task<IReadOnlyCollection<HealthEvent>> FindByFarmIdAsync(int farmId);
    Task<IReadOnlyCollection<HealthEvent>> FindByAnimalIdAsync(int animalId);
    Task<IReadOnlyCollection<HealthEvent>> FindByDateRangeAsync(int farmId, DateTime start, DateTime end);
    Task<IReadOnlyCollection<HealthEvent>> FindByTypeAsync(int farmId, string eventType);
    Task<IReadOnlyCollection<HealthEvent>> FindTodayAsync(int farmId);
    Task<IReadOnlyCollection<HealthEvent>> FindUpcomingAsync(int farmId, int days);
    Task<IReadOnlyCollection<HealthEvent>> FindOverdueAsync(int farmId);
}

public interface IDiagnosisRepository : IBaseRepository<Diagnosis>
{
    Task<IReadOnlyCollection<Diagnosis>> FindByFarmIdAsync(int farmId);
    Task<IReadOnlyCollection<Diagnosis>> FindByAnimalIdAsync(int animalId);
    Task<IReadOnlyCollection<Diagnosis>> FindByDiseaseIdAsync(int diseaseId);
    Task<IReadOnlyCollection<Diagnosis>> FindRecentAsync(int farmId);
}

public interface IVaccinationRepository : IBaseRepository<Vaccination>
{
    Task<IReadOnlyCollection<Vaccination>> FindByFarmIdAsync(int farmId);
    Task<IReadOnlyCollection<Vaccination>> FindByAnimalIdAsync(int animalId);
    Task<IReadOnlyCollection<Vaccination>> FindByDateRangeAsync(int farmId, DateTime start, DateTime end);
    Task<IReadOnlyCollection<Vaccination>> FindUpcomingAsync(int farmId);
    Task<IReadOnlyCollection<Vaccination>> FindOverdueAsync(int farmId);
}

public interface ITreatmentRepository : IBaseRepository<Treatment>
{
    Task<IReadOnlyCollection<Treatment>> FindByFarmIdAsync(int farmId);
    Task<IReadOnlyCollection<Treatment>> FindByAnimalIdAsync(int animalId);
    Task<IReadOnlyCollection<Treatment>> FindByDateRangeAsync(int farmId, DateTime start, DateTime end);
    Task<IReadOnlyCollection<Treatment>> FindActiveByFarmIdAsync(int farmId);
    Task<IReadOnlyCollection<Treatment>> FindCompletedByFarmIdAsync(int farmId);
    Task<IReadOnlyCollection<TreatmentDose>> FindDosesByTreatmentIdAsync(int treatmentId);
    Task AddDoseAsync(TreatmentDose dose);
}

public interface IVeterinaryVisitRepository : IBaseRepository<VeterinaryVisit>
{
    Task<IReadOnlyCollection<VeterinaryVisit>> FindByFarmIdAsync(int farmId);
    Task<IReadOnlyCollection<VeterinaryVisit>> FindByAnimalIdAsync(int animalId);
    Task<IReadOnlyCollection<VeterinaryVisit>> FindByDateRangeAsync(int farmId, DateTime start, DateTime end);
}

public interface IHealthAlertRepository : IBaseRepository<HealthAlert>
{
    Task<IReadOnlyCollection<HealthAlert>> FindByFarmIdAsync(int farmId);
    Task<IReadOnlyCollection<HealthAlert>> FindByAnimalIdAsync(int animalId);
    Task<IReadOnlyCollection<HealthAlert>> FindOpenByFarmIdAsync(int farmId);
    Task<IReadOnlyCollection<HealthAlert>> FindByTypeAsync(int farmId, string alertType);
}
