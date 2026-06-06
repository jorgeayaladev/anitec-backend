using Titan.AniTec.Platform.Health.Domain.Model;
using Titan.AniTec.Platform.Health.Domain.Model.Aggregates;
using Titan.AniTec.Platform.Health.Domain.Repositories;
using Titan.AniTec.Platform.Health.Application.CommandServices;
using Titan.AniTec.Platform.Shared.Application.Model;
using Titan.AniTec.Platform.Shared.Domain.Repositories;

namespace Titan.AniTec.Platform.Health.Application.Internal.CommandServices;

public class HealthCommandService(
    IVaccinationRepository vaccinationRepository,
    ITreatmentRepository treatmentRepository,
    IVeterinaryVisitRepository veterinaryVisitRepository,
    IHealthAlertRepository healthAlertRepository,
    IHealthEventRepository healthEventRepository,
    IVaccineRepository vaccineRepository,
    IMedicationRepository medicationRepository,
    IDiseaseRepository diseaseRepository,
    IDiagnosisRepository diagnosisRepository,
    IUnitOfWork unitOfWork) : IHealthCommandService
{
    public async Task<Result<Vaccination>> RegisterVaccinationAsync(RegisterVaccinationCommand command)
    {
        try
        {
            var vaccination = new Vaccination(command.UserId, command.AnimalId, command.VaccineName,
                command.ApplicationDate, command.BatchNumber, command.ApplicationRoute,
                command.Dosage, command.AppliedBy, command.NextDoseDate, command.Notes);
            await vaccinationRepository.AddAsync(vaccination);
            await unitOfWork.CompleteAsync();
            return Result<Vaccination>.Success(vaccination);
        }
        catch (Exception)
        {
            return Result<Vaccination>.Failure(HealthError.InvalidVaccinationData);
        }
    }

    public async Task<Result<Vaccination>> UpdateVaccinationAsync(UpdateVaccinationCommand command)
    {
        try
        {
            var vaccination = await vaccinationRepository.FindByIdAsync(command.VaccinationId);
            if (vaccination == null)
                return Result<Vaccination>.Failure(HealthError.VaccinationNotFound);

            vaccination.UpdateDetails(command.VaccineName, command.ApplicationDate,
                command.BatchNumber, command.ApplicationRoute, command.Dosage,
                command.AppliedBy, command.NextDoseDate, command.Notes);
            await unitOfWork.CompleteAsync();
            return Result<Vaccination>.Success(vaccination);
        }
        catch (Exception)
        {
            return Result<Vaccination>.Failure(HealthError.InvalidVaccinationData);
        }
    }

    public async Task<Result> DeleteVaccinationAsync(DeleteVaccinationCommand command)
    {
        try
        {
            var vaccination = await vaccinationRepository.FindByIdAsync(command.VaccinationId);
            if (vaccination == null)
                return Result.Failure(HealthError.VaccinationNotFound);

            vaccinationRepository.Remove(vaccination);
            await unitOfWork.CompleteAsync();
            return Result.Success();
        }
        catch (Exception)
        {
            return Result.Failure(HealthError.VaccinationNotFound);
        }
    }

    public async Task<Result<Treatment>> RegisterTreatmentAsync(RegisterTreatmentCommand command)
    {
        try
        {
            var treatment = new Treatment(command.UserId, command.AnimalId, command.TreatmentDate,
                command.Diagnosis, command.MedicationName, command.Dosage,
                command.AdministrationRoute, command.DurationDays, command.WithdrawalPeriodDays,
                command.TreatedBy, command.Notes);
            await treatmentRepository.AddAsync(treatment);
            await unitOfWork.CompleteAsync();
            return Result<Treatment>.Success(treatment);
        }
        catch (Exception)
        {
            return Result<Treatment>.Failure(HealthError.InvalidTreatmentData);
        }
    }

    public async Task<Result<Treatment>> UpdateTreatmentAsync(UpdateTreatmentCommand command)
    {
        try
        {
            var treatment = await treatmentRepository.FindByIdAsync(command.TreatmentId);
            if (treatment == null)
                return Result<Treatment>.Failure(HealthError.TreatmentNotFound);

            treatment.UpdateDetails(command.TreatmentDate, command.Diagnosis,
                command.MedicationName, command.Dosage, command.AdministrationRoute,
                command.DurationDays, command.WithdrawalPeriodDays, command.TreatedBy, command.Notes);
            await unitOfWork.CompleteAsync();
            return Result<Treatment>.Success(treatment);
        }
        catch (Exception)
        {
            return Result<Treatment>.Failure(HealthError.InvalidTreatmentData);
        }
    }

    public async Task<Result> DeleteTreatmentAsync(DeleteTreatmentCommand command)
    {
        try
        {
            var treatment = await treatmentRepository.FindByIdAsync(command.TreatmentId);
            if (treatment == null)
                return Result.Failure(HealthError.TreatmentNotFound);

            treatmentRepository.Remove(treatment);
            await unitOfWork.CompleteAsync();
            return Result.Success();
        }
        catch (Exception)
        {
            return Result.Failure(HealthError.TreatmentNotFound);
        }
    }

    public async Task<Result<Treatment>> StartTreatmentAsync(StartTreatmentCommand command)
    {
        try
        {
            var treatment = await treatmentRepository.FindByIdAsync(command.TreatmentId);
            if (treatment == null)
                return Result<Treatment>.Failure(HealthError.TreatmentNotFound);

            treatment.MarkAsStarted();
            await unitOfWork.CompleteAsync();
            return Result<Treatment>.Success(treatment);
        }
        catch (Exception)
        {
            return Result<Treatment>.Failure(HealthError.InvalidTreatmentData);
        }
    }

    public async Task<Result<Treatment>> CompleteTreatmentAsync(CompleteTreatmentCommand command)
    {
        try
        {
            var treatment = await treatmentRepository.FindByIdAsync(command.TreatmentId);
            if (treatment == null)
                return Result<Treatment>.Failure(HealthError.TreatmentNotFound);

            treatment.MarkAsCompleted();
            await unitOfWork.CompleteAsync();
            return Result<Treatment>.Success(treatment);
        }
        catch (Exception)
        {
            return Result<Treatment>.Failure(HealthError.InvalidTreatmentData);
        }
    }

    public async Task<Result<Treatment>> CancelTreatmentAsync(CancelTreatmentCommand command)
    {
        try
        {
            var treatment = await treatmentRepository.FindByIdAsync(command.TreatmentId);
            if (treatment == null)
                return Result<Treatment>.Failure(HealthError.TreatmentNotFound);

            treatment.MarkAsCancelled();
            await unitOfWork.CompleteAsync();
            return Result<Treatment>.Success(treatment);
        }
        catch (Exception)
        {
            return Result<Treatment>.Failure(HealthError.InvalidTreatmentData);
        }
    }

    public async Task<Result<VeterinaryVisit>> RegisterVeterinaryVisitAsync(RegisterVeterinaryVisitCommand command)
    {
        try
        {
            var visit = new VeterinaryVisit(command.UserId, command.AnimalId, command.VisitDate,
                command.VetName, command.Reason, command.Diagnosis, command.Recommendations,
                command.FollowUpDate, command.Cost, command.Notes);
            await veterinaryVisitRepository.AddAsync(visit);
            await unitOfWork.CompleteAsync();
            return Result<VeterinaryVisit>.Success(visit);
        }
        catch (Exception)
        {
            return Result<VeterinaryVisit>.Failure(HealthError.InvalidVeterinaryVisitData);
        }
    }

    public async Task<Result<VeterinaryVisit>> UpdateVeterinaryVisitAsync(UpdateVeterinaryVisitCommand command)
    {
        try
        {
            var visit = await veterinaryVisitRepository.FindByIdAsync(command.VisitId);
            if (visit == null)
                return Result<VeterinaryVisit>.Failure(HealthError.VeterinaryVisitNotFound);

            visit.UpdateDetails(command.VisitDate, command.VetName, command.Reason,
                command.Diagnosis, command.Recommendations, command.FollowUpDate,
                command.Cost, command.Notes);
            await unitOfWork.CompleteAsync();
            return Result<VeterinaryVisit>.Success(visit);
        }
        catch (Exception)
        {
            return Result<VeterinaryVisit>.Failure(HealthError.InvalidVeterinaryVisitData);
        }
    }

    public async Task<Result> DeleteVeterinaryVisitAsync(DeleteVeterinaryVisitCommand command)
    {
        try
        {
            var visit = await veterinaryVisitRepository.FindByIdAsync(command.VisitId);
            if (visit == null)
                return Result.Failure(HealthError.VeterinaryVisitNotFound);

            veterinaryVisitRepository.Remove(visit);
            await unitOfWork.CompleteAsync();
            return Result.Success();
        }
        catch (Exception)
        {
            return Result.Failure(HealthError.VeterinaryVisitNotFound);
        }
    }

    public async Task<Result<Vaccine>> RegisterVaccineAsync(RegisterVaccineCommand command)
    {
        try
        {
            var vaccine = new Vaccine(command.Name, command.Description, command.Manufacturer, command.DurationDays);
            await vaccineRepository.AddAsync(vaccine);
            await unitOfWork.CompleteAsync();
            return Result<Vaccine>.Success(vaccine);
        }
        catch (Exception)
        {
            return Result<Vaccine>.Failure(HealthError.InvalidVaccinationData);
        }
    }

    public async Task<Result<Vaccine>> UpdateVaccineAsync(UpdateVaccineCommand command)
    {
        try
        {
            var vaccine = await vaccineRepository.FindByIdAsync(command.VaccineId);
            if (vaccine == null)
                return Result<Vaccine>.Failure(HealthError.VaccinationNotFound);
            vaccine.UpdateDetails(command.Name, command.Description, command.Manufacturer, command.DurationDays);
            await unitOfWork.CompleteAsync();
            return Result<Vaccine>.Success(vaccine);
        }
        catch (Exception)
        {
            return Result<Vaccine>.Failure(HealthError.InvalidVaccinationData);
        }
    }

    public async Task<Result> DeleteVaccineAsync(DeleteVaccineCommand command)
    {
        try
        {
            var vaccine = await vaccineRepository.FindByIdAsync(command.VaccineId);
            if (vaccine == null)
                return Result.Failure(HealthError.VaccinationNotFound);
            vaccineRepository.Remove(vaccine);
            await unitOfWork.CompleteAsync();
            return Result.Success();
        }
        catch (Exception)
        {
            return Result.Failure(HealthError.VaccinationNotFound);
        }
    }

    public async Task<Result<Medication>> RegisterMedicationAsync(RegisterMedicationCommand command)
    {
        try
        {
            var medication = new Medication(command.Name, command.Description, command.Category, command.Manufacturer,
                command.ActiveIngredient, command.Presentation, command.DosageForm);
            await medicationRepository.AddAsync(medication);
            await unitOfWork.CompleteAsync();
            return Result<Medication>.Success(medication);
        }
        catch (Exception)
        {
            return Result<Medication>.Failure(HealthError.InvalidTreatmentData);
        }
    }

    public async Task<Result<Medication>> UpdateMedicationAsync(UpdateMedicationCommand command)
    {
        try
        {
            var medication = await medicationRepository.FindByIdAsync(command.MedicationId);
            if (medication == null)
                return Result<Medication>.Failure(HealthError.TreatmentNotFound);
            medication.UpdateDetails(command.Name, command.Description, command.Category, command.Manufacturer,
                command.ActiveIngredient, command.Presentation, command.DosageForm);
            await unitOfWork.CompleteAsync();
            return Result<Medication>.Success(medication);
        }
        catch (Exception)
        {
            return Result<Medication>.Failure(HealthError.InvalidTreatmentData);
        }
    }

    public async Task<Result> DeleteMedicationAsync(DeleteMedicationCommand command)
    {
        try
        {
            var medication = await medicationRepository.FindByIdAsync(command.MedicationId);
            if (medication == null)
                return Result.Failure(HealthError.TreatmentNotFound);
            medicationRepository.Remove(medication);
            await unitOfWork.CompleteAsync();
            return Result.Success();
        }
        catch (Exception)
        {
            return Result.Failure(HealthError.TreatmentNotFound);
        }
    }

    public async Task<Result<Disease>> RegisterDiseaseAsync(RegisterDiseaseCommand command)
    {
        try
        {
            var disease = new Disease(command.Name, command.Description, command.Category, command.Symptoms, command.IsContagious);
            await diseaseRepository.AddAsync(disease);
            await unitOfWork.CompleteAsync();
            return Result<Disease>.Success(disease);
        }
        catch (Exception)
        {
            return Result<Disease>.Failure(HealthError.InvalidVeterinaryVisitData);
        }
    }

    public async Task<Result<Disease>> UpdateDiseaseAsync(UpdateDiseaseCommand command)
    {
        try
        {
            var disease = await diseaseRepository.FindByIdAsync(command.DiseaseId);
            if (disease == null)
                return Result<Disease>.Failure(HealthError.VeterinaryVisitNotFound);
            disease.UpdateDetails(command.Name, command.Description, command.Category, command.Symptoms, command.IsContagious);
            await unitOfWork.CompleteAsync();
            return Result<Disease>.Success(disease);
        }
        catch (Exception)
        {
            return Result<Disease>.Failure(HealthError.InvalidVeterinaryVisitData);
        }
    }

    public async Task<Result> DeleteDiseaseAsync(DeleteDiseaseCommand command)
    {
        try
        {
            var disease = await diseaseRepository.FindByIdAsync(command.DiseaseId);
            if (disease == null)
                return Result.Failure(HealthError.VeterinaryVisitNotFound);
            diseaseRepository.Remove(disease);
            await unitOfWork.CompleteAsync();
            return Result.Success();
        }
        catch (Exception)
        {
            return Result.Failure(HealthError.VeterinaryVisitNotFound);
        }
    }

    public async Task<Result<Diagnosis>> RegisterDiagnosisAsync(RegisterDiagnosisCommand command)
    {
        try
        {
            var diagnosis = new Diagnosis(command.UserId, command.AnimalId, command.Condition, command.Description,
                command.Severity, command.DiagnosedBy, command.DiagnosedDate, command.DiseaseId);
            await diagnosisRepository.AddAsync(diagnosis);
            await unitOfWork.CompleteAsync();
            return Result<Diagnosis>.Success(diagnosis);
        }
        catch (Exception)
        {
            return Result<Diagnosis>.Failure(HealthError.InvalidVeterinaryVisitData);
        }
    }

    public async Task<Result<Diagnosis>> UpdateDiagnosisAsync(UpdateDiagnosisCommand command)
    {
        try
        {
            var diagnosis = await diagnosisRepository.FindByIdAsync(command.DiagnosisId);
            if (diagnosis == null)
                return Result<Diagnosis>.Failure(HealthError.VeterinaryVisitNotFound);
            diagnosis.UpdateDetails(command.Condition, command.Description,
                command.Severity, command.DiagnosedBy, command.DiagnosedDate, command.DiseaseId);
            await unitOfWork.CompleteAsync();
            return Result<Diagnosis>.Success(diagnosis);
        }
        catch (Exception)
        {
            return Result<Diagnosis>.Failure(HealthError.InvalidVeterinaryVisitData);
        }
    }

    public async Task<Result> DeleteDiagnosisAsync(DeleteDiagnosisCommand command)
    {
        try
        {
            var diagnosis = await diagnosisRepository.FindByIdAsync(command.DiagnosisId);
            if (diagnosis == null)
                return Result.Failure(HealthError.VeterinaryVisitNotFound);
            diagnosisRepository.Remove(diagnosis);
            await unitOfWork.CompleteAsync();
            return Result.Success();
        }
        catch (Exception)
        {
            return Result.Failure(HealthError.VeterinaryVisitNotFound);
        }
    }

    public async Task<Result<HealthEvent>> RegisterHealthEventAsync(RegisterHealthEventCommand command)
    {
        try
        {
            var evt = new HealthEvent(command.UserId, command.AnimalId, command.Title, command.Description,
                command.EventType, command.ScheduledDate, command.Notes);
            await healthEventRepository.AddAsync(evt);
            await unitOfWork.CompleteAsync();
            return Result<HealthEvent>.Success(evt);
        }
        catch (Exception)
        {
            return Result<HealthEvent>.Failure(HealthError.InvalidVaccinationData);
        }
    }

    public async Task<Result<HealthEvent>> UpdateHealthEventAsync(UpdateHealthEventCommand command)
    {
        try
        {
            var evt = await healthEventRepository.FindByIdAsync(command.EventId);
            if (evt == null)
                return Result<HealthEvent>.Failure(HealthError.VaccinationNotFound);
            evt.UpdateDetails(command.Title, command.Description, command.EventType,
                command.ScheduledDate, command.AnimalId, command.Notes);
            await unitOfWork.CompleteAsync();
            return Result<HealthEvent>.Success(evt);
        }
        catch (Exception)
        {
            return Result<HealthEvent>.Failure(HealthError.InvalidVaccinationData);
        }
    }

    public async Task<Result> DeleteHealthEventAsync(DeleteHealthEventCommand command)
    {
        try
        {
            var evt = await healthEventRepository.FindByIdAsync(command.EventId);
            if (evt == null)
                return Result.Failure(HealthError.VaccinationNotFound);
            healthEventRepository.Remove(evt);
            await unitOfWork.CompleteAsync();
            return Result.Success();
        }
        catch (Exception)
        {
            return Result.Failure(HealthError.VaccinationNotFound);
        }
    }

    public async Task<Result<HealthEvent>> CompleteHealthEventAsync(CompleteHealthEventCommand command)
    {
        try
        {
            var evt = await healthEventRepository.FindByIdAsync(command.EventId);
            if (evt == null)
                return Result<HealthEvent>.Failure(HealthError.VaccinationNotFound);
            evt.MarkAsCompleted(command.CompletedBy);
            await unitOfWork.CompleteAsync();
            return Result<HealthEvent>.Success(evt);
        }
        catch (Exception)
        {
            return Result<HealthEvent>.Failure(HealthError.InvalidVaccinationData);
        }
    }

    public async Task<Result<HealthEvent>> PostponeHealthEventAsync(PostponeHealthEventCommand command)
    {
        try
        {
            var evt = await healthEventRepository.FindByIdAsync(command.EventId);
            if (evt == null)
                return Result<HealthEvent>.Failure(HealthError.VaccinationNotFound);
            evt.Postpone(command.PostponedTo);
            await unitOfWork.CompleteAsync();
            return Result<HealthEvent>.Success(evt);
        }
        catch (Exception)
        {
            return Result<HealthEvent>.Failure(HealthError.InvalidVaccinationData);
        }
    }

    public async Task<Result> RegisterHealthEventBatchAsync(RegisterHealthEventBatchCommand command)
    {
        try
        {
            foreach (var item in command.Events)
            {
                var evt = new HealthEvent(item.UserId, item.AnimalId, item.Title, item.Description,
                    item.EventType, item.ScheduledDate, item.Notes);
                await healthEventRepository.AddAsync(evt);
            }
            await unitOfWork.CompleteAsync();
            return Result.Success();
        }
        catch (Exception)
        {
            return Result.Failure(HealthError.InvalidVaccinationData);
        }
    }

    public async Task<Result<TreatmentDose>> RegisterTreatmentDoseAsync(RegisterTreatmentDoseCommand command)
    {
        try
        {
            var treatment = await treatmentRepository.FindByIdAsync(command.TreatmentId);
            if (treatment == null)
                return Result<TreatmentDose>.Failure(HealthError.TreatmentNotFound);

            var dose = new TreatmentDose(command.TreatmentId, command.AdministeredDate,
                command.AdministeredBy, command.Dosage, command.Notes);
            await treatmentRepository.AddDoseAsync(dose);
            await unitOfWork.CompleteAsync();
            return Result<TreatmentDose>.Success(dose);
        }
        catch (Exception)
        {
            return Result<TreatmentDose>.Failure(HealthError.InvalidTreatmentData);
        }
    }

    public async Task<Result<HealthAlert>> RegisterHealthAlertAsync(RegisterHealthAlertCommand command)
    {
        try
        {
            var alert = new HealthAlert(command.UserId, command.AnimalId, command.AlertType, command.Description);
            await healthAlertRepository.AddAsync(alert);
            await unitOfWork.CompleteAsync();
            return Result<HealthAlert>.Success(alert);
        }
        catch (Exception)
        {
            return Result<HealthAlert>.Failure(HealthError.InvalidHealthAlertData);
        }
    }

    public async Task<Result<HealthAlert>> ResolveHealthAlertAsync(ResolveHealthAlertCommand command)
    {
        try
        {
            var alert = await healthAlertRepository.FindByIdAsync(command.AlertId);
            if (alert == null)
                return Result<HealthAlert>.Failure(HealthError.HealthAlertNotFound);

            alert.Resolve(command.ResolvedBy, command.Notes);
            await unitOfWork.CompleteAsync();
            return Result<HealthAlert>.Success(alert);
        }
        catch (Exception)
        {
            return Result<HealthAlert>.Failure(HealthError.InvalidHealthAlertData);
        }
    }

    public async Task<Result> DismissHealthAlertAsync(DismissHealthAlertCommand command)
    {
        try
        {
            var alert = await healthAlertRepository.FindByIdAsync(command.AlertId);
            if (alert == null)
                return Result.Failure(HealthError.HealthAlertNotFound);

            healthAlertRepository.Remove(alert);
            await unitOfWork.CompleteAsync();
            return Result.Success();
        }
        catch (Exception)
        {
            return Result.Failure(HealthError.HealthAlertNotFound);
        }
    }

    public async Task<Result<HealthAlert>> AcknowledgeHealthAlertAsync(AcknowledgeHealthAlertCommand command)
    {
        try
        {
            var alert = await healthAlertRepository.FindByIdAsync(command.AlertId);
            if (alert == null)
                return Result<HealthAlert>.Failure(HealthError.HealthAlertNotFound);

            alert.Acknowledge(command.AcknowledgedBy);
            await unitOfWork.CompleteAsync();
            return Result<HealthAlert>.Success(alert);
        }
        catch (Exception)
        {
            return Result<HealthAlert>.Failure(HealthError.InvalidHealthAlertData);
        }
    }
}
