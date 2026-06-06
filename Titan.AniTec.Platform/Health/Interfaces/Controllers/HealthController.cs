using Titan.AniTec.Platform.Health.Application.CommandServices;
using Titan.AniTec.Platform.Health.Domain.Repositories;
using Titan.AniTec.Platform.Health.Interfaces.Assemblers;
using Titan.AniTec.Platform.Health.Interfaces.Resources;
using Titan.AniTec.Platform.Iam.Domain.Model.Aggregates;
using Titan.AniTec.Platform.Iam.Infrastructure.Pipeline.Middleware.Attributes;
using Microsoft.AspNetCore.Mvc;

namespace Titan.AniTec.Platform.Health.Interfaces.Controllers;

[ApiController]
[Route("api/health")]
[Authorize]
public class HealthController(
    IHealthQueryService queryService,
    IHealthCommandService commandService) : ControllerBase
{
    private int CurrentUserId => ((User)HttpContext.Items["User"]!).Id;

    // Vaccinations
    [HttpGet("vaccinations")]
    public async Task<IActionResult> GetAllVaccinations()
    {
        var query = new GetAllVaccinationsQuery(CurrentUserId);
        var result = await queryService.GetAllVaccinationsAsync(query);
        return HealthActionResultAssembler.ToActionResult(
            result.Map(list => list.Select(HealthAssembler.ToResource).ToList()));
    }

    [HttpGet("vaccinations/{vaccinationId:int}")]
    public async Task<IActionResult> GetVaccinationById(int vaccinationId)
    {
        var query = new GetVaccinationByIdQuery(CurrentUserId, vaccinationId);
        var result = await queryService.GetVaccinationByIdAsync(query);
        return HealthActionResultAssembler.ToActionResult(result.Map(HealthAssembler.ToResource));
    }

    [HttpGet("vaccinations/by-animal/{animalId:int}")]
    public async Task<IActionResult> GetVaccinationsByAnimal(int animalId)
    {
        var query = new GetVaccinationsByAnimalQuery(CurrentUserId, animalId);
        var result = await queryService.GetVaccinationsByAnimalAsync(query);
        return HealthActionResultAssembler.ToActionResult(
            result.Map(list => list.Select(HealthAssembler.ToResource).ToList()));
    }

    [HttpGet("vaccinations/by-date-range")]
    public async Task<IActionResult> GetVaccinationsByDateRange([FromQuery] DateTime start, [FromQuery] DateTime end)
    {
        var query = new GetVaccinationsByDateRangeQuery(CurrentUserId, start, end);
        var result = await queryService.GetVaccinationsByDateRangeAsync(query);
        return HealthActionResultAssembler.ToActionResult(
            result.Map(list => list.Select(HealthAssembler.ToResource).ToList()));
    }

    [HttpGet("vaccinations/upcoming")]
    public async Task<IActionResult> GetUpcomingVaccinations()
    {
        var query = new GetUpcomingVaccinationsQuery(CurrentUserId);
        var result = await queryService.GetUpcomingVaccinationsAsync(query);
        return HealthActionResultAssembler.ToActionResult(
            result.Map(list => list.Select(HealthAssembler.ToResource).ToList()));
    }

    [HttpPost("vaccinations")]
    public async Task<IActionResult> CreateVaccination([FromBody] CreateVaccinationResource resource)
    {
        var command = HealthAssembler.ToCommand(CurrentUserId, resource);
        var result = await commandService.RegisterVaccinationAsync(command);
        return HealthActionResultAssembler.ToCreatedActionResult(result.Map(HealthAssembler.ToResource));
    }

    [HttpPut("vaccinations/{vaccinationId:int}")]
    public async Task<IActionResult> UpdateVaccination(int vaccinationId, [FromBody] UpdateVaccinationResource resource)
    {
        var command = HealthAssembler.ToCommand(CurrentUserId, vaccinationId, resource);
        var result = await commandService.UpdateVaccinationAsync(command);
        return HealthActionResultAssembler.ToActionResult(result.Map(HealthAssembler.ToResource));
    }

    [HttpDelete("vaccinations/{vaccinationId:int}")]
    public async Task<IActionResult> DeleteVaccination(int vaccinationId)
    {
        var command = new DeleteVaccinationCommand(CurrentUserId, vaccinationId);
        var result = await commandService.DeleteVaccinationAsync(command);
        return HealthActionResultAssembler.ToActionResult(result);
    }

    // Treatments
    [HttpGet("treatments")]
    public async Task<IActionResult> GetAllTreatments()
    {
        var query = new GetAllTreatmentsQuery(CurrentUserId);
        var result = await queryService.GetAllTreatmentsAsync(query);
        return HealthActionResultAssembler.ToActionResult(
            result.Map(list => list.Select(HealthAssembler.ToResource).ToList()));
    }

    [HttpGet("treatments/{treatmentId:int}")]
    public async Task<IActionResult> GetTreatmentById(int treatmentId)
    {
        var query = new GetTreatmentByIdQuery(CurrentUserId, treatmentId);
        var result = await queryService.GetTreatmentByIdAsync(query);
        return HealthActionResultAssembler.ToActionResult(result.Map(HealthAssembler.ToResource));
    }

    [HttpGet("treatments/by-animal/{animalId:int}")]
    public async Task<IActionResult> GetTreatmentsByAnimal(int animalId)
    {
        var query = new GetTreatmentsByAnimalQuery(CurrentUserId, animalId);
        var result = await queryService.GetTreatmentsByAnimalAsync(query);
        return HealthActionResultAssembler.ToActionResult(
            result.Map(list => list.Select(HealthAssembler.ToResource).ToList()));
    }

    [HttpGet("treatments/by-date-range")]
    public async Task<IActionResult> GetTreatmentsByDateRange([FromQuery] DateTime start, [FromQuery] DateTime end)
    {
        var query = new GetTreatmentsByDateRangeQuery(CurrentUserId, start, end);
        var result = await queryService.GetTreatmentsByDateRangeAsync(query);
        return HealthActionResultAssembler.ToActionResult(
            result.Map(list => list.Select(HealthAssembler.ToResource).ToList()));
    }

    [HttpPost("treatments")]
    public async Task<IActionResult> CreateTreatment([FromBody] CreateTreatmentResource resource)
    {
        var command = HealthAssembler.ToCommand(CurrentUserId, resource);
        var result = await commandService.RegisterTreatmentAsync(command);
        return HealthActionResultAssembler.ToCreatedActionResult(result.Map(HealthAssembler.ToResource));
    }

    [HttpPut("treatments/{treatmentId:int}")]
    public async Task<IActionResult> UpdateTreatment(int treatmentId, [FromBody] UpdateTreatmentResource resource)
    {
        var command = HealthAssembler.ToCommand(CurrentUserId, treatmentId, resource);
        var result = await commandService.UpdateTreatmentAsync(command);
        return HealthActionResultAssembler.ToActionResult(result.Map(HealthAssembler.ToResource));
    }

    [HttpDelete("treatments/{treatmentId:int}")]
    public async Task<IActionResult> DeleteTreatment(int treatmentId)
    {
        var command = new DeleteTreatmentCommand(CurrentUserId, treatmentId);
        var result = await commandService.DeleteTreatmentAsync(command);
        return HealthActionResultAssembler.ToActionResult(result);
    }

    // Veterinary Visits
    [HttpGet("visits")]
    public async Task<IActionResult> GetAllVisits()
    {
        var query = new GetAllVeterinaryVisitsQuery(CurrentUserId);
        var result = await queryService.GetAllVeterinaryVisitsAsync(query);
        return HealthActionResultAssembler.ToActionResult(
            result.Map(list => list.Select(HealthAssembler.ToResource).ToList()));
    }

    [HttpGet("visits/{visitId:int}")]
    public async Task<IActionResult> GetVisitById(int visitId)
    {
        var query = new GetVeterinaryVisitByIdQuery(CurrentUserId, visitId);
        var result = await queryService.GetVeterinaryVisitByIdAsync(query);
        return HealthActionResultAssembler.ToActionResult(result.Map(HealthAssembler.ToResource));
    }

    [HttpGet("visits/by-animal/{animalId:int}")]
    public async Task<IActionResult> GetVisitsByAnimal(int animalId)
    {
        var query = new GetVeterinaryVisitsByAnimalQuery(CurrentUserId, animalId);
        var result = await queryService.GetVeterinaryVisitsByAnimalAsync(query);
        return HealthActionResultAssembler.ToActionResult(
            result.Map(list => list.Select(HealthAssembler.ToResource).ToList()));
    }

    [HttpGet("visits/by-date-range")]
    public async Task<IActionResult> GetVisitsByDateRange([FromQuery] DateTime start, [FromQuery] DateTime end)
    {
        var query = new GetVeterinaryVisitsByDateRangeQuery(CurrentUserId, start, end);
        var result = await queryService.GetVeterinaryVisitsByDateRangeAsync(query);
        return HealthActionResultAssembler.ToActionResult(
            result.Map(list => list.Select(HealthAssembler.ToResource).ToList()));
    }

    [HttpPost("visits")]
    public async Task<IActionResult> CreateVisit([FromBody] CreateVeterinaryVisitResource resource)
    {
        var command = HealthAssembler.ToCommand(CurrentUserId, resource);
        var result = await commandService.RegisterVeterinaryVisitAsync(command);
        return HealthActionResultAssembler.ToCreatedActionResult(result.Map(HealthAssembler.ToResource));
    }

    [HttpPut("visits/{visitId:int}")]
    public async Task<IActionResult> UpdateVisit(int visitId, [FromBody] UpdateVeterinaryVisitResource resource)
    {
        var command = HealthAssembler.ToCommand(CurrentUserId, visitId, resource);
        var result = await commandService.UpdateVeterinaryVisitAsync(command);
        return HealthActionResultAssembler.ToActionResult(result.Map(HealthAssembler.ToResource));
    }

    [HttpDelete("visits/{visitId:int}")]
    public async Task<IActionResult> DeleteVisit(int visitId)
    {
        var command = new DeleteVeterinaryVisitCommand(CurrentUserId, visitId);
        var result = await commandService.DeleteVeterinaryVisitAsync(command);
        return HealthActionResultAssembler.ToActionResult(result);
    }

    // Health Alerts
    [HttpGet("alerts")]
    public async Task<IActionResult> GetAllAlerts()
    {
        var query = new GetAllHealthAlertsQuery(CurrentUserId);
        var result = await queryService.GetAllHealthAlertsAsync(query);
        return HealthActionResultAssembler.ToActionResult(
            result.Map(list => list.Select(HealthAssembler.ToResource).ToList()));
    }

    [HttpGet("alerts/{alertId:int}")]
    public async Task<IActionResult> GetAlertById(int alertId)
    {
        var query = new GetHealthAlertByIdQuery(CurrentUserId, alertId);
        var result = await queryService.GetHealthAlertByIdAsync(query);
        return HealthActionResultAssembler.ToActionResult(result.Map(HealthAssembler.ToResource));
    }

    [HttpGet("alerts/by-animal/{animalId:int}")]
    public async Task<IActionResult> GetAlertsByAnimal(int animalId)
    {
        var query = new GetHealthAlertsByAnimalQuery(CurrentUserId, animalId);
        var result = await queryService.GetHealthAlertsByAnimalAsync(query);
        return HealthActionResultAssembler.ToActionResult(
            result.Map(list => list.Select(HealthAssembler.ToResource).ToList()));
    }

    [HttpGet("alerts/open")]
    public async Task<IActionResult> GetOpenAlerts()
    {
        var query = new GetOpenHealthAlertsQuery(CurrentUserId);
        var result = await queryService.GetOpenHealthAlertsAsync(query);
        return HealthActionResultAssembler.ToActionResult(
            result.Map(list => list.Select(HealthAssembler.ToResource).ToList()));
    }

    [HttpGet("alerts/by-type/{alertType}")]
    public async Task<IActionResult> GetAlertsByType(string alertType)
    {
        var query = new GetHealthAlertsByTypeQuery(CurrentUserId, alertType);
        var result = await queryService.GetHealthAlertsByTypeAsync(query);
        return HealthActionResultAssembler.ToActionResult(
            result.Map(list => list.Select(HealthAssembler.ToResource).ToList()));
    }

    [HttpPost("alerts")]
    public async Task<IActionResult> CreateAlert([FromBody] CreateHealthAlertResource resource)
    {
        var command = HealthAssembler.ToCommand(CurrentUserId, resource);
        var result = await commandService.RegisterHealthAlertAsync(command);
        return HealthActionResultAssembler.ToCreatedActionResult(result.Map(HealthAssembler.ToResource));
    }

    [HttpPut("treatments/{treatmentId:int}/start")]
    public async Task<IActionResult> StartTreatment(int treatmentId)
    {
        var command = new StartTreatmentCommand(CurrentUserId, treatmentId);
        var result = await commandService.StartTreatmentAsync(command);
        return HealthActionResultAssembler.ToActionResult(result.Map(HealthAssembler.ToResource));
    }

    [HttpPut("treatments/{treatmentId:int}/complete")]
    public async Task<IActionResult> CompleteTreatment(int treatmentId)
    {
        var command = new CompleteTreatmentCommand(CurrentUserId, treatmentId);
        var result = await commandService.CompleteTreatmentAsync(command);
        return HealthActionResultAssembler.ToActionResult(result.Map(HealthAssembler.ToResource));
    }

    [HttpPut("treatments/{treatmentId:int}/cancel")]
    public async Task<IActionResult> CancelTreatment(int treatmentId)
    {
        var command = new CancelTreatmentCommand(CurrentUserId, treatmentId);
        var result = await commandService.CancelTreatmentAsync(command);
        return HealthActionResultAssembler.ToActionResult(result.Map(HealthAssembler.ToResource));
    }

    [HttpGet("vaccinations/by-vaccine/{vaccineName}")]
    public async Task<IActionResult> GetVaccinationsByVaccine(string vaccineName)
    {
        var all = await queryService.GetAllVaccinationsAsync(new GetAllVaccinationsQuery(CurrentUserId));
        if (all.IsFailure)
            return HealthActionResultAssembler.ToActionResult(all);

        var filtered = all.Data
            .Where(v => v.VaccineName.Contains(vaccineName, StringComparison.OrdinalIgnoreCase))
            .Select(HealthAssembler.ToResource)
            .ToList();

        return Ok(filtered);
    }

    // Vaccines Catalog
    [HttpGet("vaccines")]
    public async Task<IActionResult> GetAllVaccines()
    {
        var result = await queryService.GetAllVaccinesAsync(new GetAllVaccinesQuery());
        return HealthActionResultAssembler.ToActionResult(
            result.Map(list => list.Select(HealthAssembler.ToResource).ToList()));
    }

    [HttpGet("vaccines/{vaccineId:int}")]
    public async Task<IActionResult> GetVaccineById(int vaccineId)
    {
        var result = await queryService.GetVaccineByIdAsync(new GetVaccineByIdQuery(vaccineId));
        return HealthActionResultAssembler.ToActionResult(result.Map(HealthAssembler.ToResource));
    }

    [HttpPost("vaccines")]
    public async Task<IActionResult> CreateVaccine([FromBody] CreateVaccineResource resource)
    {
        var command = HealthAssembler.ToCommand(resource);
        var result = await commandService.RegisterVaccineAsync(command);
        return HealthActionResultAssembler.ToCreatedActionResult(result.Map(HealthAssembler.ToResource));
    }

    [HttpPut("vaccines/{vaccineId:int}")]
    public async Task<IActionResult> UpdateVaccine(int vaccineId, [FromBody] UpdateVaccineResource resource)
    {
        var command = HealthAssembler.ToCommand(vaccineId, resource);
        var result = await commandService.UpdateVaccineAsync(command);
        return HealthActionResultAssembler.ToActionResult(result.Map(HealthAssembler.ToResource));
    }

    [HttpDelete("vaccines/{vaccineId:int}")]
    public async Task<IActionResult> DeleteVaccine(int vaccineId)
    {
        var command = new DeleteVaccineCommand(vaccineId);
        var result = await commandService.DeleteVaccineAsync(command);
        return HealthActionResultAssembler.ToActionResult(result);
    }

    [HttpGet("vaccines/search")]
    public async Task<IActionResult> SearchVaccines([FromQuery] string q)
    {
        var result = await queryService.SearchVaccinesAsync(new SearchVaccinesQuery(q));
        return HealthActionResultAssembler.ToActionResult(
            result.Map(list => list.Select(HealthAssembler.ToResource).ToList()));
    }

    [HttpGet("vaccines/by-disease/{diseaseId:int}")]
    public async Task<IActionResult> GetVaccinesByDisease(int diseaseId)
    {
        var result = await queryService.GetVaccinesByDiseaseAsync(new GetVaccinesByDiseaseQuery(diseaseId));
        return HealthActionResultAssembler.ToActionResult(
            result.Map(list => list.Select(HealthAssembler.ToResource).ToList()));
    }

    [HttpGet("vaccines/by-race/{raceId:int}")]
    public async Task<IActionResult> GetVaccinesByRace(int raceId)
    {
        var result = await queryService.GetVaccinesByRaceAsync(new GetVaccinesByRaceQuery(raceId));
        return HealthActionResultAssembler.ToActionResult(
            result.Map(list => list.Select(HealthAssembler.ToResource).ToList()));
    }

    // Medications Catalog
    [HttpGet("medications")]
    public async Task<IActionResult> GetAllMedications()
    {
        var result = await queryService.GetAllMedicationsAsync(new GetAllMedicationsQuery());
        return HealthActionResultAssembler.ToActionResult(
            result.Map(list => list.Select(HealthAssembler.ToResource).ToList()));
    }

    [HttpGet("medications/{medicationId:int}")]
    public async Task<IActionResult> GetMedicationById(int medicationId)
    {
        var result = await queryService.GetMedicationByIdAsync(new GetMedicationByIdQuery(medicationId));
        return HealthActionResultAssembler.ToActionResult(result.Map(HealthAssembler.ToResource));
    }

    [HttpPost("medications")]
    public async Task<IActionResult> CreateMedication([FromBody] CreateMedicationResource resource)
    {
        var command = HealthAssembler.ToCommand(resource);
        var result = await commandService.RegisterMedicationAsync(command);
        return HealthActionResultAssembler.ToCreatedActionResult(result.Map(HealthAssembler.ToResource));
    }

    [HttpPut("medications/{medicationId:int}")]
    public async Task<IActionResult> UpdateMedication(int medicationId, [FromBody] UpdateMedicationResource resource)
    {
        var command = HealthAssembler.ToCommand(medicationId, resource);
        var result = await commandService.UpdateMedicationAsync(command);
        return HealthActionResultAssembler.ToActionResult(result.Map(HealthAssembler.ToResource));
    }

    [HttpDelete("medications/{medicationId:int}")]
    public async Task<IActionResult> DeleteMedication(int medicationId)
    {
        var command = new DeleteMedicationCommand(medicationId);
        var result = await commandService.DeleteMedicationAsync(command);
        return HealthActionResultAssembler.ToActionResult(result);
    }

    [HttpGet("medications/search")]
    public async Task<IActionResult> SearchMedications([FromQuery] string q)
    {
        var result = await queryService.SearchMedicationsAsync(new SearchMedicationsQuery(q));
        return HealthActionResultAssembler.ToActionResult(
            result.Map(list => list.Select(HealthAssembler.ToResource).ToList()));
    }

    [HttpGet("medications/by-category/{category}")]
    public async Task<IActionResult> GetMedicationsByCategory(string category)
    {
        var result = await queryService.GetMedicationsByCategoryAsync(new GetMedicationsByCategoryQuery(category));
        return HealthActionResultAssembler.ToActionResult(
            result.Map(list => list.Select(HealthAssembler.ToResource).ToList()));
    }

    // Diseases Catalog
    [HttpGet("diseases")]
    public async Task<IActionResult> GetAllDiseases()
    {
        var result = await queryService.GetAllDiseasesAsync(new GetAllDiseasesQuery());
        return HealthActionResultAssembler.ToActionResult(
            result.Map(list => list.Select(HealthAssembler.ToResource).ToList()));
    }

    [HttpGet("diseases/{diseaseId:int}")]
    public async Task<IActionResult> GetDiseaseById(int diseaseId)
    {
        var result = await queryService.GetDiseaseByIdAsync(new GetDiseaseByIdQuery(diseaseId));
        return HealthActionResultAssembler.ToActionResult(result.Map(HealthAssembler.ToResource));
    }

    [HttpPost("diseases")]
    public async Task<IActionResult> CreateDisease([FromBody] CreateDiseaseResource resource)
    {
        var command = HealthAssembler.ToCommand(resource);
        var result = await commandService.RegisterDiseaseAsync(command);
        return HealthActionResultAssembler.ToCreatedActionResult(result.Map(HealthAssembler.ToResource));
    }

    [HttpPut("diseases/{diseaseId:int}")]
    public async Task<IActionResult> UpdateDisease(int diseaseId, [FromBody] UpdateDiseaseResource resource)
    {
        var command = HealthAssembler.ToCommand(diseaseId, resource);
        var result = await commandService.UpdateDiseaseAsync(command);
        return HealthActionResultAssembler.ToActionResult(result.Map(HealthAssembler.ToResource));
    }

    [HttpDelete("diseases/{diseaseId:int}")]
    public async Task<IActionResult> DeleteDisease(int diseaseId)
    {
        var command = new DeleteDiseaseCommand(diseaseId);
        var result = await commandService.DeleteDiseaseAsync(command);
        return HealthActionResultAssembler.ToActionResult(result);
    }

    [HttpGet("diseases/by-race/{raceId:int}")]
    public async Task<IActionResult> GetDiseasesByRace(int raceId)
    {
        var result = await queryService.GetDiseasesByRaceAsync(new GetDiseasesByRaceQuery(raceId));
        return HealthActionResultAssembler.ToActionResult(
            result.Map(list => list.Select(HealthAssembler.ToResource).ToList()));
    }

    [HttpGet("diseases/by-symptoms")]
    public async Task<IActionResult> GetDiseasesBySymptoms([FromQuery] string symptoms)
    {
        var result = await queryService.GetDiseasesBySymptomsAsync(new GetDiseasesBySymptomsQuery(symptoms));
        return HealthActionResultAssembler.ToActionResult(
            result.Map(list => list.Select(HealthAssembler.ToResource).ToList()));
    }

    // Diagnoses
    [HttpGet("diagnoses")]
    public async Task<IActionResult> GetAllDiagnoses()
    {
        var result = await queryService.GetAllDiagnosesAsync(new GetAllDiagnosesQuery(CurrentUserId));
        return HealthActionResultAssembler.ToActionResult(
            result.Map(list => list.Select(HealthAssembler.ToResource).ToList()));
    }

    [HttpGet("diagnoses/{diagnosisId:int}")]
    public async Task<IActionResult> GetDiagnosisById(int diagnosisId)
    {
        var result = await queryService.GetDiagnosisByIdAsync(new GetDiagnosisByIdQuery(CurrentUserId, diagnosisId));
        return HealthActionResultAssembler.ToActionResult(result.Map(HealthAssembler.ToResource));
    }

    [HttpPost("diagnoses")]
    public async Task<IActionResult> CreateDiagnosis([FromBody] CreateDiagnosisResource resource)
    {
        var command = HealthAssembler.ToCommand(CurrentUserId, resource);
        var result = await commandService.RegisterDiagnosisAsync(command);
        return HealthActionResultAssembler.ToCreatedActionResult(result.Map(HealthAssembler.ToResource));
    }

    [HttpPut("diagnoses/{diagnosisId:int}")]
    public async Task<IActionResult> UpdateDiagnosis(int diagnosisId, [FromBody] UpdateDiagnosisResource resource)
    {
        var command = HealthAssembler.ToCommand(CurrentUserId, diagnosisId, resource);
        var result = await commandService.UpdateDiagnosisAsync(command);
        return HealthActionResultAssembler.ToActionResult(result.Map(HealthAssembler.ToResource));
    }

    [HttpDelete("diagnoses/{diagnosisId:int}")]
    public async Task<IActionResult> DeleteDiagnosis(int diagnosisId)
    {
        var command = new DeleteDiagnosisCommand(CurrentUserId, diagnosisId);
        var result = await commandService.DeleteDiagnosisAsync(command);
        return HealthActionResultAssembler.ToActionResult(result);
    }

    [HttpGet("diagnoses/by-animal/{animalId:int}")]
    public async Task<IActionResult> GetDiagnosesByAnimal(int animalId)
    {
        var result = await queryService.GetDiagnosesByAnimalAsync(new GetDiagnosesByAnimalQuery(CurrentUserId, animalId));
        return HealthActionResultAssembler.ToActionResult(
            result.Map(list => list.Select(HealthAssembler.ToResource).ToList()));
    }

    [HttpGet("diagnoses/by-disease/{diseaseId:int}")]
    public async Task<IActionResult> GetDiagnosesByDisease(int diseaseId)
    {
        var result = await queryService.GetDiagnosesByDiseaseAsync(new GetDiagnosesByDiseaseQuery(CurrentUserId, diseaseId));
        return HealthActionResultAssembler.ToActionResult(
            result.Map(list => list.Select(HealthAssembler.ToResource).ToList()));
    }

    [HttpGet("diagnoses/recent")]
    public async Task<IActionResult> GetRecentDiagnoses()
    {
        var result = await queryService.GetRecentDiagnosesAsync(new GetRecentDiagnosesQuery(CurrentUserId));
        return HealthActionResultAssembler.ToActionResult(
            result.Map(list => list.Select(HealthAssembler.ToResource).ToList()));
    }

    // Health Calendar / Events
    [HttpGet("calendar")]
    public async Task<IActionResult> GetCalendar()
    {
        var result = await queryService.GetHealthCalendarAsync(new GetHealthCalendarQuery(CurrentUserId));
        return HealthActionResultAssembler.ToActionResult(
            result.Map(list => list.Select(HealthAssembler.ToResource).ToList()));
    }

    [HttpGet("calendar/events")]
    public async Task<IActionResult> GetAllEvents()
    {
        var result = await queryService.GetAllHealthEventsAsync(new GetAllHealthEventsQuery(CurrentUserId));
        return HealthActionResultAssembler.ToActionResult(
            result.Map(list => list.Select(HealthAssembler.ToResource).ToList()));
    }

    [HttpGet("calendar/events/{eventId:int}")]
    public async Task<IActionResult> GetEventById(int eventId)
    {
        var result = await queryService.GetHealthEventByIdAsync(new GetHealthEventByIdQuery(CurrentUserId, eventId));
        return HealthActionResultAssembler.ToActionResult(result.Map(HealthAssembler.ToResource));
    }

    [HttpPost("calendar/events")]
    public async Task<IActionResult> CreateEvent([FromBody] CreateHealthEventResource resource)
    {
        var command = HealthAssembler.ToCommand(CurrentUserId, resource);
        var result = await commandService.RegisterHealthEventAsync(command);
        return HealthActionResultAssembler.ToCreatedActionResult(result.Map(HealthAssembler.ToResource));
    }

    [HttpPut("calendar/events/{eventId:int}")]
    public async Task<IActionResult> UpdateEvent(int eventId, [FromBody] UpdateHealthEventResource resource)
    {
        var command = HealthAssembler.ToCommand(CurrentUserId, eventId, resource);
        var result = await commandService.UpdateHealthEventAsync(command);
        return HealthActionResultAssembler.ToActionResult(result.Map(HealthAssembler.ToResource));
    }

    [HttpDelete("calendar/events/{eventId:int}")]
    public async Task<IActionResult> DeleteEvent(int eventId)
    {
        var command = new DeleteHealthEventCommand(CurrentUserId, eventId);
        var result = await commandService.DeleteHealthEventAsync(command);
        return HealthActionResultAssembler.ToActionResult(result);
    }

    [HttpGet("calendar/events/today")]
    public async Task<IActionResult> GetTodaysEvents()
    {
        var result = await queryService.GetTodaysHealthEventsAsync(new GetTodaysHealthEventsQuery(CurrentUserId));
        return HealthActionResultAssembler.ToActionResult(
            result.Map(list => list.Select(HealthAssembler.ToResource).ToList()));
    }

    [HttpGet("calendar/events/upcoming")]
    public async Task<IActionResult> GetUpcomingEvents([FromQuery] int days = 7)
    {
        var result = await queryService.GetUpcomingHealthEventsAsync(new GetUpcomingHealthEventsQuery(CurrentUserId, days));
        return HealthActionResultAssembler.ToActionResult(
            result.Map(list => list.Select(HealthAssembler.ToResource).ToList()));
    }

    [HttpGet("calendar/events/overdue")]
    public async Task<IActionResult> GetOverdueEvents()
    {
        var result = await queryService.GetOverdueHealthEventsAsync(new GetOverdueHealthEventsQuery(CurrentUserId));
        return HealthActionResultAssembler.ToActionResult(
            result.Map(list => list.Select(HealthAssembler.ToResource).ToList()));
    }

    [HttpGet("calendar/events/by-date-range")]
    public async Task<IActionResult> GetEventsByDateRange([FromQuery] DateTime start, [FromQuery] DateTime end)
    {
        var result = await queryService.GetHealthEventsByDateRangeAsync(
            new GetHealthEventsByDateRangeQuery(CurrentUserId, start, end));
        return HealthActionResultAssembler.ToActionResult(
            result.Map(list => list.Select(HealthAssembler.ToResource).ToList()));
    }

    [HttpGet("calendar/events/by-type/{eventType}")]
    public async Task<IActionResult> GetEventsByType(string eventType)
    {
        var result = await queryService.GetHealthEventsByTypeAsync(
            new GetHealthEventsByTypeQuery(CurrentUserId, eventType));
        return HealthActionResultAssembler.ToActionResult(
            result.Map(list => list.Select(HealthAssembler.ToResource).ToList()));
    }

    [HttpGet("calendar/events/by-animal/{animalId:int}")]
    public async Task<IActionResult> GetEventsByAnimal(int animalId)
    {
        var result = await queryService.GetHealthEventsByAnimalAsync(
            new GetHealthEventsByAnimalQuery(CurrentUserId, animalId));
        return HealthActionResultAssembler.ToActionResult(
            result.Map(list => list.Select(HealthAssembler.ToResource).ToList()));
    }

    [HttpPost("calendar/events/batch")]
    public async Task<IActionResult> CreateEventBatch([FromBody] IReadOnlyCollection<CreateHealthEventResource> resources)
    {
        var commands = resources.Select(r => HealthAssembler.ToCommand(CurrentUserId, r)).ToList();
        var command = new RegisterHealthEventBatchCommand(CurrentUserId, commands);
        var result = await commandService.RegisterHealthEventBatchAsync(command);
        return HealthActionResultAssembler.ToActionResult(result);
    }

    [HttpPut("calendar/events/{eventId:int}/complete")]
    public async Task<IActionResult> CompleteEvent(int eventId, [FromBody] CompleteHealthEventResource? resource)
    {
        var command = new CompleteHealthEventCommand(CurrentUserId, eventId, resource?.CompletedBy);
        var result = await commandService.CompleteHealthEventAsync(command);
        return HealthActionResultAssembler.ToActionResult(result.Map(HealthAssembler.ToResource));
    }

    [HttpPost("treatments/{treatmentId:int}/doses")]
    public async Task<IActionResult> RegisterDose(int treatmentId, [FromBody] CreateTreatmentDoseResource resource)
    {
        var command = HealthAssembler.ToCommand(CurrentUserId, treatmentId, resource);
        var result = await commandService.RegisterTreatmentDoseAsync(command);
        return HealthActionResultAssembler.ToCreatedActionResult(result.Map(HealthAssembler.ToResource));
    }

    [HttpGet("treatments/{treatmentId:int}/doses")]
    public async Task<IActionResult> GetDoses(int treatmentId)
    {
        var result = await queryService.GetTreatmentDosesAsync(
            new GetTreatmentDosesQuery(CurrentUserId, treatmentId));
        return HealthActionResultAssembler.ToActionResult(
            result.Map(list => list.Select(HealthAssembler.ToResource).ToList()));
    }

    [HttpPut("calendar/events/{eventId:int}/postpone")]
    public async Task<IActionResult> PostponeEvent(int eventId, [FromBody] PostponeHealthEventResource resource)
    {
        var command = new PostponeHealthEventCommand(CurrentUserId, eventId, resource.PostponedTo);
        var result = await commandService.PostponeHealthEventAsync(command);
        return HealthActionResultAssembler.ToActionResult(result.Map(HealthAssembler.ToResource));
    }

    [HttpPut("alerts/{alertId:int}/resolve")]
    public async Task<IActionResult> ResolveAlert(int alertId, [FromBody] ResolveHealthAlertResource resource)
    {
        var command = new ResolveHealthAlertCommand(CurrentUserId, alertId, resource.ResolvedBy, resource.Notes);
        var result = await commandService.ResolveHealthAlertAsync(command);
        return HealthActionResultAssembler.ToActionResult(result.Map(HealthAssembler.ToResource));
    }

    [HttpPut("alerts/{alertId:int}/acknowledge")]
    public async Task<IActionResult> AcknowledgeAlert(int alertId)
    {
        var command = new AcknowledgeHealthAlertCommand(CurrentUserId, alertId, null);
        var result = await commandService.AcknowledgeHealthAlertAsync(command);
        return HealthActionResultAssembler.ToActionResult(result.Map(HealthAssembler.ToResource));
    }

    [HttpPut("alerts/{alertId:int}/dismiss")]
    public async Task<IActionResult> DismissAlert(int alertId)
    {
        var command = new DismissHealthAlertCommand(CurrentUserId, alertId);
        var result = await commandService.DismissHealthAlertAsync(command);
        return HealthActionResultAssembler.ToActionResult(result);
    }

    [HttpGet("alerts/unacknowledged")]
    public async Task<IActionResult> GetUnacknowledgedAlerts()
    {
        var query = new GetUnacknowledgedHealthAlertsQuery(CurrentUserId);
        var result = await queryService.GetUnacknowledgedHealthAlertsAsync(query);
        return HealthActionResultAssembler.ToActionResult(
            result.Map(list => list.Select(HealthAssembler.ToResource).ToList()));
    }

    [HttpGet("alerts/by-priority/{priority}")]
    public async Task<IActionResult> GetAlertsByPriority(string priority)
    {
        var query = new GetHealthAlertsByPriorityQuery(CurrentUserId, priority);
        var result = await queryService.GetHealthAlertsByPriorityAsync(query);
        return HealthActionResultAssembler.ToActionResult(
            result.Map(list => list.Select(HealthAssembler.ToResource).ToList()));
    }

    [HttpGet("vaccinations/overdue")]
    public async Task<IActionResult> GetOverdueVaccinations()
    {
        var query = new GetOverdueVaccinationsQuery(CurrentUserId);
        var result = await queryService.GetOverdueVaccinationsAsync(query);
        return HealthActionResultAssembler.ToActionResult(
            result.Map(list => list.Select(HealthAssembler.ToResource).ToList()));
    }

    [HttpGet("treatments/by-type/{treatmentType}")]
    public async Task<IActionResult> GetTreatmentsByType(string treatmentType)
    {
        var query = new GetTreatmentsByTypeQuery(CurrentUserId, treatmentType);
        var result = await queryService.GetTreatmentsByTypeAsync(query);
        return HealthActionResultAssembler.ToActionResult(
            result.Map(list => list.Select(HealthAssembler.ToResource).ToList()));
    }

    [HttpGet("treatments/active")]
    public async Task<IActionResult> GetActiveTreatments()
    {
        var query = new GetActiveTreatmentsQuery(CurrentUserId);
        var result = await queryService.GetActiveTreatmentsAsync(query);
        return HealthActionResultAssembler.ToActionResult(
            result.Map(list => list.Select(HealthAssembler.ToResource).ToList()));
    }

    [HttpGet("treatments/completed")]
    public async Task<IActionResult> GetCompletedTreatments()
    {
        var query = new GetCompletedTreatmentsQuery(CurrentUserId);
        var result = await queryService.GetCompletedTreatmentsAsync(query);
        return HealthActionResultAssembler.ToActionResult(
            result.Map(list => list.Select(HealthAssembler.ToResource).ToList()));
    }

    [HttpGet("animals/{animalId:int}/medical-history")]
    public async Task<IActionResult> GetMedicalHistory(int animalId)
    {
        var vaccinations = await queryService.GetVaccinationsByAnimalAsync(
            new GetVaccinationsByAnimalQuery(CurrentUserId, animalId));
        var treatments = await queryService.GetTreatmentsByAnimalAsync(
            new GetTreatmentsByAnimalQuery(CurrentUserId, animalId));
        var visits = await queryService.GetVeterinaryVisitsByAnimalAsync(
            new GetVeterinaryVisitsByAnimalQuery(CurrentUserId, animalId));

        var history = new
        {
            Vaccinations = vaccinations.IsSuccess ? vaccinations.Data.Select(HealthAssembler.ToResource).ToList() : new List<VaccinationResource>(),
            Treatments = treatments.IsSuccess ? treatments.Data.Select(HealthAssembler.ToResource).ToList() : new List<TreatmentResource>(),
            Visits = visits.IsSuccess ? visits.Data.Select(HealthAssembler.ToResource).ToList() : new List<VeterinaryVisitResource>()
        };

        return Ok(history);
    }

    [HttpGet("animals/{animalId:int}/medical-history/summary")]
    public async Task<IActionResult> GetMedicalHistorySummary(int animalId)
    {
        var vaccinations = await queryService.GetVaccinationsByAnimalAsync(
            new GetVaccinationsByAnimalQuery(CurrentUserId, animalId));
        var treatments = await queryService.GetTreatmentsByAnimalAsync(
            new GetTreatmentsByAnimalQuery(CurrentUserId, animalId));
        var visits = await queryService.GetVeterinaryVisitsByAnimalAsync(
            new GetVeterinaryVisitsByAnimalQuery(CurrentUserId, animalId));

        var summary = new
        {
            TotalVaccinations = vaccinations.IsSuccess ? vaccinations.Data.Count : 0,
            TotalTreatments = treatments.IsSuccess ? treatments.Data.Count : 0,
            TotalVisits = visits.IsSuccess ? visits.Data.Count : 0,
            LastVaccination = vaccinations.IsSuccess ? vaccinations.Data.OrderByDescending(v => v.ApplicationDate).Select(HealthAssembler.ToResource).FirstOrDefault() : null,
            LastTreatment = treatments.IsSuccess ? treatments.Data.OrderByDescending(t => t.TreatmentDate).Select(HealthAssembler.ToResource).FirstOrDefault() : null,
            LastVisit = visits.IsSuccess ? visits.Data.OrderByDescending(v => v.VisitDate).Select(HealthAssembler.ToResource).FirstOrDefault() : null
        };

        return Ok(summary);
    }

    [HttpGet("animals/{animalId:int}/medical-history/timeline")]
    public async Task<IActionResult> GetMedicalHistoryTimeline(int animalId)
    {
        var vaccinations = await queryService.GetVaccinationsByAnimalAsync(
            new GetVaccinationsByAnimalQuery(CurrentUserId, animalId));
        var treatments = await queryService.GetTreatmentsByAnimalAsync(
            new GetTreatmentsByAnimalQuery(CurrentUserId, animalId));
        var visits = await queryService.GetVeterinaryVisitsByAnimalAsync(
            new GetVeterinaryVisitsByAnimalQuery(CurrentUserId, animalId));

        var timeline = new List<object>();

        if (vaccinations.IsSuccess)
            timeline.AddRange(vaccinations.Data.Select(v => new
            {
                Type = "vaccination",
                Date = v.ApplicationDate,
                Description = v.VaccineName,
                Data = HealthAssembler.ToResource(v)
            }));

        if (treatments.IsSuccess)
            timeline.AddRange(treatments.Data.Select(t => new
            {
                Type = "treatment",
                Date = t.TreatmentDate,
                Description = t.Diagnosis,
                Data = HealthAssembler.ToResource(t)
            }));

        if (visits.IsSuccess)
            timeline.AddRange(visits.Data.Select(v => new
            {
                Type = "visit",
                Date = v.VisitDate,
                Description = v.Reason,
                Data = HealthAssembler.ToResource(v)
            }));

        return Ok(timeline.OrderByDescending(e => e.Date).ToList());
    }

    [HttpGet("animals/{animalId:int}/medical-history/export")]
    public async Task<IActionResult> ExportMedicalHistory(int animalId)
    {
        var vaccinations = await queryService.GetVaccinationsByAnimalAsync(
            new GetVaccinationsByAnimalQuery(CurrentUserId, animalId));
        var treatments = await queryService.GetTreatmentsByAnimalAsync(
            new GetTreatmentsByAnimalQuery(CurrentUserId, animalId));
        var visits = await queryService.GetVeterinaryVisitsByAnimalAsync(
            new GetVeterinaryVisitsByAnimalQuery(CurrentUserId, animalId));

        var csv = "Type,Date,Description,Details\n";

        if (vaccinations.IsSuccess)
            foreach (var v in vaccinations.Data)
                csv += $"Vaccination,{v.ApplicationDate:yyyy-MM-dd},{v.VaccineName},{v.BatchNumber}\n";

        if (treatments.IsSuccess)
            foreach (var t in treatments.Data)
                csv += $"Treatment,{t.TreatmentDate:yyyy-MM-dd},{t.Diagnosis},{t.MedicationName}\n";

        if (visits.IsSuccess)
            foreach (var v in visits.Data)
                csv += $"Visit,{v.VisitDate:yyyy-MM-dd},{v.Reason},{v.VetName}\n";

        return File(System.Text.Encoding.UTF8.GetBytes(csv), "text/csv", $"medical-history-{animalId}.csv");
    }

    [HttpGet("animals/{animalId:int}/medical-history/share")]
    public async Task<IActionResult> ShareMedicalHistory(int animalId)
    {
        var shareLink = $"{Request.Scheme}://{Request.Host}/api/health/animals/{animalId}/medical-history";
        return Ok(new { ShareLink = shareLink, ExpiresAt = DateTime.UtcNow.AddDays(7) });
    }
}
