namespace Titan.AniTec.Platform.Analytics.Domain.Repositories;

// Farmer Dashboard
public record GetFarmerDashboardSummaryQuery(int UserId);
public record GetHatoSizeQuery(int UserId);
public record GetRecentBirthsQuery(int UserId);
public record GetRecentTreatmentsQuery(int UserId);
public record GetUpcomingEventsQuery(int UserId);
public record GetWeightTrendQuery(int UserId);
public record GetDeviceStatusQuery(int UserId);
public record GetFinancialSummaryQuery(int UserId);
public record GetHealthOverviewQuery(int UserId);
public record GetAlertsSummaryQuery(int UserId);

// Veterinarian Dashboard
public record GetVetDashboardSummaryQuery(int UserId);
public record GetTodayAppointmentsQuery(int UserId);
public record GetPendingCasesQuery(int UserId);
public record GetRecentPatientsQuery(int UserId);
public record GetVetActiveTreatmentsQuery(int UserId);
public record GetVetAlertsQuery(int UserId);
public record GetPatientStatisticsQuery(int UserId);
public record GetTopDiagnosesQuery(int UserId);
public record GetClientSummaryQuery(int UserId);

// Health Statistics
public record GetDiseaseIncidenceQuery(int UserId);
public record GetDiseaseIncidenceByPeriodQuery(int UserId, string Period);
public record GetTopDiseasesQuery(int UserId);
public record GetTopTreatmentsQuery(int UserId);
public record GetTopMedicationsQuery(int UserId);
public record GetVaccinationCoverageQuery(int UserId);
public record GetMortalityRateQuery(int UserId);
public record GetRecoveryRateQuery(int UserId);
public record GetHealthScoreQuery(int UserId);
public record GetHealthTrendsQuery(int UserId);

// Reproduction Statistics
public record GetReproductionSummaryQuery(int UserId);
public record GetBirthRateQuery(int UserId);
public record GetConceptionRateQuery(int UserId);
public record GetCalvingIntervalQuery(int UserId);
public record GetWeaningRateQuery(int UserId);
public record GetFertilityByRaceQuery(int UserId);
public record GetReproductiveIndexQuery(int UserId);

// Financial Statistics
public record GetIncomeTrendQuery(int UserId);
public record GetExpenseTrendQuery(int UserId);
public record GetProfitMarginQuery(int UserId);
public record GetCostPerAnimalQuery(int UserId);
public record GetRevenuePerAnimalQuery(int UserId);
public record GetRoiQuery(int UserId);
public record GetBreakEvenQuery(int UserId);

// Telemetry Statistics
public record GetWeightDistributionQuery(int UserId);
public record GetTemperatureTrendsQuery(int UserId);
public record GetActivityPatternsQuery(int UserId);
public record GetFeedingPatternsQuery(int UserId);
public record GetRuminationAnalysisQuery(int UserId);
public record GetMovementHeatmapQuery(int UserId);
public record GetHealthPredictionsQuery(int UserId);

// Comparisons & Trends
public record GetPeriodComparisonQuery(int UserId, DateTime? CurrentStart, DateTime? CurrentEnd, DateTime? PreviousStart, DateTime? PreviousEnd);
public record GetComparisonByRaceQuery(int UserId);
public record GetComparisonByLocationQuery(int UserId);
public record GetHatoGrowthTrendQuery(int UserId);
public record GetHealthTrendsDataQuery(int UserId);
public record GetFinancialTrendsDataQuery(int UserId);
public record GetGrowthPredictionQuery(int UserId);
public record GetDiseaseRiskPredictionQuery(int UserId);

// Scheduled Reports
public record GetScheduledReportsQuery(int UserId);
