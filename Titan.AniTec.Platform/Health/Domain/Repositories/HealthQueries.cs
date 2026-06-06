namespace Titan.AniTec.Platform.Health.Domain.Repositories;

public record GetAllVaccinationsQuery(int UserId);
public record GetVaccinationByIdQuery(int UserId, int VaccinationId);
public record GetVaccinationsByAnimalQuery(int UserId, int AnimalId);
public record GetVaccinationsByDateRangeQuery(int UserId, DateTime Start, DateTime End);
public record GetUpcomingVaccinationsQuery(int UserId);

public record GetAllTreatmentsQuery(int UserId);
public record GetTreatmentByIdQuery(int UserId, int TreatmentId);
public record GetTreatmentsByAnimalQuery(int UserId, int AnimalId);
public record GetTreatmentsByDateRangeQuery(int UserId, DateTime Start, DateTime End);

public record GetAllVeterinaryVisitsQuery(int UserId);
public record GetVeterinaryVisitByIdQuery(int UserId, int VisitId);
public record GetVeterinaryVisitsByAnimalQuery(int UserId, int AnimalId);
public record GetVeterinaryVisitsByDateRangeQuery(int UserId, DateTime Start, DateTime End);

public record GetAllVaccinesQuery();
public record GetVaccineByIdQuery(int VaccineId);
public record SearchVaccinesQuery(string SearchTerm);
public record GetVaccinesByDiseaseQuery(int DiseaseId);
public record GetVaccinesByRaceQuery(int RaceId);

public record GetAllMedicationsQuery();
public record GetMedicationByIdQuery(int MedicationId);
public record SearchMedicationsQuery(string SearchTerm);
public record GetMedicationsByCategoryQuery(string Category);

public record GetAllDiseasesQuery();
public record GetDiseaseByIdQuery(int DiseaseId);
public record GetDiseasesByRaceQuery(int RaceId);
public record GetDiseasesBySymptomsQuery(string Symptoms);

public record GetAllDiagnosesQuery(int UserId);
public record GetDiagnosisByIdQuery(int UserId, int DiagnosisId);
public record GetDiagnosesByAnimalQuery(int UserId, int AnimalId);
public record GetDiagnosesByDiseaseQuery(int UserId, int DiseaseId);
public record GetRecentDiagnosesQuery(int UserId);

public record GetHealthCalendarQuery(int UserId);
public record GetAllHealthEventsQuery(int UserId);
public record GetHealthEventByIdQuery(int UserId, int EventId);
public record GetTodaysHealthEventsQuery(int UserId);
public record GetUpcomingHealthEventsQuery(int UserId, int Days);
public record GetOverdueHealthEventsQuery(int UserId);
public record GetHealthEventsByDateRangeQuery(int UserId, DateTime Start, DateTime End);
public record GetHealthEventsByTypeQuery(int UserId, string EventType);
public record GetHealthEventsByAnimalQuery(int UserId, int AnimalId);

public record GetTreatmentDosesQuery(int UserId, int TreatmentId);

public record GetAllHealthAlertsQuery(int UserId);
public record GetHealthAlertByIdQuery(int UserId, int AlertId);
public record GetHealthAlertsByAnimalQuery(int UserId, int AnimalId);
public record GetOpenHealthAlertsQuery(int UserId);
public record GetHealthAlertsByTypeQuery(int UserId, string AlertType);
public record GetUnacknowledgedHealthAlertsQuery(int UserId);
public record GetHealthAlertsByPriorityQuery(int UserId, string Priority);

public record GetOverdueVaccinationsQuery(int UserId);
public record GetTreatmentsByTypeQuery(int UserId, string TreatmentType);
public record GetActiveTreatmentsQuery(int UserId);
public record GetCompletedTreatmentsQuery(int UserId);
