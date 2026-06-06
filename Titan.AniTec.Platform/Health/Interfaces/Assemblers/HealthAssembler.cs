using Titan.AniTec.Platform.Health.Domain.Model.Aggregates;
using Titan.AniTec.Platform.Health.Domain.Repositories;
using Titan.AniTec.Platform.Health.Interfaces.Resources;
using Titan.AniTec.Platform.Shared.Application.Model;
using Microsoft.AspNetCore.Mvc;

namespace Titan.AniTec.Platform.Health.Interfaces.Assemblers;

public static class HealthAssembler
{
    public static RegisterVaccineCommand ToCommand(CreateVaccineResource resource)
        => new(resource.Name, resource.Description, resource.Manufacturer, resource.DurationDays);

    public static UpdateVaccineCommand ToCommand(int vaccineId, UpdateVaccineResource resource)
        => new(vaccineId, resource.Name, resource.Description, resource.Manufacturer, resource.DurationDays);

    public static VaccineResource ToResource(Vaccine entity)
        => new(entity.Id, entity.Name, entity.Description, entity.Manufacturer, entity.DurationDays);

    public static RegisterMedicationCommand ToCommand(CreateMedicationResource resource)
        => new(resource.Name, resource.Description, resource.Category, resource.Manufacturer,
            resource.ActiveIngredient, resource.Presentation, resource.DosageForm);

    public static UpdateMedicationCommand ToCommand(int medicationId, UpdateMedicationResource resource)
        => new(medicationId, resource.Name, resource.Description, resource.Category, resource.Manufacturer,
            resource.ActiveIngredient, resource.Presentation, resource.DosageForm);

    public static MedicationResource ToResource(Medication entity)
        => new(entity.Id, entity.Name, entity.Description, entity.Category, entity.Manufacturer,
            entity.ActiveIngredient, entity.Presentation, entity.DosageForm);

    public static RegisterDiseaseCommand ToCommand(CreateDiseaseResource resource)
        => new(resource.Name, resource.Description, resource.Category, resource.Symptoms, resource.IsContagious);

    public static UpdateDiseaseCommand ToCommand(int diseaseId, UpdateDiseaseResource resource)
        => new(diseaseId, resource.Name, resource.Description, resource.Category, resource.Symptoms, resource.IsContagious);

    public static DiseaseResource ToResource(Disease entity)
        => new(entity.Id, entity.Name, entity.Description, entity.Category, entity.Symptoms, entity.IsContagious);

    public static RegisterDiagnosisCommand ToCommand(int userId, CreateDiagnosisResource resource)
        => new(userId, resource.AnimalId, resource.Condition, resource.Description,
            resource.Severity, resource.DiagnosedBy, resource.DiagnosedDate, resource.DiseaseId);

    public static UpdateDiagnosisCommand ToCommand(int userId, int diagnosisId, UpdateDiagnosisResource resource)
        => new(userId, diagnosisId, resource.Condition, resource.Description,
            resource.Severity, resource.DiagnosedBy, resource.DiagnosedDate, resource.DiseaseId);

    public static DiagnosisResource ToResource(Diagnosis entity)
        => new(entity.Id, entity.FarmId, entity.AnimalId, entity.Condition, entity.Description,
            entity.Severity, entity.DiagnosedBy, entity.DiagnosedDate, entity.DiseaseId);

    public static RegisterHealthEventCommand ToCommand(int userId, CreateHealthEventResource resource)
        => new(userId, resource.AnimalId, resource.Title, resource.Description,
            resource.EventType, resource.ScheduledDate, resource.Notes);

    public static UpdateHealthEventCommand ToCommand(int userId, int eventId, UpdateHealthEventResource resource)
        => new(userId, eventId, resource.Title, resource.Description,
            resource.EventType, resource.ScheduledDate, resource.AnimalId, resource.Notes);

    public static HealthEventResource ToResource(HealthEvent entity)
        => new(entity.Id, entity.FarmId, entity.AnimalId, entity.Title, entity.Description,
            entity.EventType, entity.ScheduledDate, entity.IsCompleted, entity.CompletedAt,
            entity.CompletedBy, entity.IsPostponed, entity.PostponedTo, entity.Notes);

    public static RegisterTreatmentDoseCommand ToCommand(int userId, int treatmentId, CreateTreatmentDoseResource resource)
        => new(userId, treatmentId, resource.AdministeredDate, resource.AdministeredBy, resource.Dosage, resource.Notes);

    public static TreatmentDoseResource ToResource(TreatmentDose entity)
        => new(entity.Id, entity.TreatmentId, entity.AdministeredDate,
            entity.AdministeredBy, entity.Dosage, entity.Notes);
    public static RegisterVaccinationCommand ToCommand(int userId, CreateVaccinationResource resource)
        => new(userId, resource.AnimalId, resource.VaccineName, resource.ApplicationDate,
            resource.BatchNumber, resource.ApplicationRoute, resource.Dosage,
            resource.AppliedBy, resource.NextDoseDate, resource.Notes);

