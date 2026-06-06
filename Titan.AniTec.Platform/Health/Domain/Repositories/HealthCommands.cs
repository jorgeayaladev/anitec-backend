namespace Titan.AniTec.Platform.Health.Domain.Repositories;

public record RegisterVaccinationCommand(int UserId, int AnimalId, string VaccineName, DateTime ApplicationDate,
    string? BatchNumber, string? ApplicationRoute, string? Dosage, string? AppliedBy,
    DateTime? NextDoseDate, string? Notes);

public record UpdateVaccinationCommand(int UserId, int VaccinationId, string VaccineName, DateTime ApplicationDate,
    string? BatchNumber, string? ApplicationRoute, string? Dosage, string? AppliedBy,
    DateTime? NextDoseDate, string? Notes);

public record DeleteVaccinationCommand(int UserId, int VaccinationId);

public record RegisterTreatmentCommand(int UserId, int AnimalId, DateTime TreatmentDate, string Diagnosis,
    string? MedicationName, string? Dosage, string? AdministrationRoute,
    int? DurationDays, int? WithdrawalPeriodDays, string? TreatedBy, string? Notes);

public record UpdateTreatmentCommand(int UserId, int TreatmentId, DateTime TreatmentDate, string Diagnosis,
    string? MedicationName, string? Dosage, string? AdministrationRoute,
    int? DurationDays, int? WithdrawalPeriodDays, string? TreatedBy, string? Notes);

public record DeleteTreatmentCommand(int UserId, int TreatmentId);
public record StartTreatmentCommand(int UserId, int TreatmentId);
public record CompleteTreatmentCommand(int UserId, int TreatmentId);
public record CancelTreatmentCommand(int UserId, int TreatmentId);

public record RegisterVeterinaryVisitCommand(int UserId, int AnimalId, DateTime VisitDate, string VetName,
    string Reason, string? Diagnosis, string? Recommendations,
    DateTime? FollowUpDate, decimal? Cost, string? Notes);

public record UpdateVeterinaryVisitCommand(int UserId, int VisitId, DateTime VisitDate, string VetName,
    string Reason, string? Diagnosis, string? Recommendations,
    DateTime? FollowUpDate, decimal? Cost, string? Notes);

public record DeleteVeterinaryVisitCommand(int UserId, int VisitId);

public record RegisterVaccineCommand(string Name, string? Description, string? Manufacturer, int? DurationDays);
public record UpdateVaccineCommand(int VaccineId, string Name, string? Description, string? Manufacturer, int? DurationDays);
public record DeleteVaccineCommand(int VaccineId);

public record RegisterMedicationCommand(string Name, string? Description, string? Category, string? Manufacturer,
    string? ActiveIngredient, string? Presentation, string? DosageForm);
public record UpdateMedicationCommand(int MedicationId, string Name, string? Description, string? Category, string? Manufacturer,
    string? ActiveIngredient, string? Presentation, string? DosageForm);
public record DeleteMedicationCommand(int MedicationId);

public record RegisterDiseaseCommand(string Name, string? Description, string? Category, string? Symptoms, bool? IsContagious);
public record UpdateDiseaseCommand(int DiseaseId, string Name, string? Description, string? Category, string? Symptoms, bool? IsContagious);
public record DeleteDiseaseCommand(int DiseaseId);

public record RegisterDiagnosisCommand(int UserId, int AnimalId, string Condition, string? Description,
    string? Severity, string? DiagnosedBy, DateTime DiagnosedDate, int? DiseaseId);
public record UpdateDiagnosisCommand(int UserId, int DiagnosisId, string Condition, string? Description,
    string? Severity, string? DiagnosedBy, DateTime DiagnosedDate, int? DiseaseId);
public record DeleteDiagnosisCommand(int UserId, int DiagnosisId);

public record RegisterHealthEventCommand(int UserId, int? AnimalId, string Title, string? Description,
    string EventType, DateTime ScheduledDate, string? Notes);
public record UpdateHealthEventCommand(int UserId, int EventId, string Title, string? Description,
    string EventType, DateTime ScheduledDate, int? AnimalId, string? Notes);
public record DeleteHealthEventCommand(int UserId, int EventId);
public record CompleteHealthEventCommand(int UserId, int EventId, string? CompletedBy);
public record PostponeHealthEventCommand(int UserId, int EventId, DateTime PostponedTo);
public record RegisterHealthEventBatchCommand(int UserId, IReadOnlyCollection<RegisterHealthEventCommand> Events);

public record RegisterTreatmentDoseCommand(int UserId, int TreatmentId, DateTime AdministeredDate,
    string? AdministeredBy, string? Dosage, string? Notes);

public record RegisterHealthAlertCommand(int UserId, int AnimalId, string AlertType, string Description);

public record ResolveHealthAlertCommand(int UserId, int AlertId, string? ResolvedBy, string? Notes);

public record DismissHealthAlertCommand(int UserId, int AlertId);

public record AcknowledgeHealthAlertCommand(int UserId, int AlertId, string? AcknowledgedBy);
