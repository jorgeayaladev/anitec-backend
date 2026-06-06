using Titan.AniTec.Platform.Analytics.Application.CommandServices;
using Titan.AniTec.Platform.Analytics.Domain.Repositories;
using Titan.AniTec.Platform.Analytics.Interfaces.Assemblers;
using Titan.AniTec.Platform.Analytics.Interfaces.Resources;
using Titan.AniTec.Platform.Iam.Domain.Model.Aggregates;
using Titan.AniTec.Platform.Iam.Infrastructure.Pipeline.Middleware.Attributes;
using Microsoft.AspNetCore.Mvc;

namespace Titan.AniTec.Platform.Analytics.Interfaces.Controllers;

[ApiController]
[Route("api/analytics")]
[Authorize]
public class AnalyticsController(
    IAnalyticsQueryService queryService,
    IAnalyticsCommandService commandService) : ControllerBase
{
    private int CurrentFarmId => ((User)HttpContext.Items["User"]!).Id;

    // Farmer Dashboard
    [HttpGet("farmer/dashboard/summary")]
    public async Task<IActionResult> GetFarmerDashboardSummary()
    {
        var result = await queryService.GetFarmerDashboardSummaryAsync(new GetFarmerDashboardSummaryQuery(CurrentFarmId));
        return AnalyticsActionResultAssembler.ToActionResult(result);
    }

    [HttpGet("farmer/dashboard/hato-size")]
    public async Task<IActionResult> GetHatoSize()
    {
        var result = await queryService.GetHatoSizeAsync(new GetHatoSizeQuery(CurrentFarmId));
        return AnalyticsActionResultAssembler.ToActionResult(result);
    }

    [HttpGet("farmer/dashboard/recent-births")]
    public async Task<IActionResult> GetRecentBirths()
    {
        var result = await queryService.GetRecentBirthsAsync(new GetRecentBirthsQuery(CurrentFarmId));
        return AnalyticsActionResultAssembler.ToActionResult(result);
    }

    [HttpGet("farmer/dashboard/recent-treatments")]
    public async Task<IActionResult> GetRecentTreatments()
    {
        var result = await queryService.GetRecentTreatmentsAsync(new GetRecentTreatmentsQuery(CurrentFarmId));
        return AnalyticsActionResultAssembler.ToActionResult(result);
    }

    [HttpGet("farmer/dashboard/upcoming-events")]
    public async Task<IActionResult> GetUpcomingEvents()
    {
        var result = await queryService.GetUpcomingEventsAsync(new GetUpcomingEventsQuery(CurrentFarmId));
        return AnalyticsActionResultAssembler.ToActionResult(result);
    }

    [HttpGet("farmer/dashboard/weight-trend")]
    public async Task<IActionResult> GetWeightTrend()
    {
        var result = await queryService.GetWeightTrendAsync(new GetWeightTrendQuery(CurrentFarmId));
        return AnalyticsActionResultAssembler.ToActionResult(result);
    }

    [HttpGet("farmer/dashboard/device-status")]
    public async Task<IActionResult> GetDeviceStatus()
    {
        var result = await queryService.GetDeviceStatusAsync(new GetDeviceStatusQuery(CurrentFarmId));
        return AnalyticsActionResultAssembler.ToActionResult(result);
    }

    [HttpGet("farmer/dashboard/financial-summary")]
    public async Task<IActionResult> GetFinancialSummary()
    {
        var result = await queryService.GetFinancialSummaryAsync(new GetFinancialSummaryQuery(CurrentFarmId));
        return AnalyticsActionResultAssembler.ToActionResult(result);
    }

    [HttpGet("farmer/dashboard/health-overview")]
    public async Task<IActionResult> GetHealthOverview()
    {
        var result = await queryService.GetHealthOverviewAsync(new GetHealthOverviewQuery(CurrentFarmId));
        return AnalyticsActionResultAssembler.ToActionResult(result);
    }

    [HttpGet("farmer/dashboard/alerts-summary")]
    public async Task<IActionResult> GetAlertsSummary()
    {
        var result = await queryService.GetAlertsSummaryAsync(new GetAlertsSummaryQuery(CurrentFarmId));
        return AnalyticsActionResultAssembler.ToActionResult(result);
    }

    // Veterinarian Dashboard
    [HttpGet("veterinarian/dashboard/summary")]
    public async Task<IActionResult> GetVetDashboardSummary()
    {
        var result = await queryService.GetVetDashboardSummaryAsync(new GetVetDashboardSummaryQuery(CurrentFarmId));
        return AnalyticsActionResultAssembler.ToActionResult(result);
    }

    [HttpGet("veterinarian/dashboard/today-appointments")]
    public async Task<IActionResult> GetTodayAppointments()
    {
        var result = await queryService.GetTodayAppointmentsAsync(new GetTodayAppointmentsQuery(CurrentFarmId));
        return AnalyticsActionResultAssembler.ToActionResult(result);
    }

    [HttpGet("veterinarian/dashboard/pending-cases")]
    public async Task<IActionResult> GetPendingCases()
    {
        var result = await queryService.GetPendingCasesAsync(new GetPendingCasesQuery(CurrentFarmId));
        return AnalyticsActionResultAssembler.ToActionResult(result);
    }

    [HttpGet("veterinarian/dashboard/recent-patients")]
    public async Task<IActionResult> GetRecentPatients()
    {
        var result = await queryService.GetRecentPatientsAsync(new GetRecentPatientsQuery(CurrentFarmId));
        return AnalyticsActionResultAssembler.ToActionResult(result);
    }

    [HttpGet("veterinarian/dashboard/active-treatments")]
    public async Task<IActionResult> GetVetActiveTreatments()
    {
        var result = await queryService.GetVetActiveTreatmentsAsync(new GetVetActiveTreatmentsQuery(CurrentFarmId));
        return AnalyticsActionResultAssembler.ToActionResult(result);
    }

    [HttpGet("veterinarian/dashboard/alerts")]
    public async Task<IActionResult> GetVetAlerts()
    {
        var result = await queryService.GetVetAlertsAsync(new GetVetAlertsQuery(CurrentFarmId));
        return AnalyticsActionResultAssembler.ToActionResult(result);
    }

    [HttpGet("veterinarian/dashboard/patient-statistics")]
    public async Task<IActionResult> GetPatientStatistics()
    {
        var result = await queryService.GetPatientStatisticsAsync(new GetPatientStatisticsQuery(CurrentFarmId));
        return AnalyticsActionResultAssembler.ToActionResult(result);
    }

    [HttpGet("veterinarian/dashboard/top-diagnoses")]
    public async Task<IActionResult> GetTopDiagnoses()
    {
        var result = await queryService.GetTopDiagnosesAsync(new GetTopDiagnosesQuery(CurrentFarmId));
        return AnalyticsActionResultAssembler.ToActionResult(result);
    }

    [HttpGet("veterinarian/dashboard/client-summary")]
    public async Task<IActionResult> GetClientSummary()
    {
        var result = await queryService.GetClientSummaryAsync(new GetClientSummaryQuery(CurrentFarmId));
        return AnalyticsActionResultAssembler.ToActionResult(result);
    }

    // Health Statistics
    [HttpGet("health/disease-incidence")]
    public async Task<IActionResult> GetDiseaseIncidence()
    {
        var result = await queryService.GetDiseaseIncidenceAsync(new GetDiseaseIncidenceQuery(CurrentFarmId));
        return AnalyticsActionResultAssembler.ToActionResult(result);
    }

    [HttpGet("health/disease-incidence/by-period")]
    public async Task<IActionResult> GetDiseaseIncidenceByPeriod([FromQuery] string period)
    {
        var result = await queryService.GetDiseaseIncidenceByPeriodAsync(new GetDiseaseIncidenceByPeriodQuery(CurrentFarmId, period));
        return AnalyticsActionResultAssembler.ToActionResult(result);
    }

    [HttpGet("health/top-diseases")]
    public async Task<IActionResult> GetTopDiseases()
    {
        var result = await queryService.GetTopDiseasesAsync(new GetTopDiseasesQuery(CurrentFarmId));
        return AnalyticsActionResultAssembler.ToActionResult(result);
    }

    [HttpGet("health/top-treatments")]
    public async Task<IActionResult> GetTopTreatments()
    {
        var result = await queryService.GetTopTreatmentsAsync(new GetTopTreatmentsQuery(CurrentFarmId));
        return AnalyticsActionResultAssembler.ToActionResult(result);
    }

    [HttpGet("health/top-medications")]
    public async Task<IActionResult> GetTopMedications()
    {
        var result = await queryService.GetTopMedicationsAsync(new GetTopMedicationsQuery(CurrentFarmId));
        return AnalyticsActionResultAssembler.ToActionResult(result);
    }

    [HttpGet("health/vaccination-coverage")]
    public async Task<IActionResult> GetVaccinationCoverage()
    {
        var result = await queryService.GetVaccinationCoverageAsync(new GetVaccinationCoverageQuery(CurrentFarmId));
        return AnalyticsActionResultAssembler.ToActionResult(result);
    }

    [HttpGet("health/mortality-rate")]
    public async Task<IActionResult> GetMortalityRate()
    {
        var result = await queryService.GetMortalityRateAsync(new GetMortalityRateQuery(CurrentFarmId));
        return AnalyticsActionResultAssembler.ToActionResult(result);
    }

    [HttpGet("health/recovery-rate")]
    public async Task<IActionResult> GetRecoveryRate()
    {
        var result = await queryService.GetRecoveryRateAsync(new GetRecoveryRateQuery(CurrentFarmId));
        return AnalyticsActionResultAssembler.ToActionResult(result);
    }

    [HttpGet("health/health-score")]
    public async Task<IActionResult> GetHealthScore()
    {
        var result = await queryService.GetHealthScoreAsync(new GetHealthScoreQuery(CurrentFarmId));
        return AnalyticsActionResultAssembler.ToActionResult(result);
    }

    [HttpGet("health/trends")]
    public async Task<IActionResult> GetHealthTrends()
    {
        var result = await queryService.GetHealthTrendsAsync(new GetHealthTrendsQuery(CurrentFarmId));
        return AnalyticsActionResultAssembler.ToActionResult(result);
    }

    // Reproduction Statistics
    [HttpGet("reproduction/summary")]
    public async Task<IActionResult> GetReproductionSummary()
    {
        var result = await queryService.GetReproductionSummaryAsync(new GetReproductionSummaryQuery(CurrentFarmId));
        return AnalyticsActionResultAssembler.ToActionResult(result);
    }

    [HttpGet("reproduction/birth-rate")]
    public async Task<IActionResult> GetBirthRate()
    {
        var result = await queryService.GetBirthRateAsync(new GetBirthRateQuery(CurrentFarmId));
        return AnalyticsActionResultAssembler.ToActionResult(result);
    }

    [HttpGet("reproduction/conception-rate")]
    public async Task<IActionResult> GetConceptionRate()
    {
        var result = await queryService.GetConceptionRateAsync(new GetConceptionRateQuery(CurrentFarmId));
        return AnalyticsActionResultAssembler.ToActionResult(result);
    }

    [HttpGet("reproduction/calving-interval")]
    public async Task<IActionResult> GetCalvingInterval()
    {
        var result = await queryService.GetCalvingIntervalAsync(new GetCalvingIntervalQuery(CurrentFarmId));
        return AnalyticsActionResultAssembler.ToActionResult(result);
    }

    [HttpGet("reproduction/weaning-rate")]
    public async Task<IActionResult> GetWeaningRate()
    {
        var result = await queryService.GetWeaningRateAsync(new GetWeaningRateQuery(CurrentFarmId));
        return AnalyticsActionResultAssembler.ToActionResult(result);
    }

    [HttpGet("reproduction/fertility-by-race")]
    public async Task<IActionResult> GetFertilityByRace()
    {
        var result = await queryService.GetFertilityByRaceAsync(new GetFertilityByRaceQuery(CurrentFarmId));
        return AnalyticsActionResultAssembler.ToActionResult(result);
    }

    [HttpGet("reproduction/reproductive-index")]
    public async Task<IActionResult> GetReproductiveIndex()
    {
        var result = await queryService.GetReproductiveIndexAsync(new GetReproductiveIndexQuery(CurrentFarmId));
        return AnalyticsActionResultAssembler.ToActionResult(result);
    }

    // Financial Statistics
    [HttpGet("finance/income-trend")]
    public async Task<IActionResult> GetIncomeTrend()
    {
        var result = await queryService.GetIncomeTrendAsync(new GetIncomeTrendQuery(CurrentFarmId));
        return AnalyticsActionResultAssembler.ToActionResult(result);
    }

    [HttpGet("finance/expense-trend")]
    public async Task<IActionResult> GetExpenseTrend()
    {
        var result = await queryService.GetExpenseTrendAsync(new GetExpenseTrendQuery(CurrentFarmId));
        return AnalyticsActionResultAssembler.ToActionResult(result);
    }

    [HttpGet("finance/profit-margin")]
    public async Task<IActionResult> GetProfitMargin()
    {
        var result = await queryService.GetProfitMarginAsync(new GetProfitMarginQuery(CurrentFarmId));
        return AnalyticsActionResultAssembler.ToActionResult(result);
    }

    [HttpGet("finance/cost-per-animal")]
    public async Task<IActionResult> GetCostPerAnimal()
    {
        var result = await queryService.GetCostPerAnimalAsync(new GetCostPerAnimalQuery(CurrentFarmId));
        return AnalyticsActionResultAssembler.ToActionResult(result);
    }

    [HttpGet("finance/revenue-per-animal")]
    public async Task<IActionResult> GetRevenuePerAnimal()
    {
        var result = await queryService.GetRevenuePerAnimalAsync(new GetRevenuePerAnimalQuery(CurrentFarmId));
        return AnalyticsActionResultAssembler.ToActionResult(result);
    }

    [HttpGet("finance/roi")]
    public async Task<IActionResult> GetRoi()
    {
        var result = await queryService.GetRoiAsync(new GetRoiQuery(CurrentFarmId));
        return AnalyticsActionResultAssembler.ToActionResult(result);
    }

    [HttpGet("finance/break-even")]
    public async Task<IActionResult> GetBreakEven()
    {
        var result = await queryService.GetBreakEvenAsync(new GetBreakEvenQuery(CurrentFarmId));
        return AnalyticsActionResultAssembler.ToActionResult(result);
    }

    // Telemetry Statistics
    [HttpGet("telemetry/weight-distribution")]
    public async Task<IActionResult> GetWeightDistribution()
    {
        var result = await queryService.GetWeightDistributionAsync(new GetWeightDistributionQuery(CurrentFarmId));
        return AnalyticsActionResultAssembler.ToActionResult(result);
    }

    [HttpGet("telemetry/temperature-trends")]
    public async Task<IActionResult> GetTemperatureTrends()
    {
        var result = await queryService.GetTemperatureTrendsAsync(new GetTemperatureTrendsQuery(CurrentFarmId));
        return AnalyticsActionResultAssembler.ToActionResult(result);
    }

    [HttpGet("telemetry/activity-patterns")]
    public async Task<IActionResult> GetActivityPatterns()
    {
        var result = await queryService.GetActivityPatternsAsync(new GetActivityPatternsQuery(CurrentFarmId));
        return AnalyticsActionResultAssembler.ToActionResult(result);
    }

    [HttpGet("telemetry/feeding-patterns")]
    public async Task<IActionResult> GetFeedingPatterns()
    {
        var result = await queryService.GetFeedingPatternsAsync(new GetFeedingPatternsQuery(CurrentFarmId));
        return AnalyticsActionResultAssembler.ToActionResult(result);
    }

    [HttpGet("telemetry/rumination-analysis")]
    public async Task<IActionResult> GetRuminationAnalysis()
    {
        var result = await queryService.GetRuminationAnalysisAsync(new GetRuminationAnalysisQuery(CurrentFarmId));
        return AnalyticsActionResultAssembler.ToActionResult(result);
    }

    [HttpGet("telemetry/movement-heatmap")]
    public async Task<IActionResult> GetMovementHeatmap()
    {
        var result = await queryService.GetMovementHeatmapAsync(new GetMovementHeatmapQuery(CurrentFarmId));
        return AnalyticsActionResultAssembler.ToActionResult(result);
    }

    [HttpGet("telemetry/health-predictions")]
    public async Task<IActionResult> GetHealthPredictions()
    {
        var result = await queryService.GetHealthPredictionsAsync(new GetHealthPredictionsQuery(CurrentFarmId));
        return AnalyticsActionResultAssembler.ToActionResult(result);
    }

    // Comparisons & Trends
    [HttpGet("comparisons/period")]
    public async Task<IActionResult> GetPeriodComparison(
        [FromQuery] DateTime? currentStart, [FromQuery] DateTime? currentEnd,
        [FromQuery] DateTime? previousStart, [FromQuery] DateTime? previousEnd)
    {
        var query = new GetPeriodComparisonQuery(CurrentFarmId, currentStart, currentEnd, previousStart, previousEnd);
        var result = await queryService.GetPeriodComparisonAsync(query);
        return AnalyticsActionResultAssembler.ToActionResult(result);
    }

    [HttpGet("comparisons/by-race")]
    public async Task<IActionResult> GetComparisonByRace()
    {
        var result = await queryService.GetComparisonByRaceAsync(new GetComparisonByRaceQuery(CurrentFarmId));
        return AnalyticsActionResultAssembler.ToActionResult(result);
    }

    [HttpGet("comparisons/by-location")]
    public async Task<IActionResult> GetComparisonByLocation()
    {
        var result = await queryService.GetComparisonByLocationAsync(new GetComparisonByLocationQuery(CurrentFarmId));
        return AnalyticsActionResultAssembler.ToActionResult(result);
    }

    [HttpGet("trends/hato-growth")]
    public async Task<IActionResult> GetHatoGrowthTrend()
    {
        var result = await queryService.GetHatoGrowthTrendAsync(new GetHatoGrowthTrendQuery(CurrentFarmId));
        return AnalyticsActionResultAssembler.ToActionResult(result);
    }

    [HttpGet("trends/health")]
    public async Task<IActionResult> GetHealthTrendsData()
    {
        var result = await queryService.GetHealthTrendsDataAsync(new GetHealthTrendsDataQuery(CurrentFarmId));
        return AnalyticsActionResultAssembler.ToActionResult(result);
    }

    [HttpGet("trends/financial")]
    public async Task<IActionResult> GetFinancialTrendsData()
    {
        var result = await queryService.GetFinancialTrendsDataAsync(new GetFinancialTrendsDataQuery(CurrentFarmId));
        return AnalyticsActionResultAssembler.ToActionResult(result);
    }

    [HttpGet("predictions/growth")]
    public async Task<IActionResult> GetGrowthPrediction()
    {
        var result = await queryService.GetGrowthPredictionAsync(new GetGrowthPredictionQuery(CurrentFarmId));
        return AnalyticsActionResultAssembler.ToActionResult(result);
    }

    [HttpGet("predictions/disease-risk")]
    public async Task<IActionResult> GetDiseaseRiskPrediction()
    {
        var result = await queryService.GetDiseaseRiskPredictionAsync(new GetDiseaseRiskPredictionQuery(CurrentFarmId));
        return AnalyticsActionResultAssembler.ToActionResult(result);
    }

    // Export & Scheduled Reports
    [HttpGet("export/{reportType}")]
    public async Task<IActionResult> ExportReport(string reportType, [FromQuery] string format = "pdf")
    {
        var data = new { ReportType = reportType, Format = format, GeneratedAt = DateTime.UtcNow };
        return format?.ToLower() switch
        {
            "csv" => File(
                System.Text.Encoding.UTF8.GetBytes(
                    $"ReportType,Format,GeneratedAt\n{reportType},{format},{DateTime.UtcNow:O}\n"),
                "text/csv", $"report-{reportType}.csv"),
            "excel" => Ok(new { ReportType = reportType, Format = format, Message = "Excel export not yet implemented" }),
            _ => Ok(data)
        };
    }

    [HttpPost("reports/schedule")]
    public async Task<IActionResult> ScheduleReport([FromBody] ScheduleReportResource resource)
    {
        var command = AnalyticsAssembler.ToCommand(CurrentFarmId, resource);
        var result = await commandService.ScheduleReportAsync(command);
        return AnalyticsActionResultAssembler.ToCreatedActionResult(result.Map(e => e.ToResource()));
    }

    [HttpGet("reports/scheduled")]
    public async Task<IActionResult> GetScheduledReports()
    {
        var result = await queryService.GetScheduledReportsAsync(new GetScheduledReportsQuery(CurrentFarmId));
        return AnalyticsActionResultAssembler.ToActionResult(
            result.Map(list => list.Select(e => e.ToResource()).ToList()));
    }

    [HttpDelete("reports/scheduled/{scheduleId:int}")]
    public async Task<IActionResult> CancelScheduledReport(int scheduleId)
    {
        var command = new CancelScheduledReportCommand(CurrentFarmId, scheduleId);
        var result = await commandService.CancelScheduledReportAsync(command);
        return AnalyticsActionResultAssembler.ToActionResult(result);
    }
}
