namespace Titan.AniTec.Platform.Financial.Domain.Repositories;

public record GetAllTransactionsQuery(int UserId);
public record GetTransactionByIdQuery(int UserId, int TransactionId);
public record GetTransactionsByTypeQuery(int UserId, string Type);
public record GetTransactionsByCategoryQuery(int UserId, string Category);
public record GetTransactionsByDateRangeQuery(int UserId, DateTime Start, DateTime End);
public record GetTransactionsByAnimalQuery(int UserId, int AnimalId);
public record GetIncomeSummaryQuery(int UserId, int Year, int? Month);
public record GetExpenseSummaryQuery(int UserId, int Year, int? Month);

public record GetAllBudgetsQuery(int UserId);
public record GetBudgetByIdQuery(int UserId, int BudgetId);
public record GetBudgetsByYearQuery(int UserId, int Year);
public record GetBudgetsByMonthQuery(int UserId, int Year, int Month);
public record GetCurrentBudgetQuery(int UserId);
public record GetBudgetVsActualQuery(int UserId, int BudgetId);

public record BudgetVsActual(int BudgetId, string Category, string BudgetType,
    decimal PlannedAmount, decimal ActualAmount, decimal Difference, int Year, int Month);

// Categories
public record GetAllCategoriesQuery(int UserId);
public record GetCategoryByIdQuery(int UserId, int CategoryId);
public record GetCategoriesByTypeQuery(int UserId, string Type);

// Reports
public record GetFinancialReportSummaryQuery(int UserId);
public record GetIncomeStatementQuery(int UserId, string Period);
public record GetCashFlowQuery(int UserId, DateTime Start, DateTime End);
public record GetMonthlyComparisonQuery(int UserId, int Year);
public record GetYearlyComparisonQuery(int UserId);
public record GetReportByAnimalQuery(int UserId, int AnimalId);
public record GetReportByCategoryQuery(int UserId);
public record GetProfitabilityQuery(int UserId);
public record GetProfitabilityByAnimalQuery(int UserId);
public record GetProfitabilityByRaceQuery(int UserId);
public record GetTaxSummaryQuery(int UserId, int Year);
public record GetExportReportQuery(int UserId, string ReportType, string Format);
public record GetDashboardIndicatorsQuery(int UserId);
public record GetFinancialTrendsQuery(int UserId);

// Report result records
public record FinancialReportSummary(decimal TotalIncome, decimal TotalExpense, decimal NetBalance,
    int TransactionCount, int IncomeCount, int ExpenseCount);

public record IncomeStatement(decimal TotalIncome, decimal TotalExpense, decimal NetIncome,
    IReadOnlyCollection<IncomeStatementLine> Lines);

public record IncomeStatementLine(string Category, decimal Amount, decimal Percentage);

public record CashFlowReport(decimal OpeningBalance, decimal TotalInflows, decimal TotalOutflows,
    decimal ClosingBalance, IReadOnlyCollection<CashFlowLine> Lines);

public record CashFlowLine(DateTime Date, string Description, string Type, decimal Amount);

public record MonthlyComparisonReport(IReadOnlyCollection<MonthlyComparisonLine> Lines);

public record MonthlyComparisonLine(int Month, decimal Income, decimal Expense, decimal Net);

public record YearlyComparisonReport(IReadOnlyCollection<YearlyComparisonLine> Lines);

public record YearlyComparisonLine(int Year, decimal Income, decimal Expense, decimal Net);

public record ReportByAnimal(decimal TotalIncome, decimal TotalExpense, decimal Net, int AnimalId);

public record ReportByCategoryLine(string Category, decimal Income, decimal Expense, decimal Net);

public record ProfitabilityReport(decimal TotalIncome, decimal TotalExpense, decimal NetProfit,
    decimal ProfitMargin, decimal CostPerAnimal);

public record ProfitabilityByAnimalLine(int AnimalId, decimal Income, decimal Expense, decimal Profit);

public record ProfitabilityByRaceLine(string Race, decimal Income, decimal Expense, decimal Profit);

public record TaxSummary(decimal TotalIncome, decimal TotalExpense, decimal TaxableIncome,
    decimal EstimatedTax, int Year);

public record DashboardIndicators(decimal CurrentMonthIncome, decimal CurrentMonthExpense,
    decimal PendingInvoices, int ActiveBudgets, decimal BudgetUtilization,
    decimal MonthlyGrowth);

public record FinancialTrendLine(DateTime Date, decimal Income, decimal Expense, decimal Net);
