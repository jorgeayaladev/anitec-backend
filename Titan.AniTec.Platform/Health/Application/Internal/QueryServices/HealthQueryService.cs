using Titan.AniTec.Platform.Health.Domain.Model;
using Titan.AniTec.Platform.Health.Domain.Model.Aggregates;
using Titan.AniTec.Platform.Health.Domain.Repositories;
using Titan.AniTec.Platform.Health.Application.CommandServices;
using Titan.AniTec.Platform.Shared.Application.Model;

namespace Titan.AniTec.Platform.Health.Application.Internal.QueryServices;

public class HealthQueryService(
    IVaccinationRepository vaccinationRepository,
    ITreatmentRepository treatmentRepository,
    IVeterinaryVisitRepository veterinaryVisitRepository,
    IHealthAlertRepository healthAlertRepository,
    IHealthEventRepository healthEventRepository,
    IVaccineRepository vaccineRepository,
    IMedicationRepository medicationRepository,
    IDiseaseRepository diseaseRepository,
    IDiagnosisRepository diagnosisRepository) : IHealthQueryService
{
    public async Task<Result<IReadOnlyCollection<Vaccine>>> GetAllVaccinesAsync(GetAllVaccinesQuery query)
    {
        try
        {
            var items = await vaccineRepository.GetAllAsync();
            return Result<IReadOnlyCollection<Vaccine>>.Success(items);
        }
        catch (Exception)
        {
            return Result<IReadOnlyCollection<Vaccine>>.Failure(HealthError.VaccinationNotFound);
        }
    }

    public async Task<Result<Vaccine>> GetVaccineByIdAsync(GetVaccineByIdQuery query)
    {
        try
        {
            var item = await vaccineRepository.FindByIdAsync(query.VaccineId);
            return item != null
                ? Result<Vaccine>.Success(item)
                : Result<Vaccine>.Failure(HealthError.VaccinationNotFound);
        }
        catch (Exception)
        {
            return Result<Vaccine>.Failure(HealthError.VaccinationNotFound);
        }
    }

    public async Task<Result<IReadOnlyCollection<Vaccine>>> SearchVaccinesAsync(SearchVaccinesQuery query)
    {
        try
        {
            var items = await vaccineRepository.SearchAsync(query.SearchTerm);
            return Result<IReadOnlyCollection<Vaccine>>.Success(items);
        }
        catch (Exception)
        {
            return Result<IReadOnlyCollection<Vaccine>>.Failure(HealthError.VaccinationNotFound);
        }
    }

    public async Task<Result<IReadOnlyCollection<Vaccine>>> GetVaccinesByDiseaseAsync(GetVaccinesByDiseaseQuery query)
    {
        try
        {
            var items = await vaccineRepository.FindByDiseaseAsync(query.DiseaseId);
            return Result<IReadOnlyCollection<Vaccine>>.Success(items);
        }
        catch (Exception)
        {
            return Result<IReadOnlyCollection<Vaccine>>.Failure(HealthError.VaccinationNotFound);
        }
    }

    public async Task<Result<IReadOnlyCollection<Vaccine>>> GetVaccinesByRaceAsync(GetVaccinesByRaceQuery query)
    {
        try
        {
            var items = await vaccineRepository.FindByRaceAsync(query.RaceId);
            return Result<IReadOnlyCollection<Vaccine>>.Success(items);
        }
        catch (Exception)
        {
            return Result<IReadOnlyCollection<Vaccine>>.Failure(HealthError.VaccinationNotFound);
        }
    }

    public async Task<Result<IReadOnlyCollection<Medication>>> GetAllMedicationsAsync(GetAllMedicationsQuery query)
    {
        try
        {
            var items = await medicationRepository.GetAllAsync();
            return Result<IReadOnlyCollection<Medication>>.Success(items);
        }
        catch (Exception)
        {
            return Result<IReadOnlyCollection<Medication>>.Failure(HealthError.TreatmentNotFound);
        }
    }

    public async Task<Result<Medication>> GetMedicationByIdAsync(GetMedicationByIdQuery query)
    {
        try
        {
            var item = await medicationRepository.FindByIdAsync(query.MedicationId);
            return item != null
                ? Result<Medication>.Success(item)
                : Result<Medication>.Failure(HealthError.TreatmentNotFound);
        }
        catch (Exception)
        {
            return Result<Medication>.Failure(HealthError.TreatmentNotFound);
        }
    }

    public async Task<Result<IReadOnlyCollection<Medication>>> SearchMedicationsAsync(SearchMedicationsQuery query)
    {
        try
        {
            var items = await medicationRepository.SearchAsync(query.SearchTerm);
            return Result<IReadOnlyCollection<Medication>>.Success(items);
        }
        catch (Exception)
        {
            return Result<IReadOnlyCollection<Medication>>.Failure(HealthError.TreatmentNotFound);
        }
    }

    public async Task<Result<IReadOnlyCollection<Medication>>> GetMedicationsByCategoryAsync(GetMedicationsByCategoryQuery query)
    {
        try
        {
            var items = await medicationRepository.FindByCategoryAsync(query.Category);
            return Result<IReadOnlyCollection<Medication>>.Success(items);
        }
        catch (Exception)
        {
            return Result<IReadOnlyCollection<Medication>>.Failure(HealthError.TreatmentNotFound);
        }
    }

    public async Task<Result<IReadOnlyCollection<Disease>>> GetAllDiseasesAsync(GetAllDiseasesQuery query)
    {
        try
        {
            var items = await diseaseRepository.GetAllAsync();
            return Result<IReadOnlyCollection<Disease>>.Success(items);
        }
        catch (Exception)
        {
            return Result<IReadOnlyCollection<Disease>>.Failure(HealthError.VeterinaryVisitNotFound);
        }
    }

    public async Task<Result<Disease>> GetDiseaseByIdAsync(GetDiseaseByIdQuery query)
    {
        try
        {
            var item = await diseaseRepository.FindByIdAsync(query.DiseaseId);
            return item != null
                ? Result<Disease>.Success(item)
                : Result<Disease>.Failure(HealthError.VeterinaryVisitNotFound);
        }
        catch (Exception)
        {
            return Result<Disease>.Failure(HealthError.VeterinaryVisitNotFound);
        }
    }

    public async Task<Result<IReadOnlyCollection<Disease>>> GetDiseasesByRaceAsync(GetDiseasesByRaceQuery query)
    {
        try
        {
            var items = await diseaseRepository.FindByRaceAsync(query.RaceId);
            return Result<IReadOnlyCollection<Disease>>.Success(items);
        }
        catch (Exception)
        {
            return Result<IReadOnlyCollection<Disease>>.Failure(HealthError.VeterinaryVisitNotFound);
        }
    }

    public async Task<Result<IReadOnlyCollection<Disease>>> GetDiseasesBySymptomsAsync(GetDiseasesBySymptomsQuery query)
    {
        try
        {
            var items = await diseaseRepository.FindBySymptomsAsync(query.Symptoms);
            return Result<IReadOnlyCollection<Disease>>.Success(items);
        }
        catch (Exception)
        {
            return Result<IReadOnlyCollection<Disease>>.Failure(HealthError.VeterinaryVisitNotFound);
        }
    }

    public async Task<Result<IReadOnlyCollection<Diagnosis>>> GetAllDiagnosesAsync(GetAllDiagnosesQuery query)
    {
        try
        {
            var items = await diagnosisRepository.FindByFarmIdAsync(query.UserId);
            return Result<IReadOnlyCollection<Diagnosis>>.Success(items);
        }
        catch (Exception)
        {
            return Result<IReadOnlyCollection<Diagnosis>>.Failure(HealthError.VeterinaryVisitNotFound);
        }
    }

    public async Task<Result<Diagnosis>> GetDiagnosisByIdAsync(GetDiagnosisByIdQuery query)
    {
        try
        {
            var item = await diagnosisRepository.FindByIdAsync(query.DiagnosisId);
            return item != null
                ? Result<Diagnosis>.Success(item)
                : Result<Diagnosis>.Failure(HealthError.VeterinaryVisitNotFound);
        }
        catch (Exception)
        {
            return Result<Diagnosis>.Failure(HealthError.VeterinaryVisitNotFound);
        }
    }

    public async Task<Result<IReadOnlyCollection<Diagnosis>>> GetDiagnosesByAnimalAsync(GetDiagnosesByAnimalQuery query)
    {
        try
        {
            var items = await diagnosisRepository.FindByAnimalIdAsync(query.AnimalId);
            return Result<IReadOnlyCollection<Diagnosis>>.Success(items);
        }
        catch (Exception)
        {
            return Result<IReadOnlyCollection<Diagnosis>>.Failure(HealthError.VeterinaryVisitNotFound);
        }
    }

    public async Task<Result<IReadOnlyCollection<Diagnosis>>> GetDiagnosesByDiseaseAsync(GetDiagnosesByDiseaseQuery query)
    {
        try
        {
            var items = await diagnosisRepository.FindByDiseaseIdAsync(query.DiseaseId);
            return Result<IReadOnlyCollection<Diagnosis>>.Success(items);
        }
        catch (Exception)
        {
            return Result<IReadOnlyCollection<Diagnosis>>.Failure(HealthError.VeterinaryVisitNotFound);
        }
    }

    public async Task<Result<IReadOnlyCollection<Diagnosis>>> GetRecentDiagnosesAsync(GetRecentDiagnosesQuery query)
    {
        try
        {
            var items = await diagnosisRepository.FindRecentAsync(query.UserId);
            return Result<IReadOnlyCollection<Diagnosis>>.Success(items);
        }
        catch (Exception)
        {
            return Result<IReadOnlyCollection<Diagnosis>>.Failure(HealthError.VeterinaryVisitNotFound);
        }
    }

    public async Task<Result<IReadOnlyCollection<Vaccination>>> GetAllVaccinationsAsync(GetAllVaccinationsQuery query)
    {
        try
        {
            var items = await vaccinationRepository.FindByFarmIdAsync(query.UserId);
            return Result<IReadOnlyCollection<Vaccination>>.Success(items);
        }
        catch (Exception)
        {
            return Result<IReadOnlyCollection<Vaccination>>.Failure(HealthError.VaccinationNotFound);
        }
    }

    public async Task<Result<Vaccination>> GetVaccinationByIdAsync(GetVaccinationByIdQuery query)
    {
        try
        {
            var item = await vaccinationRepository.FindByIdAsync(query.VaccinationId);
            return item != null
                ? Result<Vaccination>.Success(item)
                : Result<Vaccination>.Failure(HealthError.VaccinationNotFound);
        }
        catch (Exception)
        {
            return Result<Vaccination>.Failure(HealthError.VaccinationNotFound);
        }
    }

    public async Task<Result<IReadOnlyCollection<Vaccination>>> GetVaccinationsByAnimalAsync(GetVaccinationsByAnimalQuery query)
    {
        try
        {
            var items = await vaccinationRepository.FindByAnimalIdAsync(query.AnimalId);
            return Result<IReadOnlyCollection<Vaccination>>.Success(items);
        }
        catch (Exception)
        {
            return Result<IReadOnlyCollection<Vaccination>>.Failure(HealthError.VaccinationNotFound);
        }
    }

    public async Task<Result<IReadOnlyCollection<Vaccination>>> GetVaccinationsByDateRangeAsync(GetVaccinationsByDateRangeQuery query)
    {
        try
        {
            var items = await vaccinationRepository.FindByDateRangeAsync(query.UserId, query.Start, query.End);
            return Result<IReadOnlyCollection<Vaccination>>.Success(items);
        }
        catch (Exception)
        {
            return Result<IReadOnlyCollection<Vaccination>>.Failure(HealthError.VaccinationNotFound);
        }
    }

    public async Task<Result<IReadOnlyCollection<Vaccination>>> GetUpcomingVaccinationsAsync(GetUpcomingVaccinationsQuery query)
    {
        try
        {
            var items = await vaccinationRepository.FindUpcomingAsync(query.UserId);
            return Result<IReadOnlyCollection<Vaccination>>.Success(items);
        }
        catch (Exception)
        {
            return Result<IReadOnlyCollection<Vaccination>>.Failure(HealthError.VaccinationNotFound);
        }
    }

    public async Task<Result<IReadOnlyCollection<Treatment>>> GetAllTreatmentsAsync(GetAllTreatmentsQuery query)
    {
        try
        {
            var items = await treatmentRepository.FindByFarmIdAsync(query.UserId);
            return Result<IReadOnlyCollection<Treatment>>.Success(items);
        }
        catch (Exception)
        {
            return Result<IReadOnlyCollection<Treatment>>.Failure(HealthError.TreatmentNotFound);
        }
    }

    public async Task<Result<Treatment>> GetTreatmentByIdAsync(GetTreatmentByIdQuery query)
    {
        try
        {
            var item = await treatmentRepository.FindByIdAsync(query.TreatmentId);
            return item != null
                ? Result<Treatment>.Success(item)
                : Result<Treatment>.Failure(HealthError.TreatmentNotFound);
        }
        catch (Exception)
        {
            return Result<Treatment>.Failure(HealthError.TreatmentNotFound);
        }
    }

    public async Task<Result<IReadOnlyCollection<Treatment>>> GetTreatmentsByAnimalAsync(GetTreatmentsByAnimalQuery query)
    {
        try
        {
            var items = await treatmentRepository.FindByAnimalIdAsync(query.AnimalId);
            return Result<IReadOnlyCollection<Treatment>>.Success(items);
        }
        catch (Exception)
        {
            return Result<IReadOnlyCollection<Treatment>>.Failure(HealthError.TreatmentNotFound);
        }
    }

    public async Task<Result<IReadOnlyCollection<Treatment>>> GetTreatmentsByDateRangeAsync(GetTreatmentsByDateRangeQuery query)
    {
        try
        {
            var items = await treatmentRepository.FindByDateRangeAsync(query.UserId, query.Start, query.End);
            return Result<IReadOnlyCollection<Treatment>>.Success(items);
        }
        catch (Exception)
        {
            return Result<IReadOnlyCollection<Treatment>>.Failure(HealthError.TreatmentNotFound);
        }
    }

    public async Task<Result<IReadOnlyCollection<VeterinaryVisit>>> GetAllVeterinaryVisitsAsync(GetAllVeterinaryVisitsQuery query)
    {
        try
        {
            var items = await veterinaryVisitRepository.FindByFarmIdAsync(query.UserId);
            return Result<IReadOnlyCollection<VeterinaryVisit>>.Success(items);
        }
        catch (Exception)
        {
            return Result<IReadOnlyCollection<VeterinaryVisit>>.Failure(HealthError.VeterinaryVisitNotFound);
        }
    }

    public async Task<Result<VeterinaryVisit>> GetVeterinaryVisitByIdAsync(GetVeterinaryVisitByIdQuery query)
    {
        try
        {
            var item = await veterinaryVisitRepository.FindByIdAsync(query.VisitId);
            return item != null
                ? Result<VeterinaryVisit>.Success(item)
                : Result<VeterinaryVisit>.Failure(HealthError.VeterinaryVisitNotFound);
        }
        catch (Exception)
        {
            return Result<VeterinaryVisit>.Failure(HealthError.VeterinaryVisitNotFound);
        }
    }

    public async Task<Result<IReadOnlyCollection<VeterinaryVisit>>> GetVeterinaryVisitsByAnimalAsync(GetVeterinaryVisitsByAnimalQuery query)
    {
        try
        {
            var items = await veterinaryVisitRepository.FindByAnimalIdAsync(query.AnimalId);
            return Result<IReadOnlyCollection<VeterinaryVisit>>.Success(items);
        }
        catch (Exception)
        {
            return Result<IReadOnlyCollection<VeterinaryVisit>>.Failure(HealthError.VeterinaryVisitNotFound);
        }
    }

    public async Task<Result<IReadOnlyCollection<VeterinaryVisit>>> GetVeterinaryVisitsByDateRangeAsync(GetVeterinaryVisitsByDateRangeQuery query)
    {
        try
        {
            var items = await veterinaryVisitRepository.FindByDateRangeAsync(query.UserId, query.Start, query.End);
            return Result<IReadOnlyCollection<VeterinaryVisit>>.Success(items);
        }
        catch (Exception)
        {
            return Result<IReadOnlyCollection<VeterinaryVisit>>.Failure(HealthError.VeterinaryVisitNotFound);
        }
    }

    public async Task<Result<IReadOnlyCollection<HealthEvent>>> GetHealthCalendarAsync(GetHealthCalendarQuery query)
    {
        try
        {
            var items = await healthEventRepository.FindByFarmIdAsync(query.UserId);
            return Result<IReadOnlyCollection<HealthEvent>>.Success(items);
        }
        catch (Exception)
        {
            return Result<IReadOnlyCollection<HealthEvent>>.Failure(HealthError.VaccinationNotFound);
        }
    }

    public async Task<Result<IReadOnlyCollection<HealthEvent>>> GetAllHealthEventsAsync(GetAllHealthEventsQuery query)
    {
        try
        {
            var items = await healthEventRepository.FindByFarmIdAsync(query.UserId);
            return Result<IReadOnlyCollection<HealthEvent>>.Success(items);
        }
        catch (Exception)
        {
            return Result<IReadOnlyCollection<HealthEvent>>.Failure(HealthError.VaccinationNotFound);
        }
    }

    public async Task<Result<HealthEvent>> GetHealthEventByIdAsync(GetHealthEventByIdQuery query)
    {
        try
        {
            var item = await healthEventRepository.FindByIdAsync(query.EventId);
            return item != null
                ? Result<HealthEvent>.Success(item)
                : Result<HealthEvent>.Failure(HealthError.VaccinationNotFound);
        }
        catch (Exception)
        {
            return Result<HealthEvent>.Failure(HealthError.VaccinationNotFound);
        }
    }

    public async Task<Result<IReadOnlyCollection<HealthEvent>>> GetTodaysHealthEventsAsync(GetTodaysHealthEventsQuery query)
    {
        try
        {
            var items = await healthEventRepository.FindTodayAsync(query.UserId);
            return Result<IReadOnlyCollection<HealthEvent>>.Success(items);
        }
        catch (Exception)
        {
            return Result<IReadOnlyCollection<HealthEvent>>.Failure(HealthError.VaccinationNotFound);
        }
    }

    public async Task<Result<IReadOnlyCollection<HealthEvent>>> GetUpcomingHealthEventsAsync(GetUpcomingHealthEventsQuery query)
    {
        try
        {
            var items = await healthEventRepository.FindUpcomingAsync(query.UserId, query.Days);
            return Result<IReadOnlyCollection<HealthEvent>>.Success(items);
        }
        catch (Exception)
        {
            return Result<IReadOnlyCollection<HealthEvent>>.Failure(HealthError.VaccinationNotFound);
        }
    }

    public async Task<Result<IReadOnlyCollection<HealthEvent>>> GetOverdueHealthEventsAsync(GetOverdueHealthEventsQuery query)
    {
        try
        {
            var items = await healthEventRepository.FindOverdueAsync(query.UserId);
            return Result<IReadOnlyCollection<HealthEvent>>.Success(items);
        }
        catch (Exception)
        {
            return Result<IReadOnlyCollection<HealthEvent>>.Failure(HealthError.VaccinationNotFound);
        }
    }

    public async Task<Result<IReadOnlyCollection<HealthEvent>>> GetHealthEventsByDateRangeAsync(GetHealthEventsByDateRangeQuery query)
    {
        try
        {
            var items = await healthEventRepository.FindByDateRangeAsync(query.UserId, query.Start, query.End);
            return Result<IReadOnlyCollection<HealthEvent>>.Success(items);
        }
        catch (Exception)
        {
            return Result<IReadOnlyCollection<HealthEvent>>.Failure(HealthError.VaccinationNotFound);
        }
    }

    public async Task<Result<IReadOnlyCollection<HealthEvent>>> GetHealthEventsByTypeAsync(GetHealthEventsByTypeQuery query)
    {
        try
        {
            var items = await healthEventRepository.FindByTypeAsync(query.UserId, query.EventType);
            return Result<IReadOnlyCollection<HealthEvent>>.Success(items);
        }
        catch (Exception)
        {
            return Result<IReadOnlyCollection<HealthEvent>>.Failure(HealthError.VaccinationNotFound);
        }
    }

    public async Task<Result<IReadOnlyCollection<HealthEvent>>> GetHealthEventsByAnimalAsync(GetHealthEventsByAnimalQuery query)
    {
        try
        {
            var items = await healthEventRepository.FindByAnimalIdAsync(query.AnimalId);
            return Result<IReadOnlyCollection<HealthEvent>>.Success(items);
        }
        catch (Exception)
        {
            return Result<IReadOnlyCollection<HealthEvent>>.Failure(HealthError.VaccinationNotFound);
        }
    }

    public async Task<Result<IReadOnlyCollection<TreatmentDose>>> GetTreatmentDosesAsync(GetTreatmentDosesQuery query)
    {
        try
        {
            var items = await treatmentRepository.FindDosesByTreatmentIdAsync(query.TreatmentId);
            return Result<IReadOnlyCollection<TreatmentDose>>.Success(items);
        }
        catch (Exception)
        {
            return Result<IReadOnlyCollection<TreatmentDose>>.Failure(HealthError.TreatmentNotFound);
        }
    }

    public async Task<Result<IReadOnlyCollection<HealthAlert>>> GetAllHealthAlertsAsync(GetAllHealthAlertsQuery query)
    {
        try
        {
            var items = await healthAlertRepository.FindByFarmIdAsync(query.UserId);
            return Result<IReadOnlyCollection<HealthAlert>>.Success(items);
        }
        catch (Exception)
        {
            return Result<IReadOnlyCollection<HealthAlert>>.Failure(HealthError.HealthAlertNotFound);
        }
    }

    public async Task<Result<HealthAlert>> GetHealthAlertByIdAsync(GetHealthAlertByIdQuery query)
    {
        try
        {
            var item = await healthAlertRepository.FindByIdAsync(query.AlertId);
            return item != null
                ? Result<HealthAlert>.Success(item)
                : Result<HealthAlert>.Failure(HealthError.HealthAlertNotFound);
        }
        catch (Exception)
        {
            return Result<HealthAlert>.Failure(HealthError.HealthAlertNotFound);
        }
    }

    public async Task<Result<IReadOnlyCollection<HealthAlert>>> GetHealthAlertsByAnimalAsync(GetHealthAlertsByAnimalQuery query)
    {
        try
        {
            var items = await healthAlertRepository.FindByAnimalIdAsync(query.AnimalId);
            return Result<IReadOnlyCollection<HealthAlert>>.Success(items);
        }
        catch (Exception)
        {
            return Result<IReadOnlyCollection<HealthAlert>>.Failure(HealthError.HealthAlertNotFound);
        }
    }

    public async Task<Result<IReadOnlyCollection<HealthAlert>>> GetOpenHealthAlertsAsync(GetOpenHealthAlertsQuery query)
    {
        try
        {
            var items = await healthAlertRepository.FindOpenByFarmIdAsync(query.UserId);
            return Result<IReadOnlyCollection<HealthAlert>>.Success(items);
        }
        catch (Exception)
        {
            return Result<IReadOnlyCollection<HealthAlert>>.Failure(HealthError.HealthAlertNotFound);
        }
    }

    public async Task<Result<IReadOnlyCollection<HealthAlert>>> GetHealthAlertsByTypeAsync(GetHealthAlertsByTypeQuery query)
    {
        try
        {
            var items = await healthAlertRepository.FindByTypeAsync(query.UserId, query.AlertType);
            return Result<IReadOnlyCollection<HealthAlert>>.Success(items);
        }
        catch (Exception)
        {
            return Result<IReadOnlyCollection<HealthAlert>>.Failure(HealthError.HealthAlertNotFound);
        }
    }

    public async Task<Result<IReadOnlyCollection<HealthAlert>>> GetUnacknowledgedHealthAlertsAsync(GetUnacknowledgedHealthAlertsQuery query)
    {
        try
        {
            var items = await healthAlertRepository.FindByFarmIdAsync(query.UserId);
            var unacknowledged = items.Where(a => !a.IsResolved && a.Notes != "Acknowledged").ToList();
            return Result<IReadOnlyCollection<HealthAlert>>.Success(unacknowledged);
        }
        catch (Exception)
        {
            return Result<IReadOnlyCollection<HealthAlert>>.Failure(HealthError.HealthAlertNotFound);
        }
    }

    public async Task<Result<IReadOnlyCollection<HealthAlert>>> GetHealthAlertsByPriorityAsync(GetHealthAlertsByPriorityQuery query)
    {
        try
        {
            var items = await healthAlertRepository.FindByFarmIdAsync(query.UserId);
            var filtered = items.Where(a => a.AlertType.Contains(query.Priority, StringComparison.OrdinalIgnoreCase)).ToList();
            return Result<IReadOnlyCollection<HealthAlert>>.Success(filtered);
        }
        catch (Exception)
        {
            return Result<IReadOnlyCollection<HealthAlert>>.Failure(HealthError.HealthAlertNotFound);
        }
    }

    public async Task<Result<IReadOnlyCollection<Vaccination>>> GetOverdueVaccinationsAsync(GetOverdueVaccinationsQuery query)
    {
        try
        {
            var items = await vaccinationRepository.FindOverdueAsync(query.UserId);
            return Result<IReadOnlyCollection<Vaccination>>.Success(items);
        }
        catch (Exception)
        {
            return Result<IReadOnlyCollection<Vaccination>>.Failure(HealthError.VaccinationNotFound);
        }
    }

    public async Task<Result<IReadOnlyCollection<Treatment>>> GetTreatmentsByTypeAsync(GetTreatmentsByTypeQuery query)
    {
        try
        {
            var items = await treatmentRepository.FindByFarmIdAsync(query.UserId);
            var filtered = items.Where(t => t.Diagnosis.Contains(query.TreatmentType, StringComparison.OrdinalIgnoreCase)).ToList();
            return Result<IReadOnlyCollection<Treatment>>.Success(filtered);
        }
        catch (Exception)
        {
            return Result<IReadOnlyCollection<Treatment>>.Failure(HealthError.TreatmentNotFound);
        }
    }

    public async Task<Result<IReadOnlyCollection<Treatment>>> GetActiveTreatmentsAsync(GetActiveTreatmentsQuery query)
    {
        try
        {
            var items = await treatmentRepository.FindActiveByFarmIdAsync(query.UserId);
            return Result<IReadOnlyCollection<Treatment>>.Success(items);
        }
        catch (Exception)
        {
            return Result<IReadOnlyCollection<Treatment>>.Failure(HealthError.TreatmentNotFound);
        }
    }

    public async Task<Result<IReadOnlyCollection<Treatment>>> GetCompletedTreatmentsAsync(GetCompletedTreatmentsQuery query)
    {
        try
        {
            var items = await treatmentRepository.FindCompletedByFarmIdAsync(query.UserId);
            return Result<IReadOnlyCollection<Treatment>>.Success(items);
        }
        catch (Exception)
        {
            return Result<IReadOnlyCollection<Treatment>>.Failure(HealthError.TreatmentNotFound);
        }
    }
}