    public static UpdateVaccinationCommand ToCommand(int userId, int vaccinationId, UpdateVaccinationResource resource)
        => new(userId, vaccinationId, resource.VaccineName, resource.ApplicationDate,
            resource.BatchNumber, resource.ApplicationRoute, resource.Dosage,
            resource.AppliedBy, resource.NextDoseDate, resource.Notes);

    public static VaccinationResource ToResource(Vaccination entity)
        => new(entity.Id, entity.FarmId, entity.AnimalId, entity.VaccineName, entity.ApplicationDate,
            entity.BatchNumber, entity.ApplicationRoute, entity.Dosage, entity.AppliedBy,
            entity.NextDoseDate, entity.Notes);

    public static RegisterTreatmentCommand ToCommand(int userId, CreateTreatmentResource resource)
        => new(userId, resource.AnimalId, resource.TreatmentDate, resource.Diagnosis,
            resource.MedicationName, resource.Dosage, resource.AdministrationRoute,
            resource.DurationDays, resource.WithdrawalPeriodDays, resource.TreatedBy, resource.Notes);

    public static UpdateTreatmentCommand ToCommand(int userId, int treatmentId, UpdateTreatmentResource resource)
        => new(userId, treatmentId, resource.TreatmentDate, resource.Diagnosis,
            resource.MedicationName, resource.Dosage, resource.AdministrationRoute,
            resource.DurationDays, resource.WithdrawalPeriodDays, resource.TreatedBy, resource.Notes);

    public static TreatmentResource ToResource(Treatment entity)
        => new(entity.Id, entity.FarmId, entity.AnimalId, entity.TreatmentDate, entity.Diagnosis,
            entity.MedicationName, entity.Dosage, entity.AdministrationRoute,
            entity.DurationDays, entity.WithdrawalPeriodDays, entity.TreatedBy, entity.Notes, entity.Status);

    public static RegisterVeterinaryVisitCommand ToCommand(int userId, CreateVeterinaryVisitResource resource)
        => new(userId, resource.AnimalId, resource.VisitDate, resource.VetName,
            resource.Reason, resource.Diagnosis, resource.Recommendations,
            resource.FollowUpDate, resource.Cost, resource.Notes);

    public static UpdateVeterinaryVisitCommand ToCommand(int userId, int visitId, UpdateVeterinaryVisitResource resource)
        => new(userId, visitId, resource.VisitDate, resource.VetName,
            resource.Reason, resource.Diagnosis, resource.Recommendations,
            resource.FollowUpDate, resource.Cost, resource.Notes);

    public static VeterinaryVisitResource ToResource(VeterinaryVisit entity)
        => new(entity.Id, entity.FarmId, entity.AnimalId, entity.VisitDate, entity.VetName,
            entity.Reason, entity.Diagnosis, entity.Recommendations,
            entity.FollowUpDate, entity.Cost, entity.Notes);

    public static RegisterHealthAlertCommand ToCommand(int userId, CreateHealthAlertResource resource)
        => new(userId, resource.AnimalId, resource.AlertType, resource.Description);

    public static HealthAlertResource ToResource(HealthAlert entity)
        => new(entity.Id, entity.FarmId, entity.AnimalId, entity.AlertType, entity.Description,
            entity.IsResolved, entity.ResolvedAt, entity.ResolvedBy, entity.Notes);
}

public static class HealthActionResultAssembler
{
    public static IActionResult ToActionResult<T>(Result<T> result)
    {
        if (result.IsSuccess)
            return new OkObjectResult(result.Data);

        return result.Error switch
        {
            Domain.Model.HealthError.VaccinationNotFound => new NotFoundResult(),
            Domain.Model.HealthError.TreatmentNotFound => new NotFoundResult(),
            Domain.Model.HealthError.VeterinaryVisitNotFound => new NotFoundResult(),
            Domain.Model.HealthError.HealthAlertNotFound => new NotFoundResult(),
            _ => new BadRequestObjectResult(new ProblemDetails
            {
                Title = "Bad Request",
                Detail = result.Error?.ToString() ?? "Unknown error",
                Status = 400
            })
        };
    }

    public static IActionResult ToActionResult(Result result)
    {
        if (result.IsSuccess)
            return new OkResult();

        return result.Error switch
        {
            Domain.Model.HealthError.VaccinationNotFound => new NotFoundResult(),
            Domain.Model.HealthError.TreatmentNotFound => new NotFoundResult(),
            Domain.Model.HealthError.VeterinaryVisitNotFound => new NotFoundResult(),
            Domain.Model.HealthError.HealthAlertNotFound => new NotFoundResult(),
            _ => new BadRequestObjectResult(new ProblemDetails
            {
                Title = "Bad Request",
                Detail = result.Error?.ToString() ?? "Unknown error",
                Status = 400
            })
        };
    }

    public static IActionResult ToCreatedActionResult<T>(Result<T> result)
    {
        if (result.IsSuccess)
            return new CreatedResult(string.Empty, result.Value);

        return ToActionResult(result);
    }
}
