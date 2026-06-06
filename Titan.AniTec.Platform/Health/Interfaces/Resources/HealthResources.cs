namespace Titan.AniTec.Platform.Health.Interfaces.Resources;

public record VaccineResource(int Id, string Name, string? Description, string? Manufacturer, int? DurationDays);
public record CreateVaccineResource(string Name, string? Description, string? Manufacturer, int? DurationDays);
public record UpdateVaccineResource(string Name, string? Description, string? Manufacturer, int? DurationDays);

public record MedicationResource(int Id, string Name, string? Description, string? Category, string? Manufacturer,
    string? ActiveIngredient, string? Presentation, string? DosageForm);
public record CreateMedicationResource(string Name, string? Description, string? Category, string? Manufacturer,
    string? ActiveIngredient, string? Presentation, string? DosageForm);
public record UpdateMedicationResource(string Name, string? Description, string? Category, string? Manufacturer,
    string? ActiveIngredient, string? Presentation, string? DosageForm);

public record DiseaseResource(int Id, string Name, string? Description, string? Category, string? Symptoms, bool? IsContagious);
public record CreateDiseaseResource(string Name, string? Description, string? Category, string? Symptoms, bool? IsContagious);
public record UpdateDiseaseResource(string Name, string? Description, string? Category, string? Symptoms, bool? IsContagious);

public record DiagnosisResource(int Id, int FarmId, int AnimalId, string Condition, string? Description,
    string? Severity, string? DiagnosedBy, DateTime DiagnosedDate, int? DiseaseId);
public record CreateDiagnosisResource(int AnimalId, string Condition, string? Description,
    string? Severity, string? DiagnosedBy, DateTime DiagnosedDate, int? DiseaseId);
public record UpdateDiagnosisResource(string Condition, string? Description,
    string? Severity, string? DiagnosedBy, DateTime DiagnosedDate, int? DiseaseId);

public record VaccinationResource(
    int Id, int FarmId, int AnimalId, string VaccineName, DateTime ApplicationDate,
    string? BatchNumber, string? ApplicationRoute, string? Dosage, string? AppliedBy,
    DateTime? NextDoseDate, string? Notes);

public record CreateVaccinationResource(
    int AnimalId, string VaccineName, DateTime ApplicationDate,
    string? BatchNumber, string? ApplicationRoute, string? Dosage, string? AppliedBy,
    DateTime? NextDoseDate, string? Notes);

public record UpdateVaccinationResource(
    string VaccineName, DateTime ApplicationDate,
    string? BatchNumber, string? ApplicationRoute, string? Dosage, string? AppliedBy,
    DateTime? NextDoseDate, string? Notes);

public record TreatmentResource(
    int Id, int FarmId, int AnimalId, DateTime TreatmentDate, string Diagnosis,
    string? MedicationName, string? Dosage, string? AdministrationRoute,
    int? DurationDays, int? WithdrawalPeriodDays, string? TreatedBy, string? Notes, string Status);

public record CreateTreatmentResource(
    int AnimalId, DateTime TreatmentDate, string Diagnosis,
    string? MedicationName, string? Dosage, string? AdministrationRoute,
    int? DurationDays, int? WithdrawalPeriodDays, string? TreatedBy, string? Notes);

public record UpdateTreatmentResource(
    DateTime TreatmentDate, string Diagnosis,
    string? MedicationName, string? Dosage, string? AdministrationRoute,
    int? DurationDays, int? WithdrawalPeriodDays, string? TreatedBy, string? Notes);

public record VeterinaryVisitResource(
    int Id, int FarmId, int AnimalId, DateTime VisitDate, string VetName,
    string Reason, string? Diagnosis, string? Recommendations,
    DateTime? FollowUpDate, decimal? Cost, string? Notes);

public record CreateVeterinaryVisitResource(
    int AnimalId, DateTime VisitDate, string VetName,
    string Reason, string? Diagnosis, string? Recommendations,
    DateTime? FollowUpDate, decimal? Cost, string? Notes);

public record UpdateVeterinaryVisitResource(
    DateTime VisitDate, string VetName,
    string Reason, string? Diagnosis, string? Recommendations,
    DateTime? FollowUpDate, decimal? Cost, string? Notes);

public record HealthAlertResource(
    int Id, int FarmId, int AnimalId, string AlertType, string Description,
    bool IsResolved, DateTime? ResolvedAt, string? ResolvedBy, string? Notes);

public record CreateHealthAlertResource(int AnimalId, string AlertType, string Description);

public record ResolveHealthAlertResource(string? ResolvedBy, string? Notes);

public record HealthEventResource(int Id, int FarmId, int? AnimalId, string Title, string? Description,
    string EventType, DateTime ScheduledDate, bool IsCompleted, DateTime? CompletedAt,
    string? CompletedBy, bool IsPostponed, DateTime? PostponedTo, string? Notes);
public record CreateHealthEventResource(int? AnimalId, string Title, string? Description,
    string EventType, DateTime ScheduledDate, string? Notes);
public record UpdateHealthEventResource(string Title, string? Description,
    string EventType, DateTime ScheduledDate, int? AnimalId, string? Notes);
public record CompleteHealthEventResource(string? CompletedBy);
public record PostponeHealthEventResource(DateTime PostponedTo);

public record TreatmentDoseResource(int Id, int TreatmentId, DateTime AdministeredDate,
    string? AdministeredBy, string? Dosage, string? Notes);
public record CreateTreatmentDoseResource(DateTime AdministeredDate,
    string? AdministeredBy, string? Dosage, string? Notes);
