using Titan.AniTec.Platform.Financial.Domain.Model.Aggregates;
using Titan.AniTec.Platform.Financial.Domain.Repositories;
using Titan.AniTec.Platform.Shared.Application.Model;

namespace Titan.AniTec.Platform.Financial.Application.CommandServices;

public interface IFinancialCommandService
{
    Task<Result<Transaction>> RegisterTransactionAsync(RegisterTransactionCommand command);
    Task<Result<Transaction>> UpdateTransactionAsync(UpdateTransactionCommand command);
    Task<Result> DeleteTransactionAsync(DeleteTransactionCommand command);
    Task<Result<Budget>> RegisterBudgetAsync(RegisterBudgetCommand command);
    Task<Result<Budget>> UpdateBudgetAsync(UpdateBudgetCommand command);
    Task<Result> DeleteBudgetAsync(DeleteBudgetCommand command);
    Task<Result> BatchCreateTransactionsAsync(BatchCreateTransactionsCommand command);
    Task<Result<FinancialCategory>> CreateCategoryAsync(CreateCategoryCommand command);
    Task<Result<FinancialCategory>> UpdateCategoryAsync(UpdateCategoryCommand command);
    Task<Result> DeleteCategoryAsync(DeleteCategoryCommand command);
}

public interface IFinancialQueryService
{
    Task<Result<IReadOnlyCollection<Transaction>>> GetAllTransactionsAsync(GetAllTransactionsQuery query);
    Task<Result<Transaction>> GetTransactionByIdAsync(GetTransactionByIdQuery query);
    Task<Result<IReadOnlyCollection<Transaction>>> GetTransactionsByTypeAsync(GetTransactionsByTypeQuery query);
    Task<Result<IReadOnlyCollection<Transaction>>> GetTransactionsByCategoryAsync(GetTransactionsByCategoryQuery query);
    Task<Result<IReadOnlyCollection<Transaction>>> GetTransactionsByDateRangeAsync(GetTransactionsByDateRangeQuery query);
    Task<Result<IReadOnlyCollection<Transaction>>> GetTransactionsByAnimalAsync(GetTransactionsByAnimalQuery query);
    Task<Result<decimal>> GetIncomeSummaryAsync(GetIncomeSummaryQuery query);
    Task<Result<decimal>> GetExpenseSummaryAsync(GetExpenseSummaryQuery query);

    // Categories
    Task<Result<IReadOnlyCollection<FinancialCategory>>> GetAllCategoriesAsync(GetAllCategoriesQuery query);
    Task<Result<FinancialCategory>> GetCategoryByIdAsync(GetCategoryByIdQuery query);
    Task<Result<IReadOnlyCollection<FinancialCategory>>> GetCategoriesByTypeAsync(GetCategoriesByTypeQuery query);

    // Reports
    Task<Result<FinancialReportSummary>> GetReportSummaryAsync(GetFinancialReportSummaryQuery query);
    Task<Result<IncomeStatement>> GetIncomeStatementAsync(GetIncomeStatementQuery query);
    Task<Result<CashFlowReport>> GetCashFlowAsync(GetCashFlowQuery query);
    Task<Result<MonthlyComparisonReport>> GetMonthlyComparisonAsync(GetMonthlyComparisonQuery query);
    Task<Result<YearlyComparisonReport>> GetYearlyComparisonAsync(GetYearlyComparisonQuery query);
    Task<Result<IReadOnlyCollection<ReportByAnimal>>> GetReportByAnimalAsync(GetReportByAnimalQuery query);
    Task<Result<IReadOnlyCollection<ReportByCategoryLine>>> GetReportByCategoryAsync(GetReportByCategoryQuery query);
    Task<Result<ProfitabilityReport>> GetProfitabilityAsync(GetProfitabilityQuery query);
    Task<Result<IReadOnlyCollection<ProfitabilityByAnimalLine>>> GetProfitabilityByAnimalAsync(GetProfitabilityByAnimalQuery query);
    Task<Result<IReadOnlyCollection<ProfitabilityByRaceLine>>> GetProfitabilityByRaceAsync(GetProfitabilityByRaceQuery query);
    Task<Result<TaxSummary>> GetTaxSummaryAsync(GetTaxSummaryQuery query);
    Task<Result<byte[]>> GetExportReportAsync(GetExportReportQuery query);
    Task<Result<DashboardIndicators>> GetDashboardIndicatorsAsync(GetDashboardIndicatorsQuery query);
    Task<Result<IReadOnlyCollection<FinancialTrendLine>>> GetFinancialTrendsAsync(GetFinancialTrendsQuery query);

    // Budgets
    Task<Result<IReadOnlyCollection<Budget>>> GetAllBudgetsAsync(GetAllBudgetsQuery query);
    Task<Result<Budget>> GetBudgetByIdAsync(GetBudgetByIdQuery query);
    Task<Result<IReadOnlyCollection<Budget>>> GetBudgetsByYearAsync(GetBudgetsByYearQuery query);
    Task<Result<IReadOnlyCollection<Budget>>> GetBudgetsByMonthAsync(GetBudgetsByMonthQuery query);
    Task<Result<IReadOnlyCollection<Budget>>> GetCurrentBudgetAsync(GetCurrentBudgetQuery query);
    Task<Result<BudgetVsActual>> GetBudgetVsActualAsync(GetBudgetVsActualQuery query);
}
