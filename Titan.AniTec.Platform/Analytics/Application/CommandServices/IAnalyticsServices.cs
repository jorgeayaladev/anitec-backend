using Titan.AniTec.Platform.Analytics.Domain.Model.Aggregates;
using Titan.AniTec.Platform.Analytics.Domain.Repositories;
using Titan.AniTec.Platform.Shared.Application.Model;

namespace Titan.AniTec.Platform.Analytics.Application.CommandServices;

public interface IAnalyticsCommandService
{
    Task<Result<ScheduledReport>> ScheduleReportAsync(ScheduleReportCommand command);
    Task<Result> CancelScheduledReportAsync(CancelScheduledReportCommand command);
}

public interface IAnalyticsQueryService
{
    // Farmer Dashboard
    Task<Result<object>> GetFarmerDashboardSummaryAsync(GetFarmerDashboardSummaryQuery query);
    Task<Result<object>> GetHatoSizeAsync(GetHatoSizeQuery query);
    Task<Result<object>> GetRecentBirthsAsync(GetRecentBirthsQuery query);
    Task<Result<object>> GetRecentTreatmentsAsync(GetRecentTreatmentsQuery query);
    Task<Result<object>> GetUpcomingEventsAsync(GetUpcomingEventsQuery query);
    Task<Result<object>> GetWeightTrendAsync(GetWeightTrendQuery query);
    Task<Result<object>> GetDeviceStatusAsync(GetDeviceStatusQuery query);
    Task<Result<object>> GetFinancialSummaryAsync(GetFinancialSummaryQuery query);
    Task<Result<object>> GetHealthOverviewAsync(GetHealthOverviewQuery query);
    Task<Result<object>> GetAlertsSummaryAsync(GetAlertsSummaryQuery query);

    // Veterinarian Dashboard
    Task<Result<object>> GetVetDashboardSummaryAsync(GetVetDashboardSummaryQuery query);
    Task<Result<object>> GetTodayAppointmentsAsync(GetTodayAppointmentsQuery query);
    Task<Result<object>> GetPendingCasesAsync(GetPendingCasesQuery query);
    Task<Result<object>> GetRecentPatientsAsync(GetRecentPatientsQuery query);
    Task<Result<object>> GetVetActiveTreatmentsAsync(GetVetActiveTreatmentsQuery query);
    Task<Result<object>> GetVetAlertsAsync(GetVetAlertsQuery query);
    Task<Result<object>> GetPatientStatisticsAsync(GetPatientStatisticsQuery query);
    Task<Result<object>> GetTopDiagnosesAsync(GetTopDiagnosesQuery query);
    Task<Result<object>> GetClientSummaryAsync(GetClientSummaryQuery query);

    // Health Statistics
    Task<Result<object>> GetDiseaseIncidenceAsync(GetDiseaseIncidenceQuery query);
    Task<Result<object>> GetDiseaseIncidenceByPeriodAsync(GetDiseaseIncidenceByPeriodQuery query);
    Task<Result<object>> GetTopDiseasesAsync(GetTopDiseasesQuery query);
    Task<Result<object>> GetTopTreatmentsAsync(GetTopTreatmentsQuery query);
    Task<Result<object>> GetTopMedicationsAsync(GetTopMedicationsQuery query);
    Task<Result<object>> GetVaccinationCoverageAsync(GetVaccinationCoverageQuery query);
    Task<Result<object>> GetMortalityRateAsync(GetMortalityRateQuery query);
    Task<Result<object>> GetRecoveryRateAsync(GetRecoveryRateQuery query);
    Task<Result<object>> GetHealthScoreAsync(GetHealthScoreQuery query);
    Task<Result<object>> GetHealthTrendsAsync(GetHealthTrendsQuery query);

    // Reproduction Statistics
    Task<Result<object>> GetReproductionSummaryAsync(GetReproductionSummaryQuery query);
    Task<Result<object>> GetBirthRateAsync(GetBirthRateQuery query);
    Task<Result<object>> GetConceptionRateAsync(GetConceptionRateQuery query);
    Task<Result<object>> GetCalvingIntervalAsync(GetCalvingIntervalQuery query);
    Task<Result<object>> GetWeaningRateAsync(GetWeaningRateQuery query);
    Task<Result<object>> GetFertilityByRaceAsync(GetFertilityByRaceQuery query);
    Task<Result<object>> GetReproductiveIndexAsync(GetReproductiveIndexQuery query);

    // Financial Statistics
    Task<Result<object>> GetIncomeTrendAsync(GetIncomeTrendQuery query);
    Task<Result<object>> GetExpenseTrendAsync(GetExpenseTrendQuery query);
    Task<Result<object>> GetProfitMarginAsync(GetProfitMarginQuery query);
    Task<Result<object>> GetCostPerAnimalAsync(GetCostPerAnimalQuery query);
    Task<Result<object>> GetRevenuePerAnimalAsync(GetRevenuePerAnimalQuery query);
    Task<Result<object>> GetRoiAsync(GetRoiQuery query);
    Task<Result<object>> GetBreakEvenAsync(GetBreakEvenQuery query);

    // Telemetry Statistics
    Task<Result<object>> GetWeightDistributionAsync(GetWeightDistributionQuery query);
    Task<Result<object>> GetTemperatureTrendsAsync(GetTemperatureTrendsQuery query);
    Task<Result<object>> GetActivityPatternsAsync(GetActivityPatternsQuery query);
    Task<Result<object>> GetFeedingPatternsAsync(GetFeedingPatternsQuery query);
    Task<Result<object>> GetRuminationAnalysisAsync(GetRuminationAnalysisQuery query);
    Task<Result<object>> GetMovementHeatmapAsync(GetMovementHeatmapQuery query);
    Task<Result<object>> GetHealthPredictionsAsync(GetHealthPredictionsQuery query);

    // Comparisons & Trends
    Task<Result<object>> GetPeriodComparisonAsync(GetPeriodComparisonQuery query);
    Task<Result<object>> GetComparisonByRaceAsync(GetComparisonByRaceQuery query);
    Task<Result<object>> GetComparisonByLocationAsync(GetComparisonByLocationQuery query);
    Task<Result<object>> GetHatoGrowthTrendAsync(GetHatoGrowthTrendQuery query);
    Task<Result<object>> GetHealthTrendsDataAsync(GetHealthTrendsDataQuery query);
    Task<Result<object>> GetFinancialTrendsDataAsync(GetFinancialTrendsDataQuery query);
    Task<Result<object>> GetGrowthPredictionAsync(GetGrowthPredictionQuery query);
    Task<Result<object>> GetDiseaseRiskPredictionAsync(GetDiseaseRiskPredictionQuery query);

    // Scheduled Reports
    Task<Result<IReadOnlyCollection<ScheduledReport>>> GetScheduledReportsAsync(GetScheduledReportsQuery query);
}
